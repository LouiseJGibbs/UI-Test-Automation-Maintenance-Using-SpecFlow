using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xunit;

namespace FixTheTests
{
    public static class TestBase
    {
        public static int Timeout = 10;

        public static IWebDriver Driver { get; set; }

        public static void InitializeTest()
        {
            Driver = new ChromeDriver(Environment.CurrentDirectory);
            Driver.Navigate().GoToUrl("https://automationintesting.online/");
            Driver.Manage().Window.Maximize();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Timeout);
        }

        public static void ScrollToElement(IWebElement element)
        {
            var yPos = element.Location.Y;
            var windowSize = Driver.Manage().Window.Size.Height;
            var scrollPosition = yPos - (windowSize / 2);
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, arguments[0]);", scrollPosition);
        }

        public static bool Exists(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }

        }

        public static bool Exists(this By elementBy)
        {
            try
            {
                return Driver.FindElement(elementBy).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static string GenerateRandomString(int length = 10)
        {
            Thread.Sleep(500);

            string chars = "abcdefghijklmnopqrstuvwxyz";
            string finalString = "";
            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                finalString += chars[r.Next(chars.Length)];
            }

            return finalString;
        }

        public static string GenerateRandomString(int minLength, int maxLength)
        {
            Thread.Sleep(500);

            string chars = "abcdefghijklmnopqrstuvwxyz";
            string finalString = "";
            Random r = new Random();

            for (int i = 0; i < r.Next(minLength, maxLength); i++)
            {
                finalString += chars[r.Next(chars.Length)];
            }

            return finalString;
        }

        public static string GenerateRandomPhoneNumber(int minLength, int maxLength)
        {
            string chars = "0123456789";
            string finalString = "0";
            Random r = new Random();

            for (int i = 0; i < r.Next(minLength, maxLength); i++)
            {
                finalString += chars[r.Next(chars.Length)];
            }

            return finalString;
        }
    }
}