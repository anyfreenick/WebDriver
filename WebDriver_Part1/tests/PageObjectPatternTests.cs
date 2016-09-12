using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriver_Part1.PageObjects;

namespace WebDriver_Part1.tests
{
    [TestClass]
    public class PageObjectPatternTests
    {
        private const string login = "webdriver_csharp";
        private const string password = "UnitTestingFramework";
        private const string toEmail = "docent.86@mail.ru";
        private const string subjEmail = "Hello from webdriver";
        private const string bodyEmail = "Hello!!!\n\rThis email is sent automatically by selenium WebDriver!\n\rBest regards,\n\rSelenuim WebDriver.";        

        IWebDriver driver;
        
        
        [TestMethod]
        public void RunPageObjectTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(login, password);
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
            NewEmailPage newemail = homepage.CreateEmail();
            newemail.ComposeEmailAndSaveDraft(toEmail, subjEmail, bodyEmail);
            //No any other waits handled this, only hardcoded wait
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            DraftsPage draftpage = homepage.GoToDraftsFolder();
            draftpage.OpenSavedDraft(bodyEmail);
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath("//span[text() = '" + toEmail + "']")), "Draft email was not saved");
            Assert.IsTrue(draftpage.CheckDraftContent(toEmail, bodyEmail), "Error in draft content");
            Assert.IsTrue(draftpage.SendEmail(), "Error while sending email");
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
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
            homepage.LogOff();
            Assert.IsTrue(loginpage.LoggedOut(), "Log off failed");
            driver.Quit();
        }

        [TestMethod]
        public void ActionTestWithJS()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            //Login button is being clicked with Javascript
            HomePage homepage = loginpage.LoginUsingJSClick(login, password);
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
            NewEmailPage newemail = homepage.CreateEmailViaAction();
            newemail.ComposeEmailAndSaveDraft(toEmail, subjEmail, bodyEmail);
            //No any other waits handled this, only hardcoded wait
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            DraftsPage draftpage = homepage.GoToDraftsFolder();
            draftpage.OpenSavedDraft(bodyEmail);
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath("//span[text() = '" + toEmail + "']")), "Draft email was not saved");
            Assert.IsTrue(draftpage.CheckDraftContent(toEmail, bodyEmail), "Error in draft content");
            //Email is sent by pressing ctrl+enter buttons on the keyboard
            Assert.IsTrue(draftpage.SendEmailByKeyBoard(), "Error while sending email");
            homepage.GoToDraftsFolder();
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath("//div[@class='b-datalist__empty__block']")), "Email was not sent");
            SentPage sentpage = homepage.GoToSentPage();
            Assert.IsTrue(sentpage.CheckEmailSent(bodyEmail), "Sent folder is empty, no email was sent");
            //Before clicking logoff button, the button is highlighted with red color
            homepage.LogOffWithHihgLight();
            Assert.IsTrue(loginpage.LoggedOut(), "Log off failed");
            driver.Quit();
        }
    }
}