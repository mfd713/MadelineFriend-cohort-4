using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core.Repos
{
    public interface IReservationRepository
    {
        public Reservation Create(Reservation reservation);
        public List<Reservation> ReadByHost(string email);
        public Reservation Update(int id);
        public Reservation Delete(Reservation reservation);
    }
}
