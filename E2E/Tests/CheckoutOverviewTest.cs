using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using E2E.Pages;
using E2E.TestData;

namespace E2E.Tests
{
    [AllureNUnit]
    public class CheckoutOverviewTest : BaseTest
    {
        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Order Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Order Overview")]

        public async Task CheckCheckoutOverviewPage()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var checkoutPage = new CheckoutPage(Page);
            var checkoutOverviewPage = new CheckoutOverviewPage(Page);
            var item = ItemsData.Backpack;
            var itemName = item.Name;
            var itemDescription = item.Description;
            var itemPrice = item.Price;
            var client = ClientsData.JohnTester;
            var firstName = client.FirstName;
            var lastName = client.LastName;
            var zip = client.ZipOrPostalCode;

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

            await AllureApi.Step("Checkout order", async () =>
            {
                await cartPage.Checkout();
            });

            await AllureApi.Step("Complete client information and confirm", async () =>
            {
                await checkoutPage.CompleteCheckoutPage(firstName, lastName, zip);
            });

            var itemNames = await itemsPage.GetItemNames();
            var itemDescriptions = await itemsPage.GetItemDescriptions();
            var itemPrices = await itemsPage.GetItemPrices();

            //Assert
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

            await AllureApi.Step("Verify payment info label", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetPaymentInfoLabel(), Is.EqualTo("Payment Information:"));
            });

            await AllureApi.Step("Verify payment info value", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetPaymentInfoValue(), Is.EqualTo("SauceCard #31337"));
            });

            await AllureApi.Step("Verify shipping info label", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetShippingLabel(), Is.EqualTo("Shipping Information:"));
            });

            await AllureApi.Step("Verify shipping info value", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetShippingInfoValue(), Is.EqualTo("Free Pony Express Delivery!"));
            });

            await AllureApi.Step("Verify price total label", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetPriceTotalLabel(), Is.EqualTo("Price Total"));
            });

            await AllureApi.Step("Verify item total", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetItemTotal(), Is.EqualTo("Item total: $29.99"));
            });

            await AllureApi.Step("Verify tax", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetTax(), Is.EqualTo("Tax: $2.40"));
            });

            await AllureApi.Step("Verify total price", async () =>
            {
                Assert.That(await checkoutOverviewPage.GetTotal(), Is.EqualTo("Total: $32.39"));
            });

        }
    }
}
