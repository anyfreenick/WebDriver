using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;

namespace WebDriver_Part1.PageObjects
{
    public class LoginPage : BasePage
    {
        private const string url = "http://mail.ru";
        
        public LoginPage(IWebDriver driver) : base(driver)
        {            
        }

        private IWebElement LoginField
        {
            get { return GetDriver().FindElement(By.Id("mailbox__login")); }
        }

        private IWebElement PasswordField
        {
            get { return GetDriver().FindElement(By.Id("mailbox__password")); }
        }

        private IWebElement LoginButton
        {
            get { return GetDriver().FindElement(By.Id("mailbox__auth__button")); }
        }

        public void Open()
        {
            GetDriver().Navigate().GoToUrl(url);
            GetDriver().Manage().Window.Maximize();
        }

        public HomePage LoginAs(string username, string password)
        {
            LoginField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
            return new HomePage(GetDriver());
        }
    }
}
