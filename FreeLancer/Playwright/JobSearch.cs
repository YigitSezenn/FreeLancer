using FreeLancer.Sessions;
using FreeLancer.Settings;
using Microsoft.Playwright;

namespace FreeLancer.Playwright
{
    internal class JobSearch
    {
        private const string GeminiUrl = "https://gemini.google.com/app?hl=tr";
        private const int MinBidLength = 100;
        private IPage? _gemini;

        public async Task RunAsync(IPage page)
        {
            await OpenSearchAsync(page);
            await ApplyFiltersAsync(page);
            await ApplyToJobsAsync(page);
        }

        private static async Task OpenSearchAsync(IPage page)
        {
            var browse = page.GetByRole(AriaRole.Button, new() { Name = "Browse" })
                .Or(page.GetByRole(AriaRole.Button, new() { Name = "Göz Atın" }))
                .First;
            await browse.WaitForAsync(Visible(20000));
            await browse.ClickAsync();
            await page.WaitForTimeoutAsync(3000);

            var search = page.GetByRole(AriaRole.Searchbox, new() { Name = "Arama Freelancer.com" })
                .Or(page.GetByRole(AriaRole.Searchbox, new() { Name = "Search Freelancer.com" }))
                .First;
            await search.ClickAsync();
            await search.FillAsync(AppSession.Current.Work);

            var viewAll = page.Locator("a").Filter(new() { HasText = "Hepsini Gör" })
                .Or(page.Locator("a").Filter(new() { HasText = "View All" }))
                .Last;
            await viewAll.WaitForAsync(Visible(20000));
            await viewAll.ClickAsync();
        }

        private static async Task ApplyFiltersAsync(IPage page)
        {
            var isHourly = (AppSession.Current.WorkType ?? "")
                .Contains("Hourly", StringComparison.OrdinalIgnoreCase);

            var selected = page.Locator("label.CheckboxLabel", new()
            {
                HasText = isHourly ? "Hourly Rate" : "Fixed Price"
            });
            var other = page.Locator("label.CheckboxLabel", new()
            {
                HasText = isHourly ? "Fixed Price" : "Hourly Rate"
            });
            var minInput = page.Locator(isHourly ? "#min-hourly-rate-input" : "#min-fixed-price-input");
            var otherInput = page.Locator(isHourly ? "#min-fixed-price-input" : "#min-hourly-rate-input");

            await selected.CheckAsync();
            if (await minInput.IsVisibleAsync())
                await otherInput.FillAsync("0");
            await other.UncheckAsync();

            await minInput.WaitForAsync(Visible(10000));
            var amount = isHourly ? AppSession.Current.HourlyRate : AppSession.Current.FixedPrice;
            await minInput.FillAsync(amount.ToString());
            await page.WaitForTimeoutAsync(2000);
        }

        private async Task ApplyToJobsAsync(IPage page)
        {
            var searchUrl = page.Url;
            var titles = page.Locator("h2.Title-text");
            await titles.First.WaitForAsync(Visible());

            var jobUrls = await CollectNewJobUrlsAsync(page, titles);
            foreach (var jobUrl in jobUrls)
            {
                try
                {
                    await BidOnJobAsync(page, jobUrl, searchUrl, titles);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Başvuru hatası");
                }
            }
        }

        private static async Task<List<string>> CollectNewJobUrlsAsync(IPage page, ILocator titles)
        {
            var links = page.Locator("a[href*='/projects/']")
                .Filter(new() { Has = page.Locator("h2.Title-text") });
            var count = await links.CountAsync();
            var urls = new List<string>();

            for (var i = 0; i < count; i++)
            {
                var url = await links.Nth(i).EvaluateAsync<string>("el => el.href");
                if (string.IsNullOrWhiteSpace(url))
                    continue;
                if (Applied.Current.AppliedJobs.Contains(url, StringComparer.OrdinalIgnoreCase))
                    continue;
                if (!urls.Contains(url, StringComparer.OrdinalIgnoreCase))
                    urls.Add(url);
            }

            if (urls.Count == 0 && count == 0)
            {
                throw new InvalidOperationException(
                    $"İlan linki bulunamadı. Başlık: {await titles.CountAsync()}, proje linki: {count}.");
            }

            return urls;
        }

        private async Task BidOnJobAsync(IPage page, string jobUrl, string searchUrl, ILocator titles)
        {
            await page.GotoAsync(jobUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
            await page.BringToFrontAsync();
            await page.WaitForTimeoutAsync(3000);

            var proposal = page.Locator("#descriptionTextArea");
            var restricted = page.GetByText("Bidding on this project is restricted")
                .Or(page.GetByText("teklif vermek kısıtlandı"));

            try
            {
                await proposal.WaitForAsync(Visible(10000));
            }
            catch (Exception)
            {
                // Form yoksa genelde zaten başvurulmuş veya kısıtlı ilandır.
            }

            if (await restricted.First.IsVisibleAsync() || !await proposal.IsVisibleAsync())
            {
                Applied.Apply(jobUrl);
                return;
            }

            var jobText = await CopyJobDescriptionAsync(page);
            if (string.IsNullOrWhiteSpace(jobText))
                throw new InvalidOperationException("İlan metni okunamadı: " + jobUrl);

            var bidText = await WriteBidWithGeminiAsync(page, jobText);
            if (string.IsNullOrWhiteSpace(bidText))
                throw new InvalidOperationException("Gemini cevabı boş: " + jobUrl);

            await page.BringToFrontAsync();
            await proposal.WaitForAsync(Visible());
            await proposal.ClickAsync();
            await proposal.FillAsync(bidText);

            var placeBid = page.GetByRole(AriaRole.Button, new() { Name = "Place Bid" })
                .Or(page.GetByRole(AriaRole.Button, new() { Name = "Teklif Ver" }));
            await placeBid.ClickAsync();
            Applied.Apply(jobUrl);

            await page.GotoAsync(searchUrl);
            await titles.First.WaitForAsync(Visible());
        }

        private static async Task<string> CopyJobDescriptionAsync(IPage page)
        {
            var more = page.GetByText("more", new() { Exact = true })
                .Or(page.GetByText("daha fazla", new() { Exact = true }));
            if (await more.First.IsVisibleAsync())
                await more.First.ClickAsync();

            var blocks = page.Locator("span.whitespace-pre-line");
            await blocks.First.WaitForAsync(Visible(8000));
            var parts = await blocks.AllInnerTextsAsync();
            return string.Join("\n", parts.Select(t => t.Trim()).Where(t => t.Length > 0));
        }

        private async Task<string> WriteBidWithGeminiAsync(IPage freelancerPage, string jobText)
        {
            if (_gemini is null || _gemini.IsClosed)
                _gemini = await freelancerPage.Context.NewPageAsync();

            await _gemini.BringToFrontAsync();
            await _gemini.GotoAsync(GeminiUrl);
            await _gemini.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await _gemini.WaitForTimeoutAsync(3000);

            var prompt = jobText
                + "\n\nİngilizce buna karşılık en az 100 karakterlik başvuru metni yaz. Sadece başvuru metnini yaz, başka bir şey ekleme.";

            var editor = _gemini.Locator("div.ql-editor[contenteditable='true']").Last;
            await editor.WaitForAsync(Visible(20000));
            await editor.ClickAsync();
            await editor.EvaluateAsync("""
                (el, text) => {
                    el.focus();
                    el.classList.remove('ql-blank');
                    document.execCommand('selectAll', false);
                    document.execCommand('insertText', false, text);
                }
                """, prompt);

            var send = _gemini.GetByRole(AriaRole.Button, new() { Name = "Send message" })
                .Or(_gemini.GetByRole(AriaRole.Button, new() { Name = "Mesaj gönder" }))
                .Or(_gemini.Locator("button.send-button"));
            if (await send.First.IsVisibleAsync())
                await send.First.ClickAsync();
            else
                await editor.PressAsync("Enter");

            await _gemini.WaitForTimeoutAsync(10_000);

            var answer = _gemini.Locator("message-content .markdown, div.markdown").Last;
            var bidText = (await answer.InnerTextAsync()).Trim();
            if (bidText.Length < MinBidLength)
                throw new InvalidOperationException($"Gemini metni {MinBidLength} karakterden kısa ({bidText.Length}).");

            await freelancerPage.BringToFrontAsync();
            return bidText;
        }

        private static LocatorWaitForOptions Visible(float timeout = 15000) => new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = timeout
        };
    }
}
