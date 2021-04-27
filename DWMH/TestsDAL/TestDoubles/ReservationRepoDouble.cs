using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;
using DWMH.Core.Exceptions;

namespace TestsDAL.TestDoubles
{
    public class ReservationRepoDouble : IReservationRepository
    {
        private List<Reservation> _reservations = new List<Reservation>();

        public ReservationRepoDouble()
        {
            Guest guest = new Guest
            {
                LastName = "Testington",
                FirstName = "Tesla",
                Email = "tt@gmail.com",
                ID = 1
            };

            Host host = new Host
            {
                LastName = "Testy1",
                ID = "abc-123",
                Email = "test1@gmail.com",
                City = "Chicago",
                State = "IL",
            };
            host.SetRates(50M, 80M);

            Reservation reservation = new Reservation(new DateTime(2020, 1, 1), new DateTime(2020, 1, 8),
                host, guest);
            reservation.ID = 1;


            _reservations.Add(reservation);
        }
        public Reservation Create(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation Delete(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ReadByHost(Host host)
        {
            List<Reservation> result = new List<Reservation>();

            try
            {
                result = _reservations.Where(r => r.Host.Email == host.Email).ToList();
            }catch(Exception e)
            {
                throw new RepositoryException("cannot read reservation list", e);
            }
            
            return result;
        }

        public Reservation Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
