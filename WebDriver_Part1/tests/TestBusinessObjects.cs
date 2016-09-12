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
        private const string toEmail = "docent.86@mail.ru";
        private const string subjEmail = "Hello from webdriver";
        private const string bodyEmail = "Hello!!!\n\rThis email is sent automatically by selenium WebDriver!\n\rBest regards,\n\rSelenuim WebDriver.";

        IWebDriver driver;

        [TestMethod]
        public void TestBO()
        {
            driver = new FirefoxDriver();
            User user = new User("webdriver_mail.ru");
            Letter letter = new Letter("sample_mail");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            LoginPage loginpage = new LoginPage(driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(user.Login, user.Password, user.Domain);
            Assert.IsTrue(homepage.LoggedIn(), "Login failde");
            NewEmailPage newemail = homepage.CreateEmail();
            newemail.ComposeEmailAndSaveDraft(letter.Addressee, letter.Subject, letter.Body);
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
    }
}
