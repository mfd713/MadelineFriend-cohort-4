using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.BLL;

namespace DWMH
{
    public class Controller
    {
        private ReservationService reservationService;

        public Controller(ReservationService reservationService) 
        {
            this.reservationService = reservationService;
        }

        public void Run()
        {
            do
            {
                ConsoleIO.DisplayLine("Main Menu\n=======");
                int menuChoice = ConsoleIO.PromptInt("0. Exit\n1. View Reservations\n2. Add a Reservation" +
                    "\n3. Update a Reservation\n4. Delete a Reservation" +
                    "\nEnter choice [0-4]", 0, 4);
                switch (menuChoice)
                {
                    case 1:
                        View();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        private void View()
        {
            ConsoleIO.DisplayLine("*** View Reservations ***");
            string searchTerm = ConsoleIO.PromptString("Enter host email", false);
            Host host = new Host
            {
                Email = searchTerm
            };

            Result<List<Reservation>> result = reservationService.ViewByHost(host);
            
            ConsoleIO.DisplayStatus(result.Success, result.Messages);

            if (result.Success)
            {
                ConsoleIO.DisplayReservationList(result.Value);
            }
        }
    }
}
