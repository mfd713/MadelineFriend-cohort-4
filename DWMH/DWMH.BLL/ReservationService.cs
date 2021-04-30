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
            Result<Reservation> result = Validate(reservation);
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Create(reservation);

            return result;
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
                if (reservation.Total == 0)
                { 
                    reservation.SetTotal(); 
                }
            }

            result.Value = reservations.OrderBy(r => r.StartDate).ToList();
            return result;

        }

        public Result<Guest> FindGuestByEmail(string email)
        {
            Result < Guest > result = new Result<Guest>();

            Guest guest = guestRepo.ReadByEmail(email);

            if(guest == null)
            {
                result.AddMessage("guest not found");
            }
            else
            {
                result.Value = guest;
            }

            return result;

        }

        public Result<Reservation> Update(int id, Reservation reservation)
        {

            Result<Reservation> result = Validate(reservation);
            if (!result.Success)
            {
                return result;
            }

            //IDs must match
            if(id != reservation.ID)
            {
                result.AddMessage("ID must match reservation to be updated");
                return result;
            }

            //call repo's update function
            result.Value = reservationRepo.Update(id, reservation);

            //return result; if the Reservation was null, the ID was not found
            if(result.Value == null)
            {
                result.AddMessage($"no reservation with ID {id} found");
            }

            return result;
            

        }

        public Result<Reservation> Delete(Reservation reservation)
        {
            //create result
            Result<Reservation> result = new Result<Reservation>();

            //host cannot be null
            if(reservation.Host == null)
            {
                result.AddMessage("Host cannot be null");
                return result;
            }
            //dates cannot be past
            if(reservation.StartDate < DateTime.Now || reservation.EndDate < DateTime.Now)
            {
                result.AddMessage("Cannot cancel past reservation");
                return result;
            }
            //run delete on repo
            //result takes in value
            result.Value = reservationRepo.Delete(reservation);

            //if value null, add ID issue to messages list
            if (result.Value == null)
            {
                result.AddMessage($"result with ID {reservation.ID} not found");
            }

            return result;
        }

        private Result<Reservation> Validate(Reservation reservation)
        {
            Result<Reservation> result = ValidateNulls(reservation);
            if (!result.Success)
            {
                return result;
            }

            ValidateDates(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateChildren(reservation, result);

            return result; ;
        }

        private Result<Reservation> ValidateNulls(Reservation reservation)
        {
            Result<Reservation> result = new Result<Reservation>();

            if(reservation == null)
            {
                result.AddMessage("no reservation to save");
                return result;
            }

            if(reservation.Host == null)
            {
                result.AddMessage("host is required");
            }
            
            if(reservation.Guest == null)
            {
                result.AddMessage("guest is required");
            }

            return result;
        }

        private void ValidateDates(Reservation reservation, Result<Reservation> result)
        {
            //must have start date
            if (reservation.StartDate.Ticks == 0)
            {
                result.AddMessage("start date required");
                return;
            }
            //must have end date
            if (reservation.EndDate.Ticks == 0)
            {
                result.AddMessage("end date required");
                return;
            }
            //start date must be future
            if(reservation.StartDate < DateTime.Now)
            {
                result.AddMessage("start date must be in the future");
                return;
            }
            //start date must be before end date
            if(reservation.StartDate >= reservation.EndDate)
            {
                result.AddMessage("start date must be before end date");
                return;
            }

            List<Reservation> fullList = reservationRepo.ReadByHost(reservation.Host);

            //check overlaps
            foreach (var existingRes in fullList)
            {
                //prevents an incorrect fail in case where updating will shift to dates that would overlap with the old reservation
                if(existingRes.ID == reservation.ID)
                {
                    continue;
                }
                //startExisting <= endNew && endNew <= endExisting
                if (existingRes.StartDate <= reservation.EndDate && reservation.EndDate <= existingRes.EndDate)
                { 
                    result.AddMessage("end date cannot be in the during an existing reservation");
                    return;
                }

                //startExisting <= startNew && startNew<= endExisting
                if (existingRes.StartDate <= reservation.StartDate && reservation.StartDate <= existingRes.EndDate)
                {
                    result.AddMessage("start date cannot be during an existing reservation");
                    return;
                }
                // startNew <= startExisting && endNew >= endExisting
                if
                (reservation.StartDate <= existingRes.StartDate && reservation.EndDate >= existingRes.EndDate)
                {
                    result.AddMessage("new reservation dates cannot contain an existing reservation");
                    return;
                }
            }
        }

        private void ValidateChildren(Reservation reservation, Result<Reservation> result)
        {
            if(reservation.Host.ID == null ||
                !hostRepo.ReadAll().Contains(reservation.Host))
            {
                result.AddMessage("host not found");
            }

            if(reservation.Guest.ID ==0 ||
                !guestRepo.ReadAll().Contains(reservation.Guest))
            {
                result.AddMessage("guest not found");
            }
        }
    }

}
