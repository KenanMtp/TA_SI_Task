using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.IO;
using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AddContactToContactList
{
    //Multibrowser support
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class Tests<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver? driver;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void NunitSeleniumTest()
        {
            System.String path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            //WebDriver driver = new ChromeDriver(path + "\\drivers");
            driver = new TWebDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Url = "https://thinking-tester-contact-list.herokuapp.com/";

            DefaultWait<IWebDriver> driverWait = new DefaultWait<IWebDriver>(driver);
            driverWait.Timeout = TimeSpan.FromSeconds(5);
            driverWait.Until(driver => driver.Title == "Contact List App");
            Thread.Sleep(2500);

            driver.FindElement(By.Id("email")).SendKeys("mutap.kenan.sa@live.com");
            driver.FindElement(By.Id("password")).SendKeys("K3GDLwvRAt6N@@q");
            driver.FindElement(By.Id("submit")).Click();
            Console.WriteLine(driver.Title);
            driverWait.Timeout = TimeSpan.FromSeconds(5);
            wait.Until(ExpectedConditions.UrlContains("/contactList"));
            Thread.Sleep(2500);

            driver.FindElement(By.Id("add-contact")).Click();
            driverWait.Timeout = TimeSpan.FromSeconds(5);
            wait.Until(ExpectedConditions.UrlContains("/addContact"));
            Thread.Sleep(2500);

            string email = "SiN.SiLn@mail.com";

            driver.FindElement(By.Id("firstName")).SendKeys("SI N 1");
            driver.FindElement(By.Id("lastName")).SendKeys("SI LN 1");
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("birthdate")).SendKeys("1999-09-09");
            driver.FindElement(By.Id("phone")).SendKeys("00112233");
            driver.FindElement(By.Id("street1")).SendKeys("1st Street");
            driver.FindElement(By.Id("street2")).SendKeys("2nd Street");
            driver.FindElement(By.Id("city")).SendKeys("Grad");
            driver.FindElement(By.Id("stateProvince")).SendKeys("ProvinceTest");
            driver.FindElement(By.Id("postalCode")).SendKeys("98765");
            driver.FindElement(By.Id("country")).SendKeys("USA");
            driver.FindElement(By.Id("submit")).Click();
            wait.Until(ExpectedConditions.UrlContains("/contactList"));
            Thread.Sleep(2500);

            bool found = false;

            WebElement table = (WebElement)driver.FindElement(By.Id("myTable"));

            //Get all columns
            int Column_Count = driver.FindElements(By.XPath("//*[@id='myTable']/thead/tr/th")).Count();
            Console.WriteLine("Number of columns in table:" + Column_Count);

            string first_part = "//*[@id='myTable']/tr[";
            string second_part = "]/td[";
            string third_part = "]";

            //Used for loop for number of rows.
            for (int i = 1; i <= Column_Count; i++)
            {

                //Used for loop for number of columns.
                for (int j = 2; j <= Column_Count; j++)
                {
                    //Prepared final xpath of specific cell as per values of i and j.
                    string final_xpath = first_part + i + second_part + j + third_part;
                    //Will retrieve value from located cell and print It.
                    string Table_data = driver.FindElement(By.XPath(final_xpath)).Text;
                    Console.WriteLine(Table_data + " ");

                    if (Table_data.Contains(email.ToLower())) {
                        found = true;
                        Console.WriteLine("Found expected string " + Table_data + " ");
                        break;
                    }
                }
            }
            if (found == false) { 
                Console.WriteLine("Contact wasn't added to contact list");
                throw new Exception("Contact wasn't added to contact list");
            }
            driver.Close();
            driver.Quit();
        }
    }
}
