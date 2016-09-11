using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriver_Part1.PageObjects
{
    /// <summary>
    /// Summary description for RemoteTests
    /// </summary>
    [TestClass]
    public class RemoteTests
    {
        private const string login = "webdriver_csharp";
        private const string password = "UnitTestingFramework";
        private const string toEmail = "docent.86@mail.ru";
        private const string subjEmail = "Hello from webdriver";
        private const string bodyEmail = "Hello!!!\n\rThis email is sent automatically by selenium WebDriver!\n\rBest regards,\n\rSelenuim WebDriver.";
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
