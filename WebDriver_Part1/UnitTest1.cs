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

        IWebDriver driver = new FirefoxDriver();

        [TestMethod]
        public void RunTest()
        {
            Login();
            CreateNewEmailAndSaveDraft();
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
            driver.FindElement(By.XPath("//textarea[@data-original-name='To']")).SendKeys("docent.86@mail.ru");
            driver.FindElement(By.Name("Subject")).SendKeys("Hello from webdriver");
            IWebElement frame = driver.FindElement(By.XPath("//iframe[contains(@id, 'compose')]"));
            driver.SwitchTo().Frame(frame);
            IWebElement body = driver.FindElement(By.Id("tinymce"));
            body.Clear();
            body.Click();
            body.SendKeys("Hello!!!\n\rThis email is sent automatically by selenium WebDriver!\n\rBest regards,\n\rSelenuim WebDriver.");
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.XPath("//span[text()='Сохранить']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-mnemo='saveStatus']")));

            IWebElement successMessage = driver.FindElement(By.XPath("//div[@data-mnemo='saveStatus']"));
            Assert.IsTrue(successMessage.Displayed, "Message is not present");
        }
    }
}
