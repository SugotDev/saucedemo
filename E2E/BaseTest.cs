using Allure.Net.Commons;
using E2E.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace E2E
{
    public class BaseTest : PageTest
    {
        protected IBrowserContext Context;
        protected IPage Page;
        protected IConfiguration _config;
        protected AllureLifecycle _allure;

        [SetUp]
        public async Task Setup()
        {
            _allure = AllureLifecycle.Instance;
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Context = await PlaywrightGlobalSetup.Browser.NewContextAsync();

            await Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            Page = await Context.NewPageAsync();

        }
        protected async Task LoginAsStandardUser()
        {
            var loginPage = new LoginPage(Page);
            var username = _config["Credentials:Username"];
            var password = _config["Credentials:Password"];
            var baseUrl = _config["BaseUrl"];

            await Page.GotoAsync(baseUrl);
            await loginPage.Login(username, password);
        }

        [TearDown]
        public async Task Teardown()
        {
            var testName = TestContext.CurrentContext.Test.Name;

            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshotPath = Path.Combine("screenshots", $"{testName}.png");
                Directory.CreateDirectory("screenshots");

                await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

                AllureApi.AddAttachment($"{testName}_screenshot", "image/png", screenshotPath);
            }

            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = $"traces/{TestContext.CurrentContext.Test.Name}.zip"
            });

            AllureApi.AddAttachment($"{TestContext.CurrentContext.Test.Name}_trace", "application/zip", $"traces/{TestContext.CurrentContext.Test.Name}.zip");

            await Context.CloseAsync();
        }
    }

}
