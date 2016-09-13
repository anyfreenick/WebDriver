using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        private IWebElement DomainComboBox
        {
            get { return GetDriver().FindElement(By.Id("mailbox__login__domain")); }
        }

        private void SelectDomain(string domain)
        {
            var domainSelect = new SelectElement(DomainComboBox);
            domainSelect.SelectByValue(domain);
        }

        public void Open()
        {
            GetDriver().Navigate().GoToUrl(url);
            GetDriver().Manage().Window.Maximize();
        }

        public HomePage LoginAs(string username, string password, string domain = "mail.ru")
        {
            LoginField.SendKeys(username);
            PasswordField.SendKeys(password);
            SelectDomain(domain);
            LoginButton.Click();
            return new HomePage(GetDriver());
        }

        public bool LoggedOut()
        {
            if (LoginButton.Displayed)
                return true;
            else
                return false;
        }

        public HomePage LoginUsingJSClick(string username, string password, string domain = "mail.ru")
        {
            LoginField.SendKeys(username);
            PasswordField.SendKeys(password);
            SelectDomain(domain);
            IJavaScriptExecutor js = GetDriver() as IJavaScriptExecutor;
            js.ExecuteScript("document.getElementById('mailbox__auth__button').click()");
            return new HomePage(GetDriver());
        }
    }
}
