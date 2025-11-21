using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace E2E
{
    [SetUpFixture]
    public class PlaywrightGlobalSetup
    {
        public static IPlaywright PlaywrightInstance;
        public static IBrowser Browser;

        private IConfiguration _config;


        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            _config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

            bool headlessMode = bool.Parse(_config["PlaywrightSettings:Headless"]);

            PlaywrightInstance = await Playwright.CreateAsync();
            Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headlessMode
            });
        }

        [OneTimeTearDown]
        public async Task GlobalTeardown()
        {
            await Browser.CloseAsync();
            PlaywrightInstance.Dispose();
        }
    }
}
