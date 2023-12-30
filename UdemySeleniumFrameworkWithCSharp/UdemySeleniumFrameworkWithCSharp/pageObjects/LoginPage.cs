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

        public void logIn(String username, String pass)
        {
            userName.SendKeys(username);
            password.SendKeys(pass);
            btn_SignIn.Click();
        }

    }
}
