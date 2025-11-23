using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using E2E.Pages;
using E2E.TestData;

namespace E2E.Tests
{
    [AllureNUnit]
    public class OrderTests : BaseTest
    {
        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Order Tests")]
        [AllureSubSuite("Positive Tests")]
        [AllureFeature("Order End To End")]

        public async Task PlaceOrderSuccessful()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var checkoutPage = new CheckoutPage(Page);
            var checkoutOverviewPage = new CheckoutOverviewPage(Page);
            var checkoutComplete = new CheckoutCompletePage(Page);
            var item = ItemsData.Backpack;
            var itemName = item.Name;
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

            await AllureApi.Step("Finish Order", async () =>
            {
                await checkoutOverviewPage.FinishOrder();
            });

            //Assert
            await AllureApi.Step("Verify complete header", async () =>
            {
                Assert.That(await checkoutComplete.GetPonyExpress(), Does.StartWith("data:image/png"));
            });

            await AllureApi.Step("Verify complete header", async () =>
            {
                Assert.That(await checkoutComplete.GetCompleteHeader(), Is.EqualTo("Thank you for your order!"));
            });

            await AllureApi.Step("Verify complete text", async () =>
            {
                Assert.That(await checkoutComplete.GetCompleteText(), Is.EqualTo("Your order has been dispatched, and will arrive just as fast as the pony can get there!"));
            });

        }

        [Test]
        [AllureTag("E2E")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Michał Sługocki")]
        [AllureSuite("Order Tests")]
        [AllureSubSuite("Negative Tests")]
        [AllureFeature("Order End To End")]

        public async Task PlaceOrderUnsuccessful()
        {
            //Arrange
            var itemsPage = new ItemsPage(Page);
            var cartPage = new CartPage(Page);
            var checkoutPage = new CheckoutPage(Page);
            var checkoutOverviewPage = new CheckoutOverviewPage(Page);
            var checkoutComplete = new CheckoutCompletePage(Page);
            var item = ItemsData.Backpack;
            var itemName = item.Name;
            var firstName = "";
            var lastName = "";
            var zip = "";
            var expectedErrorMessage = "Error: First Name is required";

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

            //Assert
            await AllureApi.Step("Verify error message", async () =>
            {
                var errorMessage = await checkoutPage.GetErrorMessage();
                Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
            });

        }
    }
}
