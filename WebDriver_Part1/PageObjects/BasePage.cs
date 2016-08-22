using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriver_Part1.PageObjects
{
    public abstract class BasePage
    {
        private static IWebDriver _driver;        

        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebDriver GetDriver()
        {
            return _driver;
        }

        public bool IsElementPresent(By locator)
        {
            return _driver.FindElements(locator).Count > 0;
        }        
    }
}
