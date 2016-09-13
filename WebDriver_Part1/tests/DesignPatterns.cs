﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver_Part1.BusinessObjects;
using WebDriver_Part1.Patterns.Singleton;
using WebDriver_Part1.PageObjects;

namespace WebDriver_Part1.tests
{
    [TestClass]
    public class DesignPatterns
    {
        WebDriverSingleton _driver = WebDriverSingleton.GetInstance();
        User user = new User("webdriver_bk.ru");

        [TestMethod]
        public void SingletonLoginLogOut()
        {
            LoginPage loginpage = new LoginPage(_driver.Driver);
            loginpage.Open();
            HomePage homepage = loginpage.LoginAs(user.Login, user.Password, user.Domain);
            Assert.IsTrue(homepage.LoggedIn(), "Login Failed");
            homepage.LogOff();
            Assert.IsTrue(loginpage.LoggedOut(), "Logoff failed");
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Driver.Quit();
        }
    }
}
