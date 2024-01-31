using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemySeleniumFrameworkWithCSharp.pageObjects;
using UdemySeleniumFrameworkWithCSharp.utilities;

namespace UdemySeleniumFrameworkWithCSharp.tests
{
    [Parallelizable(ParallelScope.Children)]
    public class LoginPageTests : BaseTest
    {
        [Test, TestCaseSource("AddTestDataConfig2")]
        public void LogInWithCorrectCredentials(string username, string password)
        {
            By productsCheckoutButton = By.PartialLinkText("Checkout");
            By errMsgPlaceholder = By.CssSelector(".alert.alert-danger");


            string userName = username;
            string pass = password;

            LoginPage loginPage = new LoginPage(getDriver());

            if(userName == "rahulshettyacademy")
            {
                loginPage.validLogin(userName, pass);
                WaitForElementToAppear(productsCheckoutButton);

                Assert.That(driver.Value.Url, Is.Not.EqualTo(ConfigurationManager.AppSettings["baseUrl"]));
            }

            if(userName == "rahul_shetty_learning")
            {
                loginPage.invalidLogin(userName, pass);
                WaitForElementToAppear(errMsgPlaceholder);

                string expectedText = "Incorrect username/password";
                string actualText = loginPage.getErrMsgText();
                Assert.That(actualText, Is.EqualTo(expectedText));
            }          
         
        }       

        public static IEnumerable<TestCaseData> AddTestDataConfig2()
        {
            yield return new TestCaseData(getDataParser().extractData("username", ConfigurationManager.AppSettings["LoginPageTestInfo.json"]),
                                          getDataParser().extractData("password", ConfigurationManager.AppSettings["LoginPageTestInfo.json"]));

            yield return new TestCaseData(getDataParser().extractData("username_wrong", ConfigurationManager.AppSettings["LoginPageTestInfo.json"]),
                                         getDataParser().extractData("password_wrong", ConfigurationManager.AppSettings["LoginPageTestInfo.json"]));


        }
    }
}
