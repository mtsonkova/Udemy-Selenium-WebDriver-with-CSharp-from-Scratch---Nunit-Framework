using AngleSharp.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using UdemySeleniumFrameworkWithCSharp.pageObjects;
using UdemySeleniumFrameworkWithCSharp.utilities;


namespace UdemySeleniumFrameworkWithCSharp.tests
{

    public class Tests : Base
    {

        [Test]
        public void E2ETest()
        {

            string[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[expectedProducts.Count()];
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];
           
            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage products = loginPage.validLogin(userName, password);


            products.waitForPageDisplay();
            IList<IWebElement> productsInCatalogue = products.getAllProducts();

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

            driver.FindElement(By.Id("country")).SendKeys("United");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("United Kingdom")));
            driver.FindElement(By.LinkText("United Kingdom")).Click();

            //driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();
            driver.FindElement(By.TagName("label")).Click();
            driver.FindElement(By.CssSelector(".btn.btn-lg")).Click();

            // String expectedConfirmationText = "×\r\nSuccess! Thank you! Your order will be delivered in next few weeks :-).";
            string actualConfirmationText = driver.FindElement(By.CssSelector(".alert.alert-success")).Text;

            //Assert.AreEqual(expectedConfirmationText, actualConfirmationText);

            StringAssert.Contains("Success", actualConfirmationText);
        }
    }
}