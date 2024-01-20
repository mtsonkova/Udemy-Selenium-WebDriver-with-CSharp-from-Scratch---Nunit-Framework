using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemySeleniumFrameworkWithCSharp.pageObjects
{
    public class CheckoutPage
    {
       private IWebDriver driver;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> products;

        [FindsBy(How = How.CssSelector, Using = ".btn.btn-success")]
        private IWebElement btn_checkout;

        public IList<IWebElement> getProductsInCart()
        {
            return products;
        }

        public ConfirmationPage clickCleckoutBtnInCart()
        {
            btn_checkout.Click();
            return new ConfirmationPage(driver);
        }
    }
}
