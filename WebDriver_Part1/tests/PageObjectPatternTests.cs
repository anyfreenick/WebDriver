using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace WebDriver_Part1.PageObjects
{
    [TestClass]
    public class PageObjectPatternTests
    {
        private const string login = "webdriver_csharp";
        private const string password = "UnitTestingFramework";
        private const string toEmail = "docent.86@mail.ru";
        private const string subjEmail = "Hello from webdriver";
        private const string bodyEmail = "Hello!!!\n\rThis email is sent automatically by selenium WebDriver!\n\rBest regards,\n\rSelenuim WebDriver.";
        private const string WDHub = "http://localhost:4444/wd/hub";

        IWebDriver driver;
        
        
        [TestMethod]
        public void RunPageObjectTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password);
            Assert.IsTrue(homepage.LogedIn(), "Login failed");
            NewEmailPage newemail = homepage.CreateEmail();
            newemail.ComposeEmailAndSaveDraft(toEmail, subjEmail, bodyEmail);
            //No any other waits handled this, only hardcoded wait
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            DraftsPage draftpage = homepage.GoToDraftsFolder();
            draftpage.OpenSavedDraft(bodyEmail);
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath("//span[text() = '" + toEmail + "']")), "Draft email was not saved");
            Assert.IsTrue(draftpage.CheckDraftContent(toEmail, bodyEmail), "Error in draft content");
            Assert.IsTrue(draftpage.SendEmailByKeyBoard(), "Error while sending email");
            homepage.GoToDraftsFolder();
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath("//div[@class='b-datalist__empty__block']")), "Email was not sent");
            SentPage sentpage = homepage.GoToSentPage();
            Assert.IsTrue(sentpage.CheckEmailSent(bodyEmail), "Sent folder is empty, no email was sent");
            homepage.LogOff();
            Assert.IsTrue(loginpage.LoggedOut(), "Log off failed");
            driver.Quit();
        }

        [TestMethod]
        public void LoginLogout()
        {
            driver = new FirefoxDriver();
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password);
            Assert.IsTrue(homepage.LogedIn(), "Login failed");
            homepage.LogOff();
            Assert.IsTrue(loginpage.LoggedOut(), "Log off failed");
            driver.Quit();
        }

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
            Assert.IsTrue(homepage.LogedIn(), "Login failed");
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
            System.Threading.Thread.Sleep(5000);
            Assert.IsTrue(homepage.LogedIn(), "Login failed");
        }
    }
}
