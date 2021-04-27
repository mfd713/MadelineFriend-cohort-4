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

        public Result<Reservation> ViewByHost(string email)
        {
            throw new System.NotImplementedException();
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
