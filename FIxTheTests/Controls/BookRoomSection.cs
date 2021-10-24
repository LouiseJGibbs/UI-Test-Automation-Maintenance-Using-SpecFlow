using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using FixTheTests.Data;
using System.Collections.Generic;

namespace FixTheTests.Page
{
    public class BookRoomSection
    {
        //Buttons
        public IWebElement BookThisRoomButton => TestBase.Driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right openBooking']"));
        public IWebElement BookButton => TestBase.Driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right book-room']"));
        public IWebElement CancelButton => TestBase.Driver.FindElement(By.XPath("//button[@class='btn btn-outline-danger float-right book-room']"));

        //Room details
        public ReadOnlyCollection<IWebElement> RoomInformationList => TestBase.Driver.FindElements(By.XPath("//div[@class='row hotel-room-info']"));

        //Calendar
        public IWebElement Calendar => TestBase.Driver.FindElement(By.XPath("//div[@class='rbc-calendar']"));
        public ReadOnlyCollection<IWebElement> CalendarWeeks => Calendar.FindElements(By.XPath(".//div[@class='rbc-month-row']"));
        public ReadOnlyCollection<IWebElement> CalendarDays => Calendar.FindElements(By.XPath(".//div[@class='rbc-date-cell']"));
        public By DaysInWeek => By.XPath(".//div[@class='rbc-date-cell']");
        public IWebElement CurrentMonth => TestBase.Driver.FindElement(By.XPath("//span[@class='rbc-toolbar-label']"));

        //Book room form
        public IWebElement FirstNameTextbox => TestBase.Driver.FindElement(By.Name("firstname"));
        public IWebElement LastNameTextbox => TestBase.Driver.FindElement(By.Name("lastname"));
        public IWebElement EmailTextbox => TestBase.Driver.FindElement(By.Name("email"));
        public IWebElement PhoneTextbox => TestBase.Driver.FindElement(By.Name("phone"));
        public IWebElement SubmitBookingButton => TestBase.Driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right book-room']"));
        public IWebElement NextMonth => TestBase.Driver.FindElement(By.XPath("//button[text()='Next']"));


        //Successful booking message
        public IWebElement SuccessfulBookingMessageHeader => TestBase.Driver.FindElement(By.XPath("//div[@class='form-row']//h3"));
        public ReadOnlyCollection<IWebElement> SuccessfulBookingMessageText => TestBase.Driver.FindElements(By.XPath("//div[@class='form-row']//p"));


        //Warning messages when booking was not successful
        public ReadOnlyCollection<IWebElement> WarningMessagesList => TestBase.Driver.FindElements(By.XPath("//div[@class='alert alert-danger']/p"));

        private Data.TestData _testData;

        public BookRoomSection(Data.TestData testData)
        {
            _testData = testData;
        }

        public int CountRooms()
        {
            return RoomInformationList.Count;
        }

        public void ClickBookThisRoomButton()
        {
            BookThisRoomButton.Click();
        }

        public void ClickBookButton()
        {
            BookButton.Click();
        }

        public void ClickCancelButton()
        {
            CancelButton.Click();
        }

        public bool CheckBookThisRoomButtonExists()
        {
            return BookThisRoomButton.Exists();
        }

        public bool CheckBookRoomButtonExists()
        {
            try
            {
                return BookButton.Exists();
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }

        public bool CheckCancelButtonExists()
        {
            return CancelButton.Exists();
        }

        public string GetBookThisRoomButtonText()
        {
            return BookThisRoomButton.Text;
        }

        public string GetBookRoomButtonText()
        {
            return BookButton.Text;
        }

        public string GetCancelButtonText()
        {
            return CancelButton.Text;
        }

        public void SelectFirstAvailableWeek()
        {
            bool dateSet = false;

            while (!dateSet)
            {
                foreach (IWebElement week in CalendarWeeks)
                {
                    TestBase.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    if (CheckAvailable(week) && week.FindElements(DaysInWeek).Count > 1)
                    {
                        TestBase.ScrollToElement(week);
                        SelectDateRangeInWeek(week);
                        dateSet = true;
                        TestBase.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestBase.Timeout);
                        break;
                    }
                }

                NextMonth.Click();
            }
        }

        public void SelectFirstAvailableRangeOfDays(int lengthOfStay)
        {
            bool dateSet = false;
            bool available = false;

            while (!dateSet)
            {
                foreach (IWebElement day in CalendarDays)
                {
                    int index = CalendarDays.IndexOf(day);

                    TestBase.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    if (CheckAvailable(day))
                    {
                        for (int i = 0; i < lengthOfStay; i++)
                        {
                            if (!CheckAvailable(CalendarDays[index + i]))
                            {
                                available = false;
                                break;
                            }
                        }
                    }

                    TestBase.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestBase.Timeout);

                    if (available)
                    {
                        TestBase.ScrollToElement(day);
                        SelectDateRange(day, CalendarDays[index + lengthOfStay]);
                        dateSet = true;
                        break;
                    }
                }

                NextMonth.Click();
            }
        }

        public bool CheckAvailable(IWebElement week)
        {
            bool available = false;

            try
            {
                available = !week.FindElement(By.XPath(".//div[@title='Unavailable']")).Displayed;
            }
            catch (NoSuchElementException)
            {
                available = true;
            }

            return available;
        }

        public void SelectDateRangeInWeek(IWebElement week)
        {
            ReadOnlyCollection<IWebElement> days = week.FindElements(DaysInWeek);
            IWebElement day1 = days[0];
            IWebElement day2 = days[days.Count - 1];


            int year = Convert.ToInt32(CurrentMonth.Text.Remove(0, CurrentMonth.Text.Length - 4));
            int month = DateTime.ParseExact(CurrentMonth.Text.Replace(" ", "").Replace(year.ToString(), ""), "MMMM", CultureInfo.CurrentCulture).Month;

            _testData.MyRoomBooking.StartDate = new DateTime(year, month, Convert.ToInt16(day1.Text));
            _testData.MyRoomBooking.EndDate = new DateTime(year, month, Convert.ToInt16(day2.Text));

            SelectDateRange(day1, day2);
        }

        public void SelectDateRange(IWebElement day1, IWebElement day2)
        {
            var action = new Actions(TestBase.Driver);

            action.ClickAndHold(day2);
            action.MoveToElement(day2);
            action.MoveToElement(day1);
            action.DragAndDrop(day1, day2);
            action.Perform();
        }

        public string GetSuccessfulBookingMessageHeader()
        {
            return SuccessfulBookingMessageHeader.Text;
        }

        public string GetSuccessfulBookingMessageParagraphText()
        {
            string paragraphText = "";

            foreach (IWebElement element in SuccessfulBookingMessageText)
            {
                paragraphText += " " + element.Text;
            }

            return paragraphText;
        }

        public int CountWarningMessages()
        {
            return GetListOfWarningMessages().Count;
        }


        public List<string> GetListOfWarningMessages()
        {
            List<string> WarningList = new List<string>();

            foreach(IWebElement element in WarningMessagesList)
            {
                WarningList.Add(element.Text);
            }

            return WarningList;
        }

        public string GetRoomType(int index)
        {
            return TestBase.Driver.FindElements(By.XPath("//h3"))[index].Text;
        }

        public bool IsRoomAccessible(int index)
        {
            try
            {
                return TestBase.Driver.FindElement(By.XPath("//span[@class='fa fa-wheelchair wheelchair']")).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetRoomDetails(int index)
        {
            return TestBase.Driver.FindElements(By.XPath("//ul"))[index].Text;
        }

    }
}