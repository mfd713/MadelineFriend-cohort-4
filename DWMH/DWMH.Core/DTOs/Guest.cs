using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core
{
    public class Guest
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Email { get; set; }

        public int ID { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Guest guest &&
                   LastName == guest.LastName &&
                   FirstName == guest.FirstName &&
                   Email == guest.Email &&
                   ID == guest.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, FirstName, Email, ID);
        }
    }
}
