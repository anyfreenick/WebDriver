using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriver_Part1.PageObjects;

namespace WebDriver_Part1.tests
{
    [TestClass]
    public class RemoteTests
    {
        private const string login = "webdriver_csharp";
        private const string password = "UnitTestingFramework";
        private const string domain = "bk.ru";
        private const string WDHub = "http://localhost:4444/wd/hub";

        private IWebDriver driver;
        private DesiredCapabilities capabilities;

        private string IDLogoutButton = "PH_logoutLink";

        [TestInitialize]
        public void TestSetup()
        {
            capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            driver = new RemoteWebDriver(new Uri(WDHub), capabilities);
        }

        [TestMethod]
        public void RemoteWDTestOnWin()
        {
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));            
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password);
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
            homepage.LogOff();
            Assert.IsTrue(loginpage.LoggedOut(), "Log off failed");
        }

        [TestMethod]
        public void RemoteWDTestOnLinux()
        {
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Linux));
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password, domain);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(IDLogoutButton)));
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
            homepage.LogOff();
            Assert.IsTrue(loginpage.LoggedOut(), "Log off failed");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
