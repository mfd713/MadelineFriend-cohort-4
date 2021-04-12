using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public class Guest
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CheckInTime { get; private set; }

        public Guest(string first, string last, DateTime time)
        {
            FirstName = first;
            LastName = last;
            CheckInTime = time;
        }
    }

}
