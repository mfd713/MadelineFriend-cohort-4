using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;

namespace TestsDAL.TestDoubles
{
    public class GuestRepoDouble : IGuestRepository
    {
        private List<Guest> _guests = new List<Guest>();

        public GuestRepoDouble()
        {
            Guest guest = new Guest
            {
                LastName = "Testington",
                FirstName = "Tesla",
                Email = "tt@gmail.com",
                ID = 1
            };

            _guests.Add(guest);
        }
        public List<Guest> ReadAll()
        {
            return _guests;
        }

        public Guest ReadByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
