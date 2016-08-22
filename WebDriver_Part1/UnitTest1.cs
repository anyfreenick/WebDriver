using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebDriver_Part1
{
    [TestClass]
    public class UnitTest1
    {
        string url = "http://mail.ru";
        string username = "webdriver_csharp";
        string domain = "@mail.ru";
        string passwd = "UnitTestingFramework";
        string toEmail = "docent.86@mail.ru";
        string subjEmail = "Hello from webdriver";
        string bodyEmail = "Hello!!!\n\rThis email is sent automatically by selenium WebDriver!\n\rBest regards,\n\rSelenuim WebDriver.";

        IWebDriver driver = new FirefoxDriver();

        [TestMethod]
        public void RunTest()
        {
            Login();
            CreateNewEmailAndSaveDraft();
            CheckDraftsFolder();
            VerifyDraftContentAndSendEmail();
            CheckDraftsAndSentFolders();
            LogOff();
        }

        public void Login()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.XPath(".//*[@id='mailbox__login']")).SendKeys(username);

            var domain_combobox = driver.FindElement(By.XPath(".//*[@id='mailbox__login__domain']"));
            var domain_select = new SelectElement(domain_combobox);
            domain_select.SelectByText(domain);

            driver.FindElement(By.XPath(".//*[@id='mailbox__password']")).SendKeys(passwd);

            driver.FindElement(By.XPath(".//*[@id='mailbox__auth__button']")).Click();

            var loggedUser = driver.FindElement(By.XPath(".//*[@id='PH_user-email']"));

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@id='PH_user-email']")));            

            Assert.AreEqual(username + domain, loggedUser.Text);            
        }
        
        public void CreateNewEmailAndSaveDraft()
        {
            driver.FindElement(By.XPath("//span[text()='Написать письмо']")).Click();
            driver.FindElement(By.XPath("//textarea[@data-original-name='To']")).SendKeys(toEmail);
            driver.FindElement(By.Name("Subject")).SendKeys(subjEmail);
            IWebElement frame = driver.FindElement(By.XPath("//iframe[contains(@id, 'compose')]"));
            driver.SwitchTo().Frame(frame);
            IWebElement body = driver.FindElement(By.Id("tinymce"));
            body.Clear();
            body.Click();
            body.SendKeys(bodyEmail);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.XPath("//span[text()='Сохранить']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-mnemo='saveStatus']")));

            IWebElement successMessage = driver.FindElement(By.XPath("//div[@data-mnemo='saveStatus']"));
            Assert.IsTrue(successMessage.Displayed, "Message is not present");
        }

        public void CheckDraftsFolder()
        {
            driver.FindElement(By.XPath("//span[text()='Черновики']")).Click();
            IWebElement draftEmail = driver.FindElement(By.XPath("//span[text() = '" + bodyEmail.Replace("\n\r", " ") + "']"));
            Assert.IsTrue(draftEmail.Displayed, "Draft email is not displayed on the page");
        }

        public void VerifyDraftContentAndSendEmail()
        {
            IWebElement draftEmail = driver.FindElement(By.XPath("//span[text() = '" + bodyEmail.Replace("\n\r", " ") + "']"));
            draftEmail.Click();
            Assert.IsTrue(driver.FindElement(By.XPath("//span[text() = '" + toEmail + "']")).Displayed, "To email is not displayed");
            /*Assert.IsTrue(driver.FindElement(By.XPath("//input[@name='Subject']")).Text == subjEmail);
             * cannot check subject field content, brcause it is empty in DOM tree
            */
            IWebElement frame = driver.FindElement(By.XPath("//iframe[contains(@id, 'compose')]"));
            driver.SwitchTo().Frame(frame);
            IWebElement body = driver.FindElement(By.Id("tinymce"));
            string expectedBody = bodyEmail.Replace("\n\r", "").Replace("\r\n", "");
            string actualBody = body.Text.Replace("\n\r", "").Replace("\r\n", "");            
            Assert.AreEqual(expectedBody, actualBody, "Error in message body");
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.XPath("//span[text()='Отправить']")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='message-sent__title']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//div[@class='message-sent__title']")).Displayed);
        }

        public void CheckDraftsAndSentFolders()
        {
            driver.FindElement(By.XPath("//span[text()='Черновики']")).Click();
            IWebElement emptyDraftFolder = driver.FindElement(By.XPath("//div[@class='b-datalist__empty__block']"));
            Assert.IsTrue(emptyDraftFolder.Displayed, "Draft email is not displayed on the page");
            driver.FindElement(By.XPath("//span[text()='Отправленные']")).Click();
            IWebElement sentEmail = driver.FindElement(By.XPath("//span[text() = '" + bodyEmail.Replace("\n\r", " ") + "']"));
            Assert.IsTrue(sentEmail.Displayed, "Sent email is not displayed on the page");
        }

        public void LogOff()
        {
            driver.FindElement(By.Id("PH_logoutLink")).Click();
        }
    }
}
