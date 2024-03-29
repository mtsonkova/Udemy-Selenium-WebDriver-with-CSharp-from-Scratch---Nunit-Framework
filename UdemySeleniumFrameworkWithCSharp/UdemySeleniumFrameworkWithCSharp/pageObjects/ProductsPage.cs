﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace UdemySeleniumFrameworkWithCSharp.pageObjects
{
    public class ProductsPage
    {
        private IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");
        By addToCart = By.CssSelector(".card-footer button");
       // By productsCheckoutButton = By.PartialLinkText("Checkout");


        public ProductsPage(IWebDriver driver) {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> products;


        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement btn_checkout;

       public IList<IWebElement> getAllProducts()
        {
            return products;
        }

        public By getCardTitle()
        {
            return cardTitle;
        }

        public By addToCartButton()
        {
            return addToCart;
        }

        public CheckoutPage clickOnCheckOutBtn()
        {
            btn_checkout.Click();
            return new CheckoutPage(driver);
        }
    }
}
