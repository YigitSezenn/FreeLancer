using FreeLancer.Settings;
using Microsoft.Playwright;

namespace FreeLancer.Playwright
{
    internal class Login : PlaywrightBase
    {
        public async Task FreeLancerLogin(IPage page)
        {
            await page.GotoAsync("https://www.tr.freelancer.com/dashboard");
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            var emailInput = page.Locator("#emailOrUsernameInput");
            var passwordInput = page.Locator("#passwordInput");
            var browse = page.GetByRole(AriaRole.Button, new() { Name = "Browse" })
                .Or(page.GetByRole(AriaRole.Button, new() { Name = "Göz Atın" }))
                .First;

            await emailInput.Or(browse).WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 20000
            });

            var onDashboard = page.Url.Contains("/dashboard", StringComparison.OrdinalIgnoreCase)
                && !page.Url.Contains("/login", StringComparison.OrdinalIgnoreCase)
                && await browse.IsVisibleAsync();

            if (!onDashboard)
            {
                await emailInput.WaitForAsync(new() { State = WaitForSelectorState.Visible });
                await passwordInput.WaitForAsync(new() { State = WaitForSelectorState.Visible });
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
                await loginButton.WaitForAsync(new() { State = WaitForSelectorState.Visible });
                await loginButton.ClickAsync();

                var dialog = MessageBox.Show("CAPTCHA'yı tarayıcıda tamamladın mı?", "Giriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes)
                {
                    await BrowserDispose();
                    return;
                }

                await page.WaitForTimeoutAsync(3000);
            }

            await browse.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 20000
            });
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
            await viewAll.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 20000
            });
            await viewAll.ClickAsync();
            var isHourly = (AppSession.Current.WorkType ?? "")
                .Contains("Hourly", StringComparison.OrdinalIgnoreCase);
            var selectedCheckbox = page.Locator("label.CheckboxLabel", new() { HasText = isHourly ? "Hourly Rate" : "Fixed Price" });
            var otherCheckbox = page.Locator("label.CheckboxLabel", new() { HasText = isHourly ? "Fixed Price" : "Hourly Rate" });
            await selectedCheckbox.CheckAsync();
            await otherCheckbox.UncheckAsync();

            var minInput = page.Locator(isHourly ? "#min-hourly-rate-input" : "#min-fixed-price-input");
            await minInput.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });
            await minInput.ClearAsync();
            await minInput.FillAsync(
                (isHourly ? AppSession.Current.HourlyRate : AppSession.Current.FixedPrice).ToString());
        }

       
    }
}
