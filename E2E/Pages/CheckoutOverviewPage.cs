using Microsoft.Playwright;

namespace E2E.Pages
{
    public class CheckoutOverviewPage
    {
        private readonly IPage _page;
        private ILocator PaymentInfoLabel => _page.Locator("[data-test='payment-info-label']");
        private ILocator PaymentInfoValue => _page.Locator("[data-test='payment-info-value']");
        private ILocator ShippingInfoLabel => _page.Locator("[data-test='shipping-info-label']");
        private ILocator ShippingInfoValue => _page.Locator("[data-test='shipping-info-value']");
        private ILocator PriceTotalLabel => _page.Locator("[data-test='total-info-label']");
        private ILocator ItemTotalLabel => _page.Locator("[data-test='subtotal-label']");
        private ILocator TaxLabel => _page.Locator("[data-test='tax-label']");
        private ILocator TotalLabel => _page.Locator("[data-test='total-label']");
        private ILocator FinishButton => _page.Locator("#finish");
        private ILocator CanncelButton => _page.Locator("#cancel");

        public CheckoutOverviewPage(IPage page)
        {
            _page = page;
        }
        public async Task<string> GetPaymentInfoLabel() => await PaymentInfoLabel.InnerTextAsync();
        public async Task<string> GetPaymentInfoValue() => await PaymentInfoValue.InnerTextAsync();        
        public async Task<string> GetShippingLabel() => await ShippingInfoLabel.InnerTextAsync();
        public async Task<string> GetShippingInfoValue() => await ShippingInfoValue.InnerTextAsync();
        public async Task<string> GetPriceTotalLabel() => await PriceTotalLabel.InnerTextAsync();
        public async Task<string> GetItemTotal() => await ItemTotalLabel.InnerTextAsync();
        public async Task<string> GetTax() => await TaxLabel.InnerTextAsync();
        public async Task<string> GetTotal() => await TotalLabel.InnerTextAsync();
        public async Task FinishOrder()
        {
            await FinishButton.ClickAsync();
        }
    }
}
