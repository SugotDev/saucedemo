using Microsoft.Playwright;

namespace E2E.Pages
{
    public class CheckoutCompletePage
    {
        private readonly IPage _page;
        private ILocator PonyExpress => _page.Locator(".pony_express");
        private ILocator CompleteHeader => _page.Locator(".complete-header");
        private ILocator CompleteText => _page.Locator(".complete-text");
        private ILocator BackHomeButton => _page.Locator("#back-to-products");

        public CheckoutCompletePage(IPage page)
        {
            _page = page;
        }

        public async Task<string> GetPonyExpress() => await PonyExpress.GetAttributeAsync("src");
        public async Task<string> GetCompleteHeader() => await CompleteHeader.InnerHTMLAsync();
        public async Task<string> GetCompleteText() => await CompleteText.InnerHTMLAsync();

        public async Task BackHome()
        {
            await BackHomeButton.ClickAsync();
        }

    }
}
