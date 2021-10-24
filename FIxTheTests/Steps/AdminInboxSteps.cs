using FixTheTests;
using FixTheTests.Data;
using FixTheTests.Page;
using OpenQA.Selenium;
using RestfulBookerSpecflowUITests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace RestfulBookerSpecflowUITests
{
    [Binding]
    public sealed class AdminInboxSteps
    {
        public TestData _testData;

        AdminInboxPage _adminInboxPage = new AdminInboxPage();

        public AdminInboxSteps(TestData testData)
        {
            _testData = testData;
        }

        [When(@"I view the admin email inbox")]
        public void WhenViewTheEmailInbox()
        {
            _adminInboxPage.OpenAdminInbox();
        }

        [Then(@"I can see the message in the list of unread messages")]
        public void ThenICanSeeTheNameAndSubjectInTheListOfUnreadMessages()
        {
            bool found = _adminInboxPage.CheckForMessageInUnreadMessages(_testData.MyContactUsMessage.Name, _testData.MyContactUsMessage.Subject);

            Assert.True(found, "The message could not be found in the list of unread messages");
        }

        [Then(@"I can see the message in the list of read messages")]
        public void ThenICanSeeTheNameAndSubjectInTheListOdReadMessages()
        {
            bool found = _adminInboxPage.CheckForMessageInReadMessages(_testData.MyContactUsMessage.Name, _testData.MyContactUsMessage.Subject);

            Assert.True(found, "The message could not be found in the list of read messages");
        }

        [When(@"I open the message from the admin inbox")]
        public void WhenIClickOnTheMessage()
        {
            _adminInboxPage.OpenMessage(_testData.MyContactUsMessage.Name, _testData.MyContactUsMessage.Subject);
        }

        [Then(@"the message window should appear containing the expected message details")]
        public void ThenDetailsOfTheMessageCanBeViewed()
        {
            Assert.True(_adminInboxPage.CheckMessageWindowIsOpen(), "The message window is not available");

            Assert.True(_adminInboxPage.CheckMessageDetailsSectionContainsExpectedText("PhoneNumber", _testData.MyContactUsMessage.PhoneNumber), "The phone number was not found in the expected location");
            Assert.True(_adminInboxPage.CheckMessageDetailsSectionContainsExpectedText("Name", _testData.MyContactUsMessage.Name), "The name was not found in the expected location");
            Assert.True(_adminInboxPage.CheckMessageDetailsSectionContainsExpectedText("Email", _testData.MyContactUsMessage.Email), "The email was not found in the expected location");
            Assert.True(_adminInboxPage.CheckMessageDetailsSectionContainsExpectedText("Subject", _testData.MyContactUsMessage.Subject), "The subject was not found in the expected location");
            Assert.True(_adminInboxPage.CheckMessageDetailsSectionContainsExpectedText("Message", _testData.MyContactUsMessage.Message), "The message was not found in the expected location");
        }

    }
}

