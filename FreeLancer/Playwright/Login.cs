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

            if (!await IsOnDashboardAsync(page))
            {
                if (!await SignInAsync(page))
                    return;
            }

            await new JobSearch().RunAsync(page);
        }

        private static async Task<bool> IsOnDashboardAsync(IPage page)
        {
            var browse = BrowseButton(page);
            await page.Locator("#emailOrUsernameInput").Or(browse).WaitForAsync(Visible(20000));

            return page.Url.Contains("/dashboard", StringComparison.OrdinalIgnoreCase)
                && !page.Url.Contains("/login", StringComparison.OrdinalIgnoreCase)
                && await browse.IsVisibleAsync();
        }

        private async Task<bool> SignInAsync(IPage page)
        {
            var email = page.Locator("#emailOrUsernameInput");
            var password = page.Locator("#passwordInput");
            await email.WaitForAsync(Visible());
            await password.WaitForAsync(Visible());

            await email.ClickAsync();
            await email.ClearAsync();
            await email.PressSequentiallyAsync(AppSession.Current.Email, new() { Delay = 100 });
            await password.ClickAsync();
            await password.ClearAsync();
            await password.PressSequentiallyAsync(AppSession.Current.Password, new() { Delay = 100 });
            await page.WaitForTimeoutAsync(1000);

            await page.GetByText("Remember me", new() { Exact = true })
                .Or(page.GetByText("Beni hatırla", new() { Exact = true }))
                .CheckAsync();

            var loginButton = page.GetByRole(AriaRole.Button, new() { Name = "Log in" })
                .Or(page.GetByRole(AriaRole.Button, new() { Name = "Giriş" }));
            await loginButton.WaitForAsync(Visible());
            await loginButton.ClickAsync();

            var captcha = MessageBox.Show(
                "CAPTCHA'yı tarayıcıda tamamladın mı?",
                "Giriş",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (captcha != DialogResult.Yes)
            {
                await BrowserDispose();
                return false;
            }

            await page.WaitForTimeoutAsync(3000);
            return true;
        }

        private static ILocator BrowseButton(IPage page) =>
            page.GetByRole(AriaRole.Button, new() { Name = "Browse" })
                .Or(page.GetByRole(AriaRole.Button, new() { Name = "Göz Atın" }))
                .First;

        private static LocatorWaitForOptions Visible(float timeout = 15000) => new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = timeout
        };
    }
}
