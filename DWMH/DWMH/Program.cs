using System;
using DWMH.Core;
using DWMH.Core.Repos;
using DWMH.DAL;
using DWMH.BLL;
using System.IO;

namespace DWMH
{
    public class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string guestsFilePath = Path.Combine(projectDirectory, "guests.csv");
            string hostsFilePath = Path.Combine(projectDirectory, "hosts.csv");
            string reservationsDirectory = Path.Combine(projectDirectory, "reservations");

            IGuestRepository guestRepository = new GuestFileRepository(guestsFilePath);
            IHostRepository hostRepository = new HostFileRepository(hostsFilePath);
            IReservationRepository reservationRepository = new ReservationFileRepository(reservationsDirectory);

            ReservationService service = new ReservationService(reservationRepository, guestRepository, hostRepository);
            Controller controller = new Controller(service);

            controller.Run();
        }
    }
}
