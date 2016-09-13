using OpenQA.Selenium;

namespace WebDriver_Part1.PageObjects
{
    public class NewEmailPage : BasePage
    {
        public NewEmailPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement Addressee
        {
            get { return GetDriver().FindElement(By.XPath("//textarea[@data-original-name='To']")); }
        }

        private IWebElement Subject
        {
            get { return GetDriver().FindElement(By.Name("Subject")); }
        }

        private IWebElement EmailBodyFrame
        {
            get { return GetDriver().FindElement(By.XPath("//iframe[contains(@id, 'compose')]")); }
        }

        private IWebElement EmailBody
        {
            get { return GetDriver().FindElement(By.Id("tinymce")); }
        }

        private IWebElement SaveButton
        {
            get { return GetDriver().FindElement(By.XPath("//span[text()='Сохранить']")); }
        }

        public IWebElement SaveConfirmation
        {
            get { return GetDriver().FindElement(By.XPath("//div[@data-mnemo='saveStatus']")); }
        }

        public void ComposeEmailAndSaveDraft(string addressee, string subject, string body)
        {
            Addressee.SendKeys(addressee);
            Subject.SendKeys(subject);
            GetDriver().SwitchTo().Frame(EmailBodyFrame);
            EmailBody.Clear();
            EmailBody.Click();
            EmailBody.SendKeys(body);
            GetDriver().SwitchTo().DefaultContent();
            SaveButton.Click();
        }
    }
}
