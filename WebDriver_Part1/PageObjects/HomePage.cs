﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebDriver_Part1.PageObjects
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement ComposeButton
        {
            get { return GetDriver().FindElement(By.XPath("//a[@data-name='compose']")); }
        }

        private IWebElement SentFolderLink
        {
            get { return GetDriver().FindElement(By.XPath("//span[text()='Отправленные']")); }
        }

        private IWebElement DraftsFolderLink
        {
            get { return GetDriver().FindElement(By.XPath("//span[text()='Черновики']")); }
        }

        private IWebElement LogOffButton
        {
            get { return GetDriver().FindElement(By.Id("PH_logoutLink")); }
        }

        public NewEmailPage CreateEmail()
        {
            ComposeButton.Click();
            return new NewEmailPage(GetDriver());
        }

        public NewEmailPage CreateEmailViaAction()
        {
            new Actions(GetDriver()).Click(ComposeButton).Build().Perform();
            return new NewEmailPage(GetDriver());
        }

        public DraftsPage GoToDraftsFolder()
        {
            GetDriver().Navigate().Refresh();
            DraftsFolderLink.Click();
            return new DraftsPage(GetDriver());
        }

        public SentPage GoToSentPage()
        {
            GetDriver().Navigate().Refresh();
            SentFolderLink.Click();
            return new SentPage(GetDriver());
        }

        public void LogOff()
        {
            LogOffButton.Click();
        }

        public void LogOffWithHihgLight()
        {
            HighLightLogOffButton();
            LogOffButton.Click();
        }

        private void HighLightLogOffButton()
        {
            IJavaScriptExecutor js = GetDriver() as IJavaScriptExecutor;            
            js.ExecuteScript("arguments[0].style.backgroundColor = 'red'", LogOffButton);
            //this wait is used to see that log off button is highlighted
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        public bool LoggedIn()
        {
            if (LogOffButton.Displayed)
                return true;
            else
                return false;
        }
    }
}
