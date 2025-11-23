using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using E2E.Models;
using E2E.Pages;
using E2E.TestData;

namespace E2E.Tests
{
    [AllureNUnit]
    public class CartTests : BaseTest
    {
        [Test, TestCaseSource(typeof(ItemsData), nameof(ItemsData.AllItems))]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Cart Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Check Cart")]

        public async Task CheckCartItemCount(Product item)
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var itemName = item.Name;
            var itemDescription = item.Description;
            var itemPrice = item.Price;
            var itemsCount = 1;

            //Act
            await AllureApi.Step("Login by standard user", async () =>
            {
                await LoginAsStandardUser();
            });

            await AllureApi.Step($"Add item '{itemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            });

            await AllureApi.Step("Navigate to Cart Page", async () =>
            {
                await itemsPage.GoToCart();
            });

            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert
            await AllureApi.Step("Check Item Count in Cart", async () =>
            {
                Assert.That(await cartPage.GetCartItemCount(), Is.EqualTo(itemsCount), "Cart item count should be 1 after adding an item.");
            });

            await AllureApi.Step("Verify items count", async () =>
            {
                Assert.That(await itemsPage.GetItemsCount(), Is.EqualTo(itemsCount), "There should be at least one item on the items page.");
            });

            await AllureApi.Step("Verify item name", async () =>
            {
                Assert.That(itemNames, Does.Contain(itemName), $"Item names should contain '{itemName}'.");
            });

            await AllureApi.Step("Verify item description", async () =>
            {
                Assert.That(itemDescriptions, Does.Contain(itemDescription), $"Item descriptions should contain '{itemDescription}'.");
            });

            await AllureApi.Step("Verify item price", async () =>
            {
                Assert.That(itemPrices, Does.Contain(itemPrice), $"Item prices should contain '{itemPrice}'.");
            });
        }

        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Cart Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Product List Actions")]

        public async Task AddItemToCartFromProductPage()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var item = ItemsData.Backpack;
            var itemName = item.Name;
            var secondItem = ItemsData.Jacket;
            var secondItemName = secondItem.Name;
            var seconditemDescription = secondItem.Description;
            var seconditemPrice = secondItem.Price;
            var itemsCount = 2;

            await AllureApi.Step("Login by standard user", async () =>
            {
                await LoginAsStandardUser();
            });

            await AllureApi.Step($"Add item '{itemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            });

            await AllureApi.Step("Navigate to Cart Page", async () =>
            {
                await itemsPage.GoToCart();
            });

            //Act
            await AllureApi.Step("Navigate back to items page", async () =>
            {
                await cartPage.ContinueShopping();
            });

            await AllureApi.Step($"Add item '{secondItemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(secondItemName);
            });

            await AllureApi.Step("Navigate to Cart Page", async () =>
            {
                await itemsPage.GoToCart();
            });

            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert
            await AllureApi.Step("Check Item Count in Cart", async () =>
            {
                Assert.That(await cartPage.GetCartItemCount(), Is.EqualTo(itemsCount), "Cart item count should be 2 after adding an item.");
            });

            await AllureApi.Step("Verify items count", async () =>
            {
                Assert.That(await itemsPage.GetItemsCount(), Is.EqualTo(itemsCount), "There should be at least two items on the items page.");
            });

            await AllureApi.Step("Verify item name", async () =>
            {
                Assert.That(itemNames, Does.Contain(itemName), $"Item names should contain '{itemName}'.");
            });

            await AllureApi.Step("Verify item description", async () =>
            {
                Assert.That(itemDescriptions, Does.Contain(seconditemDescription), $"Item descriptions should contain '{seconditemDescription}'.");
            });

            await AllureApi.Step("Verify item price", async () =>
            {
                Assert.That(itemPrices, Does.Contain(seconditemPrice), $"Item prices should contain '{seconditemPrice}'.");
            });
        }


        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Cart Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Product List Actions")]

        public async Task RemoveItemToCartFromProductPage()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var item = ItemsData.Backpack;
            var itemName = item.Name;
            var secondItem = ItemsData.Jacket;
            var secondItemName = secondItem.Name;
            var thirdItem = ItemsData.Onesie;
            var thirdItemName = thirdItem.Name;
            var itemsCount = 2;

            await AllureApi.Step("Login by standard user", async () =>
            {
                await LoginAsStandardUser();
            });

            await AllureApi.Step($"Add item '{itemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            });

            await AllureApi.Step($"Add item '{secondItemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(secondItemName);
            });

            await AllureApi.Step($"Add item '{thirdItemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(thirdItemName);
            });

            await AllureApi.Step("Navigate to Cart Page", async () =>
            {
                await itemsPage.GoToCart();
            });

            //Act
            await AllureApi.Step($"Remove item '{secondItemName}' from cart", async () =>
            {
                await cartPage.RemoveItemByName(secondItemName);
            });

            //Assert
            await AllureApi.Step("Check Item Count in Cart", async () =>
            {
                Assert.That(await cartPage.GetCartItemCount(), Is.EqualTo(itemsCount), "Cart item count should be 2 after adding an item.");
            });

            await AllureApi.Step("Verify items count", async () =>
            {
                Assert.That(await itemsPage.GetItemsCount(), Is.EqualTo(itemsCount), "There should be at least two items on the items page.");
            });
        }
    }
}
