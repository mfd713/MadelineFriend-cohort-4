using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core.Repos;
using DWMH.Core;

namespace DWMH.BLL
{
    public class ReservationService
    {
        private IReservationRepository reservationRepo;
        private IGuestRepository guestRepo;
        private IHostRepository hostRepo;

        public ReservationService(IReservationRepository reservationRepository, IGuestRepository guestRepository,
            IHostRepository hostRepository)
        {
            reservationRepo = reservationRepository;
            guestRepo = guestRepository;
            hostRepo = hostRepository;
        }

        public Result<Reservation> Create(Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        public Result<List<Reservation>> ViewByHost(Host host)
        {
            //match the host with an ID
            Dictionary<string, Host> hostMap = hostRepo.ReadAll()
                .ToDictionary(i => i.ID);

            foreach (var hostID in hostMap)
            {
                if (hostID.Value.Email == host.Email)
                {
                    host.ID = hostID.Key;
                    break;
                }
            }
            //get list of reservations
            List<Reservation> reservations = reservationRepo.ReadByHost(host);

            Dictionary<int, Guest> guestMap = guestRepo.ReadAll()
                .ToDictionary(i => i.ID);

            //return false and null list if no reservations found
            Result<List<Reservation>> result = new Result<List<Reservation>>();
            if(reservations.Count == 0)
            {
                result.AddMessage("no reservations found for host");
            }

            foreach (var reservation in reservations)
            {
                reservation.Host = hostMap[host.ID];
                reservation.Guest = guestMap[reservation.Guest.ID];
            }

            result.Value = reservations;
            return result;

        }

        public Result<Reservation> Update(int id)
        {
            throw new System.NotImplementedException();
        }

        public Result<Reservation> Delete(Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        private Result<Reservation> Validate(Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        private Result<Reservation> ValidateNulls(string reservation)
        {
            throw new System.NotImplementedException();
        }

        private void ValidateFields(Reservation reservation, Result<Reservation> result)
        {
            throw new System.NotImplementedException();
        }

        private void ValidateChildren(Reservation reservation, string result)
        {
            throw new System.NotImplementedException();
        }
    }

}
