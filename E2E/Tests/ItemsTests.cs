using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using E2E.Pages;
using NUnit.Allure.Core;

namespace E2E.Tests
{
    [AllureNUnit]
    [Obsolete]
    public class ItemsTests : BaseTest
    {
        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Items Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Add To Cart")]
        public async Task AddItemToCart()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var itemName = "Sauce Labs Backpack";

            //Act
            await _allure.WrapInStepAsync(async () =>
            {
                await LoginAsStandardUser();
            }, "Login by standard user");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            }, $"Add item '{itemName}' to cart");


            //Assert
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await itemsPage.GetCartItemCount(), Is.EqualTo(1), "Cart item count should be 1 after adding an item.");
            }, "Check Item Count in Cart");

        }

        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Items Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Remove From Cart")]
        public async Task RemoveItemFromCart()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var itemName = "Sauce Labs Backpack";

            //Act
            await _allure.WrapInStepAsync(async () =>
            {
                await LoginAsStandardUser();
            }, "Login by standard user");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.AddItemToCartByName(itemName);
            }, $"Add item '{itemName}' to cart");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.RemoveItemFromCartByName(itemName);
            }, $"Remove item '{itemName}' from cart");

            //Assert
            Assert.That(await itemsPage.GetCartItemCount(), Is.EqualTo(0), "Cart item count should be 0 after removing the item.");
        }

        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Items Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Check Item Details")]
        public async Task CheckItemDetails()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var itemName = "Sauce Labs Backpack";
            var itemDescription = "carry.allTheThings() with the sleek, streamlined Sly Pack that melds uncompromising style with unequaled laptop and tablet protection.";
            var itemPrice = "$29.99";
            var itemImageSrc = "/static/media/sauce-backpack-1200x1500";
            var itemsCount = 6;
            //Act
            await _allure.WrapInStepAsync(async () =>
            {
                await LoginAsStandardUser();
            }, "Login by standard user");
            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await itemsPage.GetItemsCount(), Is.EqualTo(itemsCount), "There should be at least one item on the items page.");
            }, "Verify items count");
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(itemNames, Does.Contain(itemName), $"Item names should contain '{itemName}'.");
            }, "Verify item name");
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(itemDescriptions, Does.Contain(itemDescription), $"Item descriptions should contain '{itemDescription}'.");
            }, "Verify item description");
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(itemPrices, Does.Contain(itemPrice), $"Item prices should contain '{itemPrice}'.");
            }, "Verify item price");
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.DoesNotThrowAsync(async () => await itemsPage.GetItemImage(itemImageSrc), $"Item with image source '{itemImageSrc}' should be present.");
            }, "Verify item image");


        }
    }
}
