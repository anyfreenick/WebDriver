using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace WebDriver_Part1.Patterns.Singleton
{
    public sealed class WebDriverSingleton
    {
        private static IWebDriver _webdriver;

        private static WebDriverSingleton instance;

        private WebDriverSingleton() { }
        
        public static WebDriverSingleton GetInstance(DriverType drivertype)
        {
            if (instance == null)
            {
                instance = new WebDriverSingleton();
                switch (drivertype)
                {
                    case DriverType.Firefox:
                        instance.Driver = new FirefoxDriver();
                        break;
                    case DriverType.Chrome:
                        instance.Driver = new ChromeDriver();
                        break;
                    default:
                        instance.Driver = new FirefoxDriver();
                        break;
                }
            }
            return instance;
        }

        public IWebDriver Driver
        {
            private set { _webdriver = value; }
            get { return _webdriver; }
        }
    }
    public enum DriverType
    {
        Firefox,
        Chrome
    }
}
