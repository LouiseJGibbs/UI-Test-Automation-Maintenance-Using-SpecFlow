using FixTheTests;
using FixTheTests.Data;
using FixTheTests.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace FIxTheTests.Steps
{
    [Binding]
    public sealed class ContactUsSteps
    {
        private readonly TestData _testData = new TestData();
        private readonly ContactUsSection _contactUsSection = new ContactUsSection();

        public ContactUsSteps(TestData testData)
        {
            _testData = testData;
        }

        [Given(@"I have generated some valid contact us message details")]
        public void GivenIHaveGeneratedSomeValidContactUsMessageDetails()
        {
            Random r = new Random();

            _testData.MyContactUsMessage.Name = TestBase.GenerateRandomString();
            _testData.MyContactUsMessage.Email = _testData.MyRoomBooking.Email = String.Format("{0}@{1}.com", TestBase.GenerateRandomString(), TestBase.GenerateRandomString());
            _testData.MyContactUsMessage.PhoneNumber = TestBase.GenerateRandomPhoneNumber(11, 22); ;
            _testData.MyContactUsMessage.Subject = TestBase.GenerateRandomString(); 
            _testData.MyContactUsMessage.Message = TestBase.GenerateRandomString(r.Next(20, 2000)); 
        }

        [Given(@"I have entered and submitted the contact us details")]
        public void GivenIHaveEnteredAndSubmittedTheContactUsDetails()
        {
            _contactUsSection.NameTextBox.SendKeys(_testData.MyContactUsMessage.Name);
            _contactUsSection.EmailTextBox.SendKeys(_testData.MyContactUsMessage.Email);
            _contactUsSection.PhoneTextBox.SendKeys(_testData.MyContactUsMessage.PhoneNumber);
            _contactUsSection.SubjectTextBox.SendKeys(_testData.MyContactUsMessage.Subject);
            _contactUsSection.MessageTextBox.SendKeys(_testData.MyContactUsMessage.Message);

            TestBase.ScrollToElement(_contactUsSection.SubmitButton);
            _contactUsSection.SubmitButton.Click();
        }

        [When(@"I submit the following contact details (.*), (.*), (.*), (.*) and (.*)")]
        public void WhenISubmitTheFollowingContactDetailsTestTestTest_ComTestingAndHelloWorldCanIBookARoomPlease(string name, string email, string phone, string subject, string message)
        {
            _testData.MyContactUsMessage.Name = name;
            _testData.MyContactUsMessage.Email = email;
            _testData.MyContactUsMessage.PhoneNumber = phone;
            _testData.MyContactUsMessage.Subject = subject;
            _testData.MyContactUsMessage.Message = message;

            GivenIHaveEnteredAndSubmittedTheContactUsDetails();
        }

        [When(@"I complete the name field in the contact us form")]
        public void WhenICompleteTheNameFieldInTheContactUsForm()
        {
            IWebElement NameTextBox = TestBase.Driver.FindElement(By.XPath("//input[]"));
            NameTextBox.SendKeys(_testData.MyContactUsMessage.Name);
        }

        [When(@"I complete the email field in the contact us form")]
        public void WhenICompleteTheEmailFieldInTheContactUsForm()
        {
            IWebElement EmailTextBox = TestBase.Driver.FindElement(By.XPath("//input[@id=email'"));
            EmailTextBox.SendKeys(_testData.MyContactUsMessage.Email);
        }


        [When(@"I complete the phone field in the contact us form")]
        public void WhenICompleteThePhoneFieldInTheContactUsForm()
        {
            IWebElement PhoneTextBox = TestBase.Driver.FindElement(By.XPath("//input[@placeholder='PhoneNumber']"));
            PhoneTextBox.SendKeys(_testData.MyContactUsMessage.PhoneNumber);
        }

        [When(@"I complete the subject field in the contact us form")]
        public void WhenICompleteTheSubjectFieldInTheContactUsForm()
        {
            IWebElement MessageTextBox = TestBase.Driver.FindElement(By.XPath("//input[@id='description']"));
            MessageTextBox.SendKeys(_testData.MyContactUsMessage.Message);
        }

        [When(@"I complete the message field in the contact us form")]
        public void WhenICompleteTheMessageFieldInTheContactUsForm()
        {
            //Oops, looks like someone forgot to add the locator. You'll need to write it yourself

            //IWebElement MessageTextBox = TestBase.Driver.FindElement(By.XPath());
            //MessageTextBox.SendKeys(_testData.MyContactUsMessage.Message);
            ScenarioContext.Current.Pending();
        }

        [When(@"I click the submit button in the contact us form")]
        public void WhenIClickTheSubmitButtonInTheContactUsForm()
        {
            TestBase.ScrollToElement(_contactUsSection.SubmitButton);
            _contactUsSection.SubmitButton.Click();
        }



        [Then(@"I should be told that the form was submitted")]
        public void ThenIShouldBeToldThatTheFormWasSubmitted()
        {
            Thread.Sleep(2000);

            Assert.Equal(String.Format("Thanks for getting in touch {0}!", _testData.MyContactUsMessage.Name), _contactUsSection.HeaderText.Text);

            string paragraph = "";
            foreach (IWebElement p in _contactUsSection.ParagraphText)
                paragraph += p.Text + " ";

            Assert.Equal(String.Format("We'll get back to you about {0} as soon as possible. ", _testData.MyContactUsMessage.Subject), paragraph);
        }

        [Then(@"I should see the message '(.*)'")]
        public void ThenIShouldSeeTheMessage(string expectedMessage)
        {
            Assert.Contains(expectedMessage, _contactUsSection.getListOfWarningMessages());
        }

    }
}