using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.IO;
using System;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using AddContactToContactList;

namespace RemoveContactFromContactList
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class Tests<TWebDriver>  where TWebDriver : IWebDriver, new()
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

            _driver.FindElement(By.Id("email")).SendKeys("mutap.kenan.sa@live.com");
            _driver.FindElement(By.Id("password")).SendKeys("K3GDLwvRAt6N@@q");
            _driver.FindElement(By.Id("submit")).Click();
            Console.WriteLine(_driver.Title);
            driverWait.Timeout = TimeSpan.FromSeconds(5);
            wait.Until(ExpectedConditions.UrlContains("/contactList"));
            Thread.Sleep(2500);

            string email = "SiN.SiLn@mail.com";

            bool found = false;

            WebElement table = (WebElement)_driver.FindElement(By.Id("myTable"));

            //Get all columns
            int Column_Count = _driver.FindElements(By.XPath("//*[@id='myTable']/thead/tr/th")).Count();
            Console.WriteLine("Number of columns in table:" + Column_Count);

            string first_part = "//*[@id='myTable']/tr[";
            string second_part = "]/td[";
            string third_part = "]";

            //Used for loop for number of rows.
            for (int i = 1; i <= Column_Count; i++)
            {

                //Used for loop for number of columns.
                //for (int j = 2; j <= Column_Count; j++)
                //{
                char j = '4';
                //Prepared final xpath of specific cell as per values of i and j.
                string final_xpath = first_part + i + second_part + j + third_part;
                //Will retrieve value from located cell and print It.
                string Table_data = _driver.FindElement(By.XPath(final_xpath)).Text;
                Console.WriteLine(Table_data + " ");

                if (Table_data.Contains(email.ToLower())) {
                    found = true;
                    _driver.FindElement(By.XPath(final_xpath)).Click();
                    driverWait.Timeout = TimeSpan.FromSeconds(5);
                    wait.Until(ExpectedConditions.UrlContains("/contactDetails"));
                    _driver.FindElement(By.Id("delete")).Click();
                    Thread.Sleep(2500);
                    _driver.SwitchTo().Alert().Accept();
                    Thread.Sleep(10000);
                    break;
                }
                //}
            }
            if (found == false) { 
                Console.WriteLine("Contact wasn't added to contact list");
                throw new Exception("Contact wasn't added to contact list");
            }
        }

        [TearDown]
        public void EndTest()
        {
            _driver?.Quit();
        }

    }
}