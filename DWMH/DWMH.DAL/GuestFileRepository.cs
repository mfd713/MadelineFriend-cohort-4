using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;

namespace DWMH.DAL
{
    public class GuestFileRepository : IGuestRepository
    {
        public List<Guest> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Guest ReadByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
