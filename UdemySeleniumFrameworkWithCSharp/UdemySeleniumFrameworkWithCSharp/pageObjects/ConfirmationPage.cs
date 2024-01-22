using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemySeleniumFrameworkWithCSharp.pageObjects
{
    public class ConfirmationPage
    {
        private IWebDriver driver;

        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countrySelector;

        [FindsBy(How = How.TagName, Using = "label")]
        private IWebElement acceptTermsAndConditions;

        [FindsBy(How = How.CssSelector, Using = ".btn.btn-lg")]
        private IWebElement btn_Purchase;

        [FindsBy(How = How.CssSelector, Using = ".alert.alert-success")]
        private IWebElement confirmationMsgField;

        By nameOfCountry = By.LinkText("United Kingdom");

        public void EnterCountryName(string country)
        {
            countrySelector.SendKeys(country);
        }

         public void clickOnAcceptTermsAndConditions()
        {
            acceptTermsAndConditions.Click();
        }

        public void clickOnPurchaseBtn()
        {
            btn_Purchase.Click();
        }

        public string getConfirmationMsg()
        {
            return confirmationMsgField.Text;
        }

    }
}
