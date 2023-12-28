using AngleSharp.Text;
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

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void E2ETest()
        {
           
            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[expectedProducts.Count()];
           
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

            IList<IWebElement> productsInCart = driver.FindElements(By.CssSelector("h4 a"));

            for(int i = 0; i < productsInCart.Count; i++)
            {
                actualProducts[i] = productsInCart[i].Text;
                
            }

            Assert.AreEqual(expectedProducts, actualProducts);


            driver.FindElement(By.CssSelector(".btn.btn-success")).Click();

            driver.FindElement(By.Id("country")).SendKeys("United");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("United Kingdom")));
            driver.FindElement(By.LinkText("United Kingdom")).Click();

            //driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();
            driver.FindElement(By.TagName("label")).Click();
            driver.FindElement(By.CssSelector(".btn.btn-lg")).Click();

           // String expectedConfirmationText = "×\r\nSuccess! Thank you! Your order will be delivered in next few weeks :-).";
            String actualConfirmationText = driver.FindElement(By.CssSelector(".alert.alert-success")).Text;

            //Assert.AreEqual(expectedConfirmationText, actualConfirmationText);

            StringAssert.Contains("Success", actualConfirmationText);
        }
    }
}