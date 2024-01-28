using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using OpenQA.Selenium.Support.UI;

namespace UdemySeleniumFrameworkWithCSharp.utilities
{
    public class BaseTest
    {
       public IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            String browserName = ConfigurationManager.AppSettings["browser"];
            // Global configuration file to pass Global variables
            InitBrowser(browserName);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = ConfigurationManager.AppSettings["baseUrl"];
        }

        public void InitBrowser(string browserName)
        {
            switch(browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
            }
        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        public IWebElement WaitForElementToAppear(By elementLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
            return driver.FindElement(elementLocator);
        }

        public static JSONReader getDataParser()
        {
            return new JSONReader();
        }
        
    }
}
