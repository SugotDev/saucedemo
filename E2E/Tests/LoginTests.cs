using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using E2E.Pages;
using NUnit.Allure.Core;

namespace E2E.Tests
{
    [AllureNUnit]
    [Obsolete]
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
            await _allure.WrapInStepAsync(async () =>
            {
                await Page.GotoAsync(baseUrl);
            }, "Navigate to base URL");

            await _allure.WrapInStepAsync(async () =>
            {
                await loginPage.Login(username, password);
            }, "Login by standard user");

            // Assert
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await Page.TitleAsync(), Is.EqualTo("Swag Labs"));
            }, "Verify page title");
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
            await _allure.WrapInStepAsync(async () =>
            {
                await Page.GotoAsync(baseUrl);
            }, "Navigate to base URL");

            await _allure.WrapInStepAsync(async () =>
            {
                await loginPage.Login(username, password);
            }, "Login by standard user");

            // Assert
            await _allure.WrapInStepAsync(async () =>
            {
                var errorMessage = await loginPage.GetErrorMessage();
                Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
            }, "Verify error message");
        }
    }
}
