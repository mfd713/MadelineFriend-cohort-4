using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;

namespace DWMH.DAL
{
    public class ReservationFileRepository : IReservationRepository
    {
        public Reservation Create(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation Delete(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ReadByHost(string email)
        {
            throw new NotImplementedException();
        }

        public Reservation Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
