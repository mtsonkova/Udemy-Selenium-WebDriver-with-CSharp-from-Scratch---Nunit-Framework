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
        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void StartBrower()
        {
            String browserName = ConfigurationManager.AppSettings["browser"];
            // Global configuration file to pass Global variables
            InitBrowser(browserName);

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = ConfigurationManager.AppSettings["baseUrl"];
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
            }
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        [TearDown]
        public void Teardown()
        {
            driver.Value.Quit();
        }

        public IWebElement WaitForElementToAppear(By elementLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
            return driver.Value.FindElement(elementLocator);
        }

        public static JSONReader getDataParser()
        {
            return new JSONReader();
        }

    }
}
