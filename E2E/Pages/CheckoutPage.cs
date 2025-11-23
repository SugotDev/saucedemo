using Microsoft.Playwright;

namespace E2E.Pages
{
    public class CheckoutPage
    {
        private readonly IPage _page;
        private ILocator FirstNameInput => _page.Locator("#first-name");
        private ILocator LastNameInput => _page.Locator("#last-name");
        private ILocator ZipOrPostalCodeInput => _page.Locator("#postal-code");
        private ILocator ContinueButton => _page.Locator("#continue");
        private ILocator ErrorMessage => _page.Locator("[data-test='error']");

        public CheckoutPage(IPage page)
        {
            _page = page;
        }

        public async Task CompleteCheckoutPage(string firstName, string lastName, string zipOrPostalCode)
        {
            await FirstNameInput.FillAsync(firstName);
            await LastNameInput.FillAsync(lastName);
            await ZipOrPostalCodeInput.FillAsync(zipOrPostalCode);
            await ContinueButton.ClickAsync();
        }

        public async Task<string> GetErrorMessage()
        {
            return await ErrorMessage.InnerTextAsync();
        }
    }
}
