using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using E2E.Pages;

namespace E2E.Tests
{
    [AllureNUnit]
    public class LoginTests : BaseTest
    {
        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Login Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Login Feature")]
        public async Task SuccessfulLoginUser()
        {
            //Arrange
            var loginPage = new LoginPage(Page);
            var username = _config["Credentials:Username"];
            var password = _config["Credentials:Password"];
            var baseUrl = _config["BaseUrl"];

            // Act
            await AllureApi.Step("Navigate to base URL", async () =>
            {
                await Page.GotoAsync(baseUrl);
            });

            await AllureApi.Step("Login by standard user", async () =>
            {
                await loginPage.Login(username, password);
            });

            // Assert
            await AllureApi.Step("Verify page title", async () =>
            {
                Assert.That(await Page.TitleAsync(), Is.EqualTo("Swag Labs"));
            });
        }

        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Login Tests")]
        [AllureSubSuite("Negative Tests")]
        [AllureFeature("Login Feature")]
        public async Task UnsuccessfulLoginUser()
        {
            //Arrange
            var loginPage = new LoginPage(Page);
            var username = _config["Credentials:Username"];
            var password = "wrong_password";
            var baseUrl = _config["BaseUrl"];
            var expectedErrorMessage = "Epic sadface: Username and password do not match any user in this service";

            // Act
            await AllureApi.Step("Navigate to base URL", async () =>
            {
                await Page.GotoAsync(baseUrl);
            });

            await AllureApi.Step("Login by standard user", async () =>
            {
                await loginPage.Login(username, password);
            });

            // Assert
            await AllureApi.Step("Verify error message", async () =>
            {
                var errorMessage = await loginPage.GetErrorMessage();
                Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
            });
        }
    }
}
