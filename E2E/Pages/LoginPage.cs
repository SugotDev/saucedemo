using Microsoft.Playwright;

namespace E2E.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        private ILocator UsernameInput => _page.Locator("#user-name");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("#login-button");
        private ILocator ErrorMessage => _page.Locator("[data-test='error']");

        public LoginPage(IPage page)
        {
            _page = page;
        }

         public async Task Login(string username, string password)
        {
            await UsernameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task<string> GetErrorMessage()
        {
            return await ErrorMessage.InnerTextAsync();
        }
    }
}
