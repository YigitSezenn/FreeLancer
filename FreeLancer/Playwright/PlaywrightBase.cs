using ManagedCode.Playwright.Stealth;
using Microsoft.Playwright;
namespace FreeLancer.Playwright
{
    internal class PlaywrightBase
    {
        public IPlaywright? _playwright;
        public IPage? _page;
        private IBrowserContext? _context;

        private static readonly string UserDataDir = Path.Combine(
       Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
       "FreeLancer", "chrome-profile");
        public async Task<IPage> Browser()
        {
            if (_page is not null && !_page.IsClosed)
                return _page;

            Directory.CreateDirectory(UserDataDir);

            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            _context = await _playwright.Chromium.LaunchPersistentContextAsync(UserDataDir, new()
            {
                Headless = false,
                 Channel = "chrome",
                ViewportSize = ViewportSize.NoViewport,
                Args =
                [
                    ..PlaywrightStealthExtensions.StealthArgs,
                    "--start-maximized",    
                    "--disable-notifications"
                ]
            });

            await _context.ApplyStealthAsync();

            _page = await _context.NewPageAsync();
            

            return _page;
        }

        public async Task BrowserDispose()
        {
            if (_context is not null)
                await _context.CloseAsync();

            _playwright?.Dispose();
            _page = null;
            _context = null;
            _playwright = null;

        }
    }
}
