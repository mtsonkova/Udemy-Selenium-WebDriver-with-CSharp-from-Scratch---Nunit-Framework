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
    public class LoginPageTests : BaseTest
    {
        [Test]
        public void LogInWithCorrectCredentials(string username, string password)
        {

            string userName = username;
            string pass = password;
            LoginPage loginPage = new LoginPage(getDriver());


            Assert.That(driver.Url, Is.Not.EqualTo(ConfigurationManager.AppSettings["baseUrl"]));
            //https://rahulshettyacademy.com/angularpractice/shop
        }

        public void InvalidLogin(string username_wrong, string password_wrong) { 
        }


        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username", ConfigurationManager.AppSettings["LoginPageTestInfo.json"]),
                                          getDataParser().extractData("password", ConfigurationManager.AppSettings["LoginPageTestInfo.json"]));
         

        }
    }
}
