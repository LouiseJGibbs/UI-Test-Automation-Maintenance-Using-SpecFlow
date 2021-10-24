using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using FixTheTests.Data;
using System.Collections.Generic;

namespace FixTheTests.Page
{
    public class AdminLoginPage
    {
        public IWebElement Footer => TestBase.Driver.FindElement(By.Id("footer"));
        public IWebElement AdminLink => Footer.FindElement(By.XPath(".//a[@href='/#/admin']"));
        public IWebElement UsernameTextbox => TestBase.Driver.FindElement(By.Id("username"));
        public IWebElement PasswordTextbox => TestBase.Driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => TestBase.Driver.FindElement(By.Id("doLogin"));

        public void ClickAdminLink()
        {
            AdminLink.Click();
        }

        public void SubmitUsernameAndPassword()
        {
            UsernameTextbox.SendKeys("admin");
            PasswordTextbox.SendKeys("password");
            LoginButton.Click();
        }

    }
}
