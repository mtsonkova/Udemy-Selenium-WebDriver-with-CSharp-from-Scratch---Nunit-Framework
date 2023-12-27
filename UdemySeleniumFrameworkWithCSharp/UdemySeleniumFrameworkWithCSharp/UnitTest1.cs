using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UdemySeleniumFrameworkWithCSharp
{
   
    public class Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void E2ETest()
        {
            String url = "https://rahulshettyacademy.com/angularpractice/";
            String[] expectedProducts = { "iphone X", "Blackberry" };
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//div[@class='form-group][5]/label/span/input"));
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible);


        }
    }
}