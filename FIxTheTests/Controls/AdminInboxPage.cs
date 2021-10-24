using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using FixTheTests.Data;
using System.Collections.Generic;
using Xunit;

namespace FixTheTests.Page
{
    public class AdminInboxPage
    {
        public IWebElement MessagesInboxLink => TestBase.Driver.FindElement(By.XPath("//i[@class='fa fa-inbox']"));
        public IWebElement Messages => TestBase.Driver.FindElement(By.XPath("//div[@class='messages']"));
        public ReadOnlyCollection<IWebElement> MessagesList_All => Messages.FindElements(By.XPath(".//div[starts-with(@class,'row detail')]"));
        public ReadOnlyCollection<IWebElement> MessagesList_Read => Messages.FindElements(By.XPath(".//div[@class='row detail read-true']"));
        public ReadOnlyCollection<IWebElement> MessagesList_Unread => Messages.FindElements(By.XPath(".//div[@class='row detail read-false']"));
        public IWebElement MessageFromList;


        public By MessageWindowBy = By.XPath("//div[@class = 'ReactModal__Content ReactModal__Content--after-open message-modal']");
        public IWebElement MessageWindow => TestBase.Driver.FindElement(MessageWindowBy);
        public ReadOnlyCollection<IWebElement> MessageDetails => MessageWindow.FindElements(By.XPath("./div[@class='form-row']"));

        public void OpenAdminInbox()
        {
            MessagesInboxLink.Click();
        }

        public bool CheckForMessageInUnreadMessages(string senderName, string messageSubject)
        {
            bool found = false;

            //Newer messages are more likely to appear at the end of the list, 
            //Start with final message first to speed up time test is run

            for (int i = MessagesList_Unread.Count; i > 0; i--)
            {
                IWebElement messageInAdminInbox = MessagesList_Unread[i - 1];
                string messageInAdminInboxText = messageInAdminInbox.Text;

                if(messageInAdminInboxText.Contains(senderName) && messageInAdminInboxText.Contains(messageSubject))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        public bool CheckForMessageInReadMessages(string senderName, string messageSubject)
        {
            bool found = false;

            //Newer messages are more likely to appear at the end of the list, 
            //Start with final message first to speed up time test is run

            for (int i = MessagesList_Read.Count; i > 0; i--)
            {
                IWebElement messageInAdminInbox = MessagesList_Read[i - 1];
                string messageInAdminInboxText = messageInAdminInbox.Text;

                if (messageInAdminInboxText.Contains(senderName) && messageInAdminInboxText.Contains(messageSubject))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        public void OpenMessage(string senderName, string messageSubject)
        {
            //Newer messages are more likely to appear at the end of the list, 
            //Start with final message first to speed up time test is run

            for (int i = MessagesList_All.Count; i > 0; i--)
            {
                IWebElement messageInAdminInbox = MessagesList_All[i - 1];
                string messageInAdminInboxText = messageInAdminInbox.Text;

                if (messageInAdminInboxText.Contains(senderName) && messageInAdminInboxText.Contains(messageSubject))
                {
                    messageInAdminInbox.Click();
                    break;
                }
            }
        }

        public bool CheckMessageWindowIsOpen()
        {
            return TestBase.Exists(MessageWindow); 
        }

        public bool CheckMessageDetailsSectionContainsExpectedText(string section, string expectedText)
        {
            string textSearchString = $".//*[text()='{expectedText}']";

            switch (section)
            {
                case "PhoneNumber":
                    return MessageDetails[0].FindElement(By.XPath(textSearchString)).Exists();
                case "Name":
                    return MessageDetails[0].FindElement(By.XPath(textSearchString)).Exists();
                case "Email":
                    return MessageDetails[1].FindElement(By.XPath(textSearchString)).Exists();
                case "Subject":
                    return MessageDetails[2].FindElement(By.XPath(textSearchString)).Exists();
                case "Message":
                    return MessageDetails[3].FindElement(By.XPath(textSearchString)).Exists();
                default:
                    Assert.True(false, "Invalid section");
                    break;
            }

            return false;
        }


    }
}
