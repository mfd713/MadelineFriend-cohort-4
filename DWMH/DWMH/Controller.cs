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
                    "\n3. Update a Reservation\n4. Cancel a Reservation" +
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
                    case 3:
                        Update();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    case 4:
                        Cancel();
                        ConsoleIO.AnyKeyToContinue();
                        break
                    default:
                        break;
                }
            } while (true);
        }

        private void Cancel()
        {
            //when getting list to show, query to only show future dates
        }

        private void Update()
        {
            ConsoleIO.DisplayLine("*** Update Reservation ***");

            //get host list and reservations
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

            //get id to edit
            Reservation original = null;
            do
            {
                int idToEdit = ConsoleIO.PromptInt("Enter the ID of the Reservation you want to edit");
                original = reservationsResult.Value.Find(r => r.ID == idToEdit);
            } while (original == null);
           
            ConsoleIO.DisplayLine($"\n** Editing Reservation {original.ID} **");

            //prompt new start date (or enter to keep)
            DateTime newStart = ConsoleIO.PromptDateTime($"New start ({original.StartDate:d})", true);
            //prompt new end date (or enter to keep)
            DateTime newEnd = ConsoleIO.PromptDateTime($"New end ({original.EndDate:d})", true);

            //instantiate a host
            Host host = original.Host;

            //set up Reservation, show summary and confirm correct
            Reservation toUpdate = new Reservation
            {
                Host = host,
                Guest = original.Guest
            };

            toUpdate.StartDate = newStart == default(DateTime) ? original.StartDate :  newStart; //if PromptDateTime returned the default,
                                                                                                //we know user wants it the same.
            toUpdate.EndDate = newEnd == default(DateTime) ? original.EndDate : newEnd;
            toUpdate.SetTotal();

            ConsoleIO.DisplayReservationSummary(toUpdate);
            if (!ConsoleIO.PromptYesNo())
            {
                ConsoleIO.DisplayLine("Reservation was not updated");
                return;
            }

            //perform Update and display result
            toUpdate.ID = original.ID;
            var result = reservationService.Update(original.ID, toUpdate);

            ConsoleIO.DisplayStatus(result.Success, result.Messages);
            if (result.Success)
            {
                ConsoleIO.DisplayLine($"Reservation with ID {result.Value.ID} was updated");
            }
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
