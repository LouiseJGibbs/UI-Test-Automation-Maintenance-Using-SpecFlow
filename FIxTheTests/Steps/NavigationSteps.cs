
using OpenQA.Selenium;
using FixTheTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace FixTheTests
{
    [Binding]
    public sealed class NavigationSteps
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public IWebElement LetMeHackButton => TestBase.Driver.FindElement(By.XPath("//button[text()='Let me hack!']"));
        public IWebElement Footer => TestBase.Driver.FindElement(By.Id("footer"));
        public IWebElement AdminLink => Footer.FindElement(By.XPath(".//a[@href='/#/admin']"));
        public IWebElement UsernameTextbox => TestBase.Driver.FindElement(By.Id("username"));
        public IWebElement PasswordTextbox => TestBase.Driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => TestBase.Driver.FindElement(By.Id("doLogin"));
        public IWebElement FrontPageLink => TestBase.Driver.FindElement(By.Id("frontPageLink"));


        [BeforeScenario]
        public void BeforeScenario()
        {
            TestBase.InitializeTest();
            LetMeHackButton.Click();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            TestBase.Driver.Close();
        }

       
    }
}