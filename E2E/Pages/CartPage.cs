using Microsoft.Playwright;

namespace E2E.Pages
{
    public class CartPage
    {
        private readonly IPage _page;
        private ILocator CartItems => _page.Locator(".cart_item");
        private ILocator ItemNames => _page.Locator(".inventory_item_name");
        private ILocator ItemDescriptions => _page.Locator(".inventory_item_desc");
        private ILocator ItemPrices => _page.Locator(".inventory_item_price");
        private ILocator ContinueShoppingButton => _page.Locator("#continue-shopping");
        private ILocator CheckoutButton => _page.Locator("#checkout");
        private ILocator RemoveButtons => _page.Locator("button:text('Remove')");

        public CartPage(IPage page)
        {
            _page = page;
        }

        public async Task<int> GetCartItemCount()
        {
            return await CartItems.CountAsync();
        }

        public async Task<List<string>> GetItemNames()
        {
            var names = new List<string>();
            var count = await ItemNames.CountAsync();
            for (int i = 0; i < count; i++)
            {
                names.Add(await ItemNames.Nth(i).InnerTextAsync());
            }
            return names;
        }

        public async Task<List<string>> GetItemDescriptions()
        {
            var descriptions = new List<string>();
            var count = await ItemDescriptions.CountAsync();
            for (int i = 0; i < count; i++)
            {
                descriptions.Add(await ItemDescriptions.Nth(i).InnerTextAsync());
            }
            return descriptions;
        }

        public async Task<List<string>> GetItemPrices()
        {
            var prices = new List<string>();
            var count = await ItemPrices.CountAsync();
            for (int i = 0; i < count; i++)
            {
                prices.Add(await ItemPrices.Nth(i).InnerTextAsync());
            }
            return prices;
        }

        public async Task ContinueShopping()
        {
            await ContinueShoppingButton.ClickAsync();
        }

        public async Task Checkout()
        {
            await CheckoutButton.ClickAsync();
        }

        public async Task RemoveItemByName(string itemName)
        {
            var count = await ItemNames.CountAsync();
            for (int i = 0; i < count; i++)
            {
                var name = await ItemNames.Nth(i).InnerTextAsync();
                if (name == itemName)
                {
                    await RemoveButtons.Nth(i).ClickAsync();
                    break;
                }
            }
        }
    }
}
