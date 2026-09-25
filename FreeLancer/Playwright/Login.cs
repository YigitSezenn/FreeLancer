using FreeLancer.Sessions;
using FreeLancer.Settings;
using Microsoft.Playwright;

namespace FreeLancer.Playwright
{
    internal class Login : PlaywrightBase
    {
        private const string Site = "https://www.tr.freelancer.com";

        public async Task FreeLancerLogin(IPage page)
        {
            await page.GotoAsync($"{Site}/dashboard");
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            var emailInput = page.Locator("#emailOrUsernameInput");
            var passwordInput = page.Locator("#passwordInput");
            var browse = page.GetByRole(AriaRole.Button, new() { Name = "Browse" })
                .Or(page.GetByRole(AriaRole.Button, new() { Name = "Göz Atın" }))
                .First;

            await emailInput.Or(browse).WaitForAsync(Visible(20000));

            var onDashboard = page.Url.Contains("/dashboard", StringComparison.OrdinalIgnoreCase)
                && !page.Url.Contains("/login", StringComparison.OrdinalIgnoreCase)
                && await browse.IsVisibleAsync();

            if (!onDashboard)
            {
                await emailInput.WaitForAsync(Visible());
                await passwordInput.WaitForAsync(Visible());
                await emailInput.ClickAsync();
                await emailInput.ClearAsync();
                await emailInput.PressSequentiallyAsync(AppSession.Current.Email, new() { Delay = 100 });
                await passwordInput.ClickAsync();
                await passwordInput.ClearAsync();
                await passwordInput.PressSequentiallyAsync(AppSession.Current.Password, new() { Delay = 100 });
                await page.WaitForTimeoutAsync(1000);
                await page.GetByText("Remember me", new() { Exact = true })
                    .Or(page.GetByText("Beni hatırla", new() { Exact = true }))
                    .CheckAsync();

                var loginButton = page.GetByRole(AriaRole.Button, new() { Name = "Log in" })
                    .Or(page.GetByRole(AriaRole.Button, new() { Name = "Giriş" }));
                await loginButton.WaitForAsync(Visible());
                await loginButton.ClickAsync();

                var dialog = MessageBox.Show(
                    "CAPTCHA'yı tarayıcıda tamamladın mı?",
                    "Giriş",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes)
                {
                    await BrowserDispose();
                    return;
                }

                await page.WaitForTimeoutAsync(3000);
            }

            await browse.WaitForAsync(Visible(20000));
            await browse.ClickAsync();
            await page.WaitForTimeoutAsync(3000);

            var searchbox = page.GetByRole(AriaRole.Searchbox, new() { Name = "Arama Freelancer.com" })
                .Or(page.GetByRole(AriaRole.Searchbox, new() { Name = "Search Freelancer.com" }))
                .First;
            await searchbox.ClickAsync();
            await searchbox.FillAsync(AppSession.Current.Work);

            var viewAll = page.Locator("a").Filter(new() { HasText = "Hepsini Gör" })
                .Or(page.Locator("a").Filter(new() { HasText = "View All" }))
                .Last;
            await viewAll.WaitForAsync(Visible(20000));
            await viewAll.ClickAsync();

            var isHourly = (AppSession.Current.WorkType ?? "")
                .Contains("Hourly", StringComparison.OrdinalIgnoreCase);
            var selectedCheckbox = page.Locator("label.CheckboxLabel", new()
            {
                HasText = isHourly ? "Hourly Rate" : "Fixed Price"
            });
            var otherCheckbox = page.Locator("label.CheckboxLabel", new()
            {
                HasText = isHourly ? "Fixed Price" : "Hourly Rate"
            });
            var minInput = page.Locator(isHourly ? "#min-hourly-rate-input" : "#min-fixed-price-input");
            var otherInput = page.Locator(isHourly ? "#min-fixed-price-input" : "#min-hourly-rate-input");

            await selectedCheckbox.CheckAsync();
            if (await minInput.IsVisibleAsync())
                await otherInput.FillAsync("0");
            await otherCheckbox.UncheckAsync();
            await minInput.WaitForAsync(Visible(10000));
            await minInput.FillAsync(
                (isHourly ? AppSession.Current.HourlyRate : AppSession.Current.FixedPrice).ToString());

            await ApplyJobAsync(page);
        }

        public async Task ApplyJobAsync(IPage page)
        {
            var searchUrl = page.Url;
            var titles = page.Locator("h2.Title-text");
            await titles.First.WaitForAsync(Visible());

            var jobUrls = new List<string>();
            var count = await titles.CountAsync();
            for (var i = 0; i < count; i++)
            {
                var href = await titles.Nth(i)
                    .Locator("xpath=ancestor::a[1]")
                    .GetAttributeAsync("href");

                if (string.IsNullOrEmpty(href))
                    continue;

                var url = href.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                    ? href
                    : Site + href;

                if (Applied.Current.AppliedJobs.Contains(url, StringComparer.OrdinalIgnoreCase))
                    continue;

                jobUrls.Add(url);
            }

            foreach (var jobUrl in jobUrls)
            {
                try
                {
                    await page.GotoAsync(jobUrl);

                    var restricted = page.GetByText("Bidding on this project is restricted");
                    var proposal = page.Locator("#descriptionTextArea");
                    var tokenexpired = page.GetByText("It looks like you're out of bids. Get more bids now, by purchasing a Bid Pack or upgrading your Membership.");

                    await restricted.Or(proposal).Or(tokenexpired).First.WaitForAsync(Visible(10000));

                    if (await tokenexpired.IsVisibleAsync())
                    {
                        await BrowserDispose();
                        MessageBox.Show("Freelancer başvuru hakkınız bitmiştir.");
                        break;
                    }

                    if (await restricted.IsVisibleAsync())
                    {
                        Applied.Apply(jobUrl);
                        continue;
                    }

                    if (!await proposal.IsVisibleAsync())
                    {
                        Applied.Apply(jobUrl);
                        continue;
                    }

                    await proposal.FillAsync(AppSession.Current.BasvuruMetni);
                    var placeBid = page.GetByRole(AriaRole.Button, new() { Name = "Place Bid" })
                        .Or(page.GetByRole(AriaRole.Button, new() { Name = "Teklif Ver" }));
                    await placeBid.ClickAsync();
                    Applied.Apply(jobUrl);

                    await page.GotoAsync(searchUrl);
                    await titles.First.WaitForAsync(Visible());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Başvuru hatası");
                }
            }
        }

        private static LocatorWaitForOptions Visible(float timeout = 15000) => new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = timeout
        };
    }
}
