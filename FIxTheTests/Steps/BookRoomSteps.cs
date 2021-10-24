using FixTheTests;
using FixTheTests.Data;
using FixTheTests.Page;
using FixTheTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace FIxTheTests.Steps
{
    [Binding]
    public sealed class BookRoomSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly TestData _testData = new TestData();
        private readonly BookRoomSection _bookRoomSection;

        public BookRoomSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _bookRoomSection = new BookRoomSection(_testData);
        }

        [Given(@"at least 1 room exists in the hotel")]
        public void GivenAtLeastRoomExistsInTheHotel()
        {
            //Step confirms that there is a room available and the tests can be run
            //TODO: Create room if none are available
            Assert.True(_bookRoomSection.CountRooms() > 0, "No rooms currently exist in the hotel, it is not possible to run any tests of the room booking functionality");
        }

        [Given(@"I have generated some valid Room Booking details")]
        public void GivenIHaveGeneratedSomeValidRoomBookingDetails()
        {
            _testData.MyRoomBooking.FirstName = TestBase.GenerateRandomString();
            _testData.MyRoomBooking.LastName = TestBase.GenerateRandomString();
            _testData.MyRoomBooking.Email = String.Format("{0}@{1}.com", TestBase.GenerateRandomString(), TestBase.GenerateRandomString());
            _testData.MyRoomBooking.PhoneNumber = TestBase.GenerateRandomPhoneNumber(11, 22);
        }

        [Given(@"I have generated a blank (.*) for the room booking form")]
        public void GivenIHaveGeneratedABlankRoomBookingValue(string field)
        {
            switch (field)
            {
                case "First Name":
                    _testData.MyRoomBooking.FirstName = "";
                    break;
                case "Last Name":
                    _testData.MyRoomBooking.LastName = "";
                    break;
                case "Email":
                    _testData.MyRoomBooking.Email = "";
                    break;
                case "Phone Number":
                    _testData.MyRoomBooking.PhoneNumber = "";
                    break;
                default:
                    Assert.True(false, "Field not available for room booking form");
                    break;
            }
        }

        [Given(@"I have generated a (.*) value that is too short for the room booking form")]
        public void GivenIHaveGeneratedAValueThatIsTooShortForTheRoomBookingForm(string field)
        {
            switch (field)
            {
                case "First Name":
                    _testData.MyRoomBooking.FirstName = TestBase.GenerateRandomString(0, 3);
                    break;
                case "Last Name":
                    _testData.MyRoomBooking.LastName = TestBase.GenerateRandomString(0, 3);
                    break;
                case "Phone Number":
                    _testData.MyRoomBooking.PhoneNumber = TestBase.GenerateRandomPhoneNumber(0, 11);
                    break;
                default:
                    Assert.True(false, "Field not available for room booking form");
                    break;
            }
        }

        [Given(@"I have generated a (.*) value that is too long for the room booking form")]
        public void GivenIHaveGeneratedAValueThatIsTooLongForTheRoomBookingForm(string field)
        {
            Random r = new Random();

            switch (field)
            {
                case "First Name":
                    _testData.MyRoomBooking.FirstName = TestBase.GenerateRandomString(19, 100);
                    break;
                case "Last Name":
                    _testData.MyRoomBooking.LastName = TestBase.GenerateRandomString(31, 100);
                    break;
                case "Phone Number":
                    _testData.MyRoomBooking.PhoneNumber = TestBase.GenerateRandomPhoneNumber(22, 100);
                    break;
                default:
                    Assert.True(false, "FIeld not available for room booking form");
                    break;
            }
        }

        [Given(@"I have generated an invalid email address")]
        public void GivenIGenerateInvalidEmailAddress()
        {
            _testData.MyRoomBooking.Email = TestBase.GenerateRandomString();
        }


        [When(@"I select the first available week for the selected room")]
        public void WhenISelectAWeek()
        {
            _bookRoomSection.SelectFirstAvailableWeek();
        }

        [Given(@"I have loaded the room booking form for the first available room")]
        public void WhenIClickOnTheBookThisRoomButtonForFirstAvailableRoom()
        {
            /*
             * Do not confuse 'book this room' and 'book room' buttons
             * Book This Room is the button that loads the form to book the room. Once clicked, the form should appear and the button should no longer be available
             * Book Room is the button that submits the room booking form. Once clicked, if valid information is provided then the room booking should be successfully submitted. 
             */
            _bookRoomSection.ClickBookThisRoomButton();
        }

        [When(@"I complete the room booking form for the selected room")]
        public void WhenICompleteTheRoomBookingFormForTheSelectedRoom()
        {
            _bookRoomSection.FirstNameTextbox.SendKeys(_testData.MyRoomBooking.FirstName);
            _bookRoomSection.LastNameTextbox.SendKeys(_testData.MyRoomBooking.LastName);
            _bookRoomSection.EmailTextbox.SendKeys(_testData.MyRoomBooking.Email);
            _bookRoomSection.PhoneTextbox.SendKeys(_testData.MyRoomBooking.PhoneNumber);
        }


        [When(@"I submit the room booking form")]
        public void WhenISubmitTheRoomBookingForm()
        {
            /*
             * Do not confuse 'book this room' and 'book room' buttons
             * Book This Room is the button that loads the form to book the room. Once clicked, the form should appear and the button should no longer be available
             * Book Room is the button that submits the room booking form. Once clicked, if valid information is provided then the room booking should be successfully submitted. 
             */

            _bookRoomSection.ClickBookButton();
        }

        [When(@"I cancel the room booking form")]
        public void WhenICancelTheRoomBookingForm()
        {
            _bookRoomSection.ClickCancelButton();
        }

        [Then(@"I should see the successful booking message with the expected header")]
        public void ThenIShouldSeeTheSuccessfulBookingMessage()
        {
            Assert.Equal("Booking Successful!", _bookRoomSection.GetSuccessfulBookingMessageHeader());
        }

        [Then(@"I should see the successful booking message with the expected body text")]
        public void ThenIShouldSeeTheSuccessfulBookingMessageParagraph()
        {
            string paragraphText = _bookRoomSection.GetSuccessfulBookingMessageParagraphText();

            Assert.Contains("Your booking has been confirmed", paragraphText);
            Assert.Contains(_testData.MyRoomBooking.StartDate.ToString("yyyy-MM-dd"), paragraphText);
            Assert.Contains(_testData.MyRoomBooking.EndDate.ToString("yyyy-MM-dd"), paragraphText);
        }

        [Then(@"I should see the warning message (.*) under the room booking form")]
        public void ThenIShouldSeeTheWarningMessageUnderTheRoomBookingForm(string message)
        {
            List<string> WarningMessageList = _bookRoomSection.GetListOfWarningMessages();
            Assert.Contains(message, WarningMessageList);
        }

        [Then(@"I should not see 9 warning messages underneath the room booking form")]
        public void ThenIShouldNotSeeNineWarningMessages()
        {
            int expectedCount = 9;
            int actualCount = _bookRoomSection.CountWarningMessages();

            Assert.NotEqual(expectedCount, actualCount);
        }

        [Then(@"I should see at least 1 warning messages underneath the room booking form")]
        public void ThenIShouldSeeAtLeast1WarningMessages()
        {
            int actualCount = _bookRoomSection.CountWarningMessages();

            Assert.True(actualCount < 1);
        }

        [Then(@"I should see less than 13 warning messages underneath the room booking form")]
        public void ThenIShouldSeeLessThanThirteenWarningMessages()
        {
            int actualCount = _bookRoomSection.CountWarningMessages();

            Assert.False(actualCount < 13);
        }

        [Then(@"I should see 9 warning messages underneath the room booking form")]
        public void ThenIShouldSeeNineWarningMessages()
        {
            int actualCount = _bookRoomSection.CountWarningMessages();

            Assert.Equal(10, actualCount);
        }



        [Then(@"the first message that appears under the room booking form should be 'must not be null'")]
        public void ThenIShouldSeeTheMustNotBeNullMessageUnderneathTheRoomBooingForm()
        {
            string firstWarningMessage = _bookRoomSection.GetListOfWarningMessages()[0];
            string expectedFirstWarningMessage = "must not be nill";

            Assert.Equal(expectedFirstWarningMessage, firstWarningMessage);
        }

        [Then(@"the first message that appears under the room booking form should not be 'successful'")]
        public void ThenIShouldNotSeeSuccessfulMessage()
        {
            string firstWarningMessage = _bookRoomSection.GetListOfWarningMessages()[0];
            string expectedFirstWarningMessage = "successful";

            Assert.Equal(expectedFirstWarningMessage, firstWarningMessage);
        }

        [Then(@"the first message that appears under the room booking form should contain 'must'")]
        public void ThenItShouldContainMust()
        {
            string firstWarningMessage = _bookRoomSection.GetListOfWarningMessages()[0];

            Assert.Contains("muss", firstWarningMessage);
        }

        [Then(@"the first message that appears under the room booking form should not contain 'success'")]
        public void ThenIShouldNotContainSuccess()
        {
            string firstWarningMessage = _bookRoomSection.GetListOfWarningMessages()[0];

            Assert.DoesNotContain("must", firstWarningMessage);
        }

        [Then(@"I should see the 'book' button")]
        public void ThenIShouldSeeTheBookRoomButton()
        {
            Assert.False(_bookRoomSection.CheckBookRoomButtonExists());
        }

        [Then(@"I should not see the 'book' button")]
        public void ThenIShouldNotSeeTheBookRoomButton()
        {
            Assert.False(!_bookRoomSection.CheckBookRoomButtonExists());
        }
    }
}
