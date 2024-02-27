using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.DevTools.V118.DOM;

namespace UdemySeleniumFrameworkWithCSharp.utilities
{
    public class BaseTest
    {
        public ExtentReports extentReports;
        public ExtentTest test;

        [OneTimeSetUp]

        public void SetUp()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extentReports = new ExtentReports();

            extentReports.AttachReporter(htmlReporter);
            extentReports.AddSystemInfo("Host name", "Local host");
            extentReports.AddSystemInfo("Environment", "QA");
            extentReports.AddSystemInfo("Username", "mtsonkova");

        }

        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();



        [SetUp]
        public void StartBrower()
        {
            test = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
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
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            string fileName = "Screenshot " + time.ToString("h_mm_ss") + ".png";

            if(status == TestStatus.Failed)
            {
                test.Fail("Test is failed.", captureScreenshot(driver.Value,fileName));
                test.Log(Status.Fail, "Test failed with log" + stackTrace);
            }
            else if(status == TestStatus.Passed)
            {
                test.Pass("Test is passed.");
            }
            else if (status == TestStatus.Skipped)
            {
                test.Skip("Test is skipped");
            }

            extentReports.Flush();

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

        public MediaEntityModelProvider captureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            var screenshot = takesScreenshot.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }
    }
}
