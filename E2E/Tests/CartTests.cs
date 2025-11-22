using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using E2E.Pages;
using E2E.TestData;
using NUnit.Allure.Core;

namespace E2E.Tests
{
    [AllureNUnit]
    [Obsolete]
    public class CartTests : BaseTest
    {
        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Cart Page Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Check Cart")]

        public async Task CheckCartItemCount()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var item = ItemsData.Backpack;
            var itemName = item.Name;
            var itemDescription = item.Description;
            var itemPrice = item.Price;
            var itemsCount = 1;

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
                await itemsPage.GoToCart();
            }, "Navigate to Cart Page");

            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await cartPage.GetCartItemCount(), Is.EqualTo(itemsCount), "Cart item count should be 1 after adding an item.");
            }, "Check Item Count in Cart");

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
                await itemsPage.GoToCart();
            }, "Navigate to Cart Page");

            //Act
            await _allure.WrapInStepAsync(async () =>
            {
                await cartPage.ContinueShopping();
            }, "Navigate back to items page");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.AddItemToCartByName(secondItemName);
            }, $"Add item '{secondItemName}' to cart");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.GoToCart();
            }, "Navigate to Cart Page");

            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await cartPage.GetCartItemCount(), Is.EqualTo(itemsCount), "Cart item count should be 2 after adding an item.");
            }, "Check Item Count in Cart");

            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await itemsPage.GetItemsCount(), Is.EqualTo(itemsCount), "There should be at least two items on the items page.");
            }, "Verify items count");

            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(itemNames, Does.Contain(itemName), $"Item names should contain '{itemName}'.");
            }, "Verify item name");

            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(itemDescriptions, Does.Contain(seconditemDescription), $"Item descriptions should contain '{seconditemDescription}'.");
            }, "Verify item description");

            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(itemPrices, Does.Contain(seconditemPrice), $"Item prices should contain '{seconditemPrice}'.");
            }, "Verify item price");
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
                await itemsPage.AddItemToCartByName(secondItemName);
            }, $"Add item '{secondItemName}' to cart");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.AddItemToCartByName(thirdItemName);
            }, $"Add item '{thirdItemName}' to cart");

            await _allure.WrapInStepAsync(async () =>
            {
                await itemsPage.GoToCart();
            }, "Navigate to Cart Page");

            //Act
            await _allure.WrapInStepAsync(async () =>
            {
                await cartPage.RemoveItemByName(secondItemName);
            }, $"Remove item '{secondItemName}' from cart");

            //Assert
            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await cartPage.GetCartItemCount(), Is.EqualTo(itemsCount), "Cart item count should be 2 after adding an item.");
            }, "Check Item Count in Cart");

            await _allure.WrapInStepAsync(async () =>
            {
                Assert.That(await itemsPage.GetItemsCount(), Is.EqualTo(itemsCount), "There should be at least two items on the items page.");
            }, "Verify items count");
        }
    }
}
