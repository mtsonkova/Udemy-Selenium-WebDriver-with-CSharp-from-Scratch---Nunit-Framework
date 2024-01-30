using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemySeleniumFrameworkWithCSharp.pageObjects
{
    public class LoginPage
    {
       private IWebDriver driver;

        By errMsgPlaceholder = By.CssSelector("alert alert-danger");
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userName;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign In']")]
        private IWebElement btn_SignIn;

        [FindsBy(How = How.CssSelector, Using = "alert alert-danger")]
        private IWebElement errorMsgPlaceholder;

        public IWebElement getUserNameField()
        {
            return userName;
        }

        public IWebElement getPasswordField()
        {
            return password;
        }

        public IWebElement getBtnSignIn()
        {
            return btn_SignIn;
        }

        public ProductsPage validLogin(String username, String pass)
        {
            userName.SendKeys(username);
            password.SendKeys(pass);
            btn_SignIn.Click();
            return new ProductsPage(driver);
        }

        public void invalidLogin(String username, String pass)
        {
            userName.SendKeys(username);
            password.SendKeys(pass);
            btn_SignIn.Click();
          
        }

        public string getErrMsgText()
        {
            return errorMsgPlaceholder.Text;
        }

    }
}
