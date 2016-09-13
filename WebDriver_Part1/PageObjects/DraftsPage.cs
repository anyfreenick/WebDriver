using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebDriver_Part1.PageObjects
{
    public class DraftsPage : BasePage
    {
        public DraftsPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement EmailBodyFrame
        {
            get { return GetDriver().FindElement(By.XPath("//iframe[contains(@id, 'compose')]")); }
        }

        private IWebElement EmailBody
        {
            get { return GetDriver().FindElement(By.Id("tinymce")); }
        }

        private IWebElement GetDraftEmail(string emailText)
        {
            return GetDriver().FindElement(By.XPath("//span[text() = '" + emailText.Replace("\n\r", " ") + "']"));
        }

        private IWebElement SendButton
        {
            get { return GetDriver().FindElement(By.XPath("//span[text()='Отправить']")); }
        }

        public void OpenSavedDraft(string emailText)
        {
            GetDraftEmail(emailText).Click();
        }

        public bool CheckDraftContent(string addresse, string emailText)
        {
            if (GetDriver().FindElement(By.XPath("//span[text() = '" + addresse + "']")).Displayed)
            {
                GetDriver().SwitchTo().Frame(EmailBodyFrame);
                string expectedBody = emailText.Replace("\n\r", "").Replace("\r\n", "");
                string actualBody = EmailBody.Text.Replace("\n\r", "").Replace("\r\n", "");
                if (expectedBody.Equals(actualBody))
                {
                    GetDriver().SwitchTo().DefaultContent();
                    return true;
                }                    
                GetDriver().SwitchTo().DefaultContent();
                return false;
            }
            else
                return false;
        }

        public bool SendEmail()
        {
            if (SendButton.Displayed)
            {
                SendButton.Click();
                if (IsElementPresent(By.XPath("//div[@class='message-sent__title']")))
                    return true;
                return false;
            }
            return false;
        }

        public bool SendEmailByKeyBoard()
        {
            if (SendButton.Displayed)
            {
                
                //this action emulated pressing ctrl+enter for sendin email
                new Actions(GetDriver()).KeyDown(Keys.Control).SendKeys(Keys.Enter).KeyUp(Keys.Control).Build().Perform();
                if (IsElementPresent(By.XPath("//div[@class='message-sent__title']")))
                    return true;
                return false;
            }
            return false;
        }
    }
}
