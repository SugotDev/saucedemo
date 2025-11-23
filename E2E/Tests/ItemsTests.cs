using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using E2E.Models;
using E2E.Pages;
using E2E.TestData;

namespace E2E.Tests
{
    [AllureNUnit]
    public class ItemsTests : BaseTest
    {
        [Test, TestCaseSource(typeof(ItemsData), nameof(ItemsData.AllItems))]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Items Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Add To Cart")]
        public async Task AddItemToCart(Product item)
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var itemName = item.Name;

            //Act
            await AllureApi.Step("Login by standard user", async () =>
            {
                await LoginAsStandardUser();
            });

            await AllureApi.Step($"Add item '{itemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            });

            //Assert
            await AllureApi.Step("Check Item Count in Cart", async () =>
            {
                Assert.That(await itemsPage.GetCartItemCount(), Is.EqualTo(1), "Cart item count should be 1 after adding an item.");
            });

        }

        [Test, TestCaseSource(typeof(ItemsData), nameof(ItemsData.AllItems))]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Items Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Remove From Cart")]
        public async Task RemoveItemFromCart(Product item)
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var itemName = item.Name;

            //Act
            await AllureApi.Step("Login by standard user", async () =>
            {
                await LoginAsStandardUser();
            });

            await AllureApi.Step($"Add item '{itemName}' to cart", async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            });

            await AllureApi.Step($"Remove item '{itemName}' from cart", async () =>
            {
                await itemsPage.RemoveItemFromCartByName(itemName);
            });

            //Assert
            await AllureApi.Step("Verify item count", async () =>
            {
                Assert.That(await itemsPage.GetCartItemCount(), Is.EqualTo(0), "Cart item count should be 0 after removing the item.");
            });
        }

        [Test, TestCaseSource(typeof(ItemsData), nameof(ItemsData.AllItems))]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Items Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Check Item Details")]
        public async Task CheckItemDetails(Product item)
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var itemName = item.Name;
            var itemDescription = item.Description;
            var itemPrice = item.Price;
            var itemImageSrc = item.ImageSrc;
            var itemsCount = 6;

            //Act
            await AllureApi.Step("Login by standard user", async () =>
            {
                await LoginAsStandardUser();
            });

            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert

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

            await AllureApi.Step("Verify item image", async () =>
            {
                Assert.DoesNotThrowAsync(async () => await itemsPage.GetItemImage(itemImageSrc), $"Item with image source '{itemImageSrc}' should be present.");
            });
        }
    }
}
