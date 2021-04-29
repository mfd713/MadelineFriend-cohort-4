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

            Reservation reservation = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guest
            };
            

            reservation.SetTotal();
            reservation.ID = 1;


            _reservations.Add(reservation);
        }
        public Reservation Create(Reservation reservation)
        {
            //set reservation ID
            int maxID = _reservations.Max(r => r.ID) + 1;
            reservation.ID = maxID;

            //add it to the list
            _reservations.Add(reservation);

            //return added reservation with ID
            return reservation;
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

        public Reservation Update(int id, Reservation reservation)
        {
            Reservation result = null;
            foreach (var existingReservation in _reservations)
            {
                if(existingReservation.ID == id)
                {
                    existingReservation.StartDate = reservation.StartDate;
                    existingReservation.EndDate = reservation.EndDate;
                    existingReservation.Guest = reservation.Guest;
                    existingReservation.Host = reservation.Host;
                    existingReservation.SetTotal();

                    result = existingReservation;
                    return result;
                }
            }

            return result;
        }
    }
}
