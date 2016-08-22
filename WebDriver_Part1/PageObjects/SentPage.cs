using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebDriver_Part1.PageObjects
{
    public class SentPage : BasePage
    {
        public SentPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement SentEmail(string emailText)
        {
            return GetDriver().FindElement(By.XPath("//span[text() = '" + emailText.Replace("\n\r", " ") + "']"));
        }

        public bool CheckEmailSent(string emailText)
        {
            if (SentEmail(emailText).Displayed)
                return true;
            else
                return false;
        }
    }
}
