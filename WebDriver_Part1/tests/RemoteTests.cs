using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriver_Part1.PageObjects
{
    [TestClass]
    public class RemoteTests
    {
        private const string login = "webdriver_csharp";
        private const string password = "UnitTestingFramework";
        private const string WDHub = "http://localhost:4444/wd/hub";

        private IWebDriver driver;

        [TestMethod]
        public void RemoteWDTestOnWin()
        {
            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            driver = new RemoteWebDriver(new Uri(WDHub), capabilities);
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password);
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
        }

        [TestMethod]
        public void RemoteWDTestOnLinux()
        {
            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Linux));
            driver = new RemoteWebDriver(new Uri(WDHub), capabilities);
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("PH_logoutLink")));
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
        }
    }
}
