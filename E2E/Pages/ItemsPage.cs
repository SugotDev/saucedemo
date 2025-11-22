using Microsoft.Playwright;

namespace E2E.Pages
{
    public class ItemsPage
    {
        private readonly IPage _page;
        private ILocator ItemImage => _page.Locator(".inventory_item_img");
        private ILocator ItemNames => _page.Locator(".inventory_item_name");
        private ILocator ItemDescriptions => _page.Locator(".inventory_item_desc");
        private ILocator ItemPrices => _page.Locator(".inventory_item_price");
        private ILocator ProductCards => _page.Locator(".inventory_item");
        private ILocator ShoppingCartLink => _page.Locator(".shopping_cart_badge");
        public ItemsPage(IPage page)
        {
            _page = page;
        }

        public async Task GoToCart()
        {
            await _page.Locator(".shopping_cart_link").ClickAsync();
        }
        public async Task<int> GetItemsCount()
        {
            return await ItemNames.CountAsync();
        }
        public async Task GetItemImage (string src)
        {
            var count = await ItemImage.CountAsync();
            for (int i = 0; i < count; i++)
            {
                var imageSrc = await ItemImage.Nth(i).GetAttributeAsync("src");
                if (imageSrc != null && imageSrc.Contains(src, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            throw new Exception($"Item with image source '{src}' not found.");
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
        public async Task AddItemToCartByName(string itemName)
        {
            int count = await ProductCards.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var card = ProductCards.Nth(i);
                var name = await card.Locator(".inventory_item_name").InnerTextAsync();

                if (name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    var button = card.Locator("button");
                    var buttonText = await button.InnerTextAsync();

                    if (buttonText.Equals("Add to cart", StringComparison.OrdinalIgnoreCase))
                    {
                        await button.ClickAsync();
                    }

                    return;
                }
            }

            throw new Exception($"Item '{itemName}' not found.");
        }

        public async Task RemoveItemFromCartByName(string itemName)
        {
            int count = await ProductCards.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var card = ProductCards.Nth(i);
                var name = await card.Locator(".inventory_item_name").InnerTextAsync();

                if (name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    var button = card.Locator("button");

                    await button.ClickAsync();
                    return;
                }
            }

            throw new Exception($"Item with name '{itemName}' not found.");
        }

        public async Task<int> GetCartItemCount()
        {
            var isVisible = await ShoppingCartLink.IsVisibleAsync();
            if (!isVisible)
            {
                return 0;
            }
            var itemCountText = await ShoppingCartLink.InnerTextAsync();
            if (int.TryParse(itemCountText, out int itemCount))
            {
                return itemCount;
            }
            return 0;
        }
    }
}
