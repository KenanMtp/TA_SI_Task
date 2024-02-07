using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Negative_Phone4OrLessCharacters
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class Tests<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver _driver;
        [SetUp]
        public void TestInitialize()
        {
            //StartExtentTest(TestContext.CurrentContext.Test.Name);
            //_driver = new ChromeDriver();
            //_driver.Navigate().GoToUrl("https://www.google.com/");
            _driver = new TWebDriver();
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://thinking-tester-contact-list.herokuapp.com/";
        }

        [Test]
        public void NunitSeleniumTest()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            System.String path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            //WebDriver driver = new ChromeDriver(path + "\\drivers");

            DefaultWait<IWebDriver> driverWait = new DefaultWait<IWebDriver>(_driver);
            driverWait.Timeout = TimeSpan.FromSeconds(5);
            driverWait.Until(_driver => _driver.Title == "Contact List App");
            Thread.Sleep(2500);

            //Used unexisting email for login
            _driver.FindElement(By.Id("email")).SendKeys("incorrect@email.com");
            _driver.FindElement(By.Id("password")).SendKeys("Password12345");
            _driver.FindElement(By.Id("submit")).Click();
            Console.WriteLine(_driver.Title);
            driverWait.Timeout = TimeSpan.FromSeconds(5);
            Thread.Sleep(2500);

            bool found = false;
            string appeared_error_message = _driver.FindElement(By.Id("error")).Text;
            string expected_error_message = "Incorrect username or password";
            if (appeared_error_message.Contains(expected_error_message))
            {
                found = true;  
                Console.WriteLine("Expected error message available!");
            }
            if (found == false) { 
                Console.WriteLine("Expected err message didn't appear");
                throw new Exception("Expected err message didn't appear");
            }
        }

        [TearDown]
        public void EndTest()
        {
            if (_driver != null)
                _driver.Quit();
        }

    }
}