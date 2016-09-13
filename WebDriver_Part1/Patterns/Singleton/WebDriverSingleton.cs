using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebDriver_Part1.Patterns.Singleton
{
    public sealed class WebDriverSingleton
    {
        private static IWebDriver _webdriver;

        private static WebDriverSingleton _driver = new WebDriverSingleton();

        private WebDriverSingleton()
        {
            _webdriver = new FirefoxDriver();
        }

        public static WebDriverSingleton GetInstance()
        {
            return _driver;
        }

        public IWebDriver Driver
        {
            private set { _webdriver = value; }
            get { return _webdriver; }
        }
    }
}
