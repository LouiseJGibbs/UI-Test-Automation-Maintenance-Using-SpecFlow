using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using FixTheTests.Data;
using System.Collections.Generic;

namespace FixTheTests.Page
{
    public class ContactUsSection
    {
        public IWebElement ContactSection => TestBase.Driver.FindElement(By.XPath(".//div[@class='row contact']//div[@class='col-sm-5']"));


        //Contact form
        public By FormSectionBy => By.XPath("//form");
        //public IWebElement FormSection => ContactSection.FindElement(FormSectionBy);

        public IWebElement NameTextBox => TestBase.Driver.FindElement(By.XPath("//input[@id='name']"));
        public IWebElement EmailTextBox => TestBase.Driver.FindElement(By.XPath("//input[@id='email']"));
        public IWebElement PhoneTextBox => TestBase.Driver.FindElement(By.XPath("//input[@placeholder='Phone']"));
        public IWebElement SubjectTextBox => TestBase.Driver.FindElement(By.XPath("//input[@id='subject']"));
        public IWebElement MessageTextBox => TestBase.Driver.FindElement(By.XPath("//textarea[@id='description']"));
        public IWebElement SubmitButton => TestBase.Driver.FindElement(By.XPath("//button[@id='submitContact']"));

        //Confirmation message

        public IWebElement HeaderText => ContactSection.FindElement(By.XPath(".//h2"));
        public IReadOnlyCollection<IWebElement> ParagraphText => ContactSection.FindElements(By.XPath(".//p"));

        //Warning messages

        public IReadOnlyCollection<IWebElement> WarningMessageList => TestBase.Driver.FindElements(By.XPath("//div[@class='alert alert-danger']/p"));

        public List<string> getListOfWarningMessages()
        {
            List<string> WarningList = new List<string>();

            foreach (IWebElement element in WarningMessageList)
            {
                WarningList.Add(element.Text);
            }

            return WarningList;
        }

    }
}




//CORRECT XPATHS 

