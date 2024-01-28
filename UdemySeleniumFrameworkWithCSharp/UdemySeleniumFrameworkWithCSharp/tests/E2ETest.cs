using AngleSharp.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using UdemySeleniumFrameworkWithCSharp.pageObjects;
using UdemySeleniumFrameworkWithCSharp.utilities;


namespace UdemySeleniumFrameworkWithCSharp.tests
{

    public class Tests : BaseTest
    {

        [Test, TestCaseSource("AddTestDataConfig")]
        public void E2ETest(string username, string password, string[] expectedProducts)
        {

            //string[] expectedProducts = expected_products;
            string[] actualProducts = new string[expectedProducts.Count()];
            string userName = username;
            string pass = password;

            By nameOfCountry = By.LinkText("United Kingdom");
            By productsCheckoutButton = By.PartialLinkText("Checkout");


            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage products = loginPage.validLogin(userName, password);


            WaitForElementToAppear(productsCheckoutButton);
            //products.waitForCheckoutBtnToAppearOnProductsPage(By.PartialLinkText("Checkout"));
            IList<IWebElement> productsInCatalogue = products.getAllProducts();
            Console.WriteLine(productsInCatalogue.Count());

            foreach (IWebElement product in productsInCatalogue)
            {

                string productTitle = product.FindElement(products.getCardTitle()).Text;

                if (expectedProducts.Contains(productTitle))
                {
                    IWebElement btn_Add = product.FindElement(products.addToCartButton());

                    btn_Add.Click();
                }

            }

            CheckoutPage checkoutPage = products.clickOnCheckOutBtn();

            IList<IWebElement> productsInCart = checkoutPage.getProductsInCart();

            for (int i = 0; i < productsInCart.Count; i++)
            {
                actualProducts[i] = productsInCart[i].Text;
            }
 
            Assert.AreEqual(expectedProducts, actualProducts);


            ConfirmationPage confirmationPage = checkoutPage.clickCleckoutBtnInCart();

            confirmationPage.EnterCountryName("united");
            WaitForElementToAppear(nameOfCountry).Click();
          
            confirmationPage.clickOnAcceptTermsAndConditions();
            confirmationPage.clickOnPurchaseBtn();
                  
            string actualConfirmationText = confirmationPage.getConfirmationMsg();

            StringAssert.Contains("Success", actualConfirmationText);
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"),
                                          getDataParser().extractData("password"),
                                          getDataParser().extractDataArray("products"));
           
            yield return new TestCaseData(getDataParser().extractData("username_wrong"),
                                          getDataParser().extractData("password_wrong"),
                                          getDataParser().extractDataArray("products"));

        }
    }
}