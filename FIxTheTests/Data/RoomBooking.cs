using System;
using System.Collections.Generic;
using System.Text;

namespace FixTheTests
{
    public class RoomBooking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageSubject = "You have a new booking!";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}