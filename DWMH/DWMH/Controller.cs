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
                    case 2:
                        Add();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        private void Add()
        {
            ConsoleIO.DisplayLine("*** Add Reservation ***");

            //get the host list and resrvations
            Result<List<Reservation>> reservationsResult = GetReservations();

            ConsoleIO.DisplayStatus(reservationsResult.Success, reservationsResult.Messages);

            if (reservationsResult.Success)
            {
                ConsoleIO.DisplayReservationList(reservationsResult.Value);
            }
            else
            {
                return;
            }

            //instantiate a host
            Host host = reservationsResult.Value.FirstOrDefault().Host;

            //get guest & instantiate
            string guestEmail = ConsoleIO.PromptString("Enter guest email", false);
            Result<Guest> guestResult = reservationService.FindGuestByEmail(guestEmail);


            if (!guestResult.Success)
            {
                ConsoleIO.DisplayStatus(guestResult.Success, guestResult.Messages);
                return;
            }
            else
            {
                ConsoleIO.DisplayLine("Guest found");
                ConsoleIO.DisplayLine("");
            }
            Guest guest = guestResult.Value;

            //get start date (required)
            DateTime startDate = ConsoleIO.PromptDateTime("Reservation start date", false);
            //get end date (required)
            DateTime endDate = ConsoleIO.PromptDateTime("Reservation end date", false);

            //perform Create and display result
            Reservation toAdd = new Reservation
            {
                StartDate = startDate,
                EndDate = endDate,
                Guest = guest,
                Host = host
            };
            toAdd.SetTotal();

            ConsoleIO.DisplayReservationSummary(toAdd);
            if (!ConsoleIO.PromptYesNo())
            {
                ConsoleIO.DisplayLine("Reservation was not added");
                return;
            }

            Result<Reservation> createResult = reservationService.Create(toAdd);

            ConsoleIO.DisplayStatus(createResult.Success, createResult.Messages);

            if (createResult.Success)
            {
                ConsoleIO.DisplayLine($"Reservation with ID {createResult.Value.ID} created");
            }
        }

        private void View()
        {
            ConsoleIO.DisplayLine("*** View Reservations ***");

            Result<List<Reservation>> result = GetReservations();

            ConsoleIO.DisplayStatus(result.Success, result.Messages);

            if (result.Success)
            {
                ConsoleIO.DisplayReservationList(result.Value);
            }
        }

        private Result<List<Reservation>> GetReservations()
        {
            string searchTerm = ConsoleIO.PromptString("Enter host email", false);
            Host host = new Host
            {
                Email = searchTerm
            };

            Result<List<Reservation>> result = reservationService.ViewByHost(host);
            return result;
        }
    }
}
