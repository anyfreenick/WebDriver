using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriver_Part1.PageObjects;
using WebDriver_Part1.BusinessObjects;

namespace WebDriver_Part1.tests
{
    [TestClass]
    public class TestBusinessObjects
    {
        // Business objects
        private IWebDriver driver;
        private User user;
        private Letter letter;

        // Locators
        private string xpathEmptyDraftFolder = "//div[@class='b-datalist__empty__block']";

        [TestInitialize]
        public void TestSetup()
        {
            driver = new FirefoxDriver();
            user = new User("webdriver_mail.ru");
            letter = new Letter("sample_mail");
        }

        [TestMethod]
        public void TestBO()
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(user.Login, user.Password, user.Domain);
            Assert.IsTrue(homepage.LoggedIn(), "Login failed");
            NewEmailPage newemail = homepage.CreateEmail();
            newemail.ComposeEmailAndSaveDraft(letter.Addressee, letter.Subject, letter.Body);
            //No any other waits handled this, only hardcoded wait
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            DraftsPage draftpage = homepage.GoToDraftsFolder();
            draftpage.OpenSavedDraft(letter.Body);
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath("//span[text() = '" + letter.Addressee + "']")), "Draft email was not saved");
            Assert.IsTrue(draftpage.CheckDraftContent(letter.Addressee, letter.Body), "Error in draft content");
            Assert.IsTrue(draftpage.SendEmail(), "Error while sending email");
            homepage.GoToDraftsFolder();
            Assert.IsTrue(draftpage.IsElementPresent(By.XPath(xpathEmptyDraftFolder)), "Email was not sent");
            SentPage sentpage = homepage.GoToSentPage();
            Assert.IsTrue(sentpage.CheckEmailSent(letter.Body), "Sent folder is empty, no email was sent");
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
