using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public class Guest
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int LengthOfStay { get; private set; }

        public Guest(string first, string last, int days)
        {
            FirstName = first;
            LastName = last;
            LengthOfStay = days;
        }
    }

}
