using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace UdemySeleniumFrameworkWithCSharp
{
   
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void E2ETest()
        {
           
            String[] expectedProducts = { "iphone X", "Blackberry" };
           
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
           
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
           
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> products =  driver.FindElements(By.TagName("app-card"));

            foreach(IWebElement product in products) 
            {

                String productTitle = product.FindElement(By.CssSelector(".card-title a")).Text;
               
                if(expectedProducts.Contains(productTitle))
                {
                    IWebElement btn_Add = product.FindElement(By.CssSelector(".card-footer button"));

                    btn_Add.Click();
                }
                
            }


            driver.FindElement(By.PartialLinkText("Checkout")).Click();


        }
    }
}