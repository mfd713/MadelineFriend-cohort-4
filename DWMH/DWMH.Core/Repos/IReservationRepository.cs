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
        public List<Reservation> ReadByHost(Host host);
        public Reservation Update(int id, Reservation reservation);
        public Reservation Delete(Reservation reservation);
    }
}
