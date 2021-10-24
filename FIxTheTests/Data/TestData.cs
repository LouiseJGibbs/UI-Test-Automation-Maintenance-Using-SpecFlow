using FixTheTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixTheTests.Data
{
    public class TestData
    {
        public ContactUsMessage MyContactUsMessage;
        public RoomBooking MyRoomBooking;
        public Room MyRoom;

        public TestData()
        {
            MyContactUsMessage = new ContactUsMessage();
            MyRoomBooking = new RoomBooking();
            MyRoom = new Room();
        }
    }
}