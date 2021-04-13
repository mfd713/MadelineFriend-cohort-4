using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public class HotelWorkflow
    {

        public void Run()
        {
            //instantiate hotel
            List<Capsule> Hotel = new List<Capsule>();
            //greet w/ startup menu
            ConsoleIO.StartupMenu();


            //main menu
            do
            {
                Console.Clear();
                Console.WriteLine($"Main Menu" +
                    $"\n1. View Guest List" +
                    $"\n2. Check In a Guest" +
                    $"\n3. Check Out a Guest" +
                    $"\n4. Exit");

                int menuChoice = ConsoleIO.ReadInt("Please enter the number of the menu item you wish to do");
                //switch statement starts the method of the menu item the user chooses
                switch (menuChoice)
                {
                    case -1:
                        break;
                    case 1:
                        ConsoleIO.ViewGuestList(Hotel);
                        break;
                    case 2:
                        //This hotel has infinite space! Create a new capsule, take in guest info, and slap them on the end of the List
                        Hotel.Add(new Capsule());
                        Guest aGuest = ConsoleIO.GetGuestInfo();
                        if (Hotel[Hotel.Count-1].CheckIn(aGuest))
                        {
                            Console.WriteLine($"{aGuest.FirstName} {aGuest.LastName} was successfully checked in");
                            ConsoleIO.AnyKeyToContinue();
                        }
                        else
                        {
                            ConsoleIO.PrintErrorMessage();
                        }               
                        break;
                    case 3:
                        if (CanCheckOut(Hotel))
                        {
                            int capsuleIndex = ConsoleIO.ReadInt($"Which capsule would you like to check a guest out of? (1 - {Hotel.Count})", 1, Hotel.Count) - 1;
                            Capsule toRemove = Hotel[capsuleIndex];

                            do
                            {
                                aGuest = toRemove.CheckOut();
                            } while (aGuest == null);
                            Hotel.RemoveAt(capsuleIndex);

                            ConsoleIO.AnyKeyToContinue();
                        }
                        break;
                    case 4:
                        if (CheckExit().Contains("y"))
                        {
                            return;
                        }
                        else
                        {
                            break;
                        }
                }

            } while (true);//will not exit until user chooses exit option
        }

        /// <summary>
        /// Confirms that the user really wants to exit, and exits if they do. Otherwise returns to main menu
        /// </summary>
        /// <returns>string yesNo</returns>
        static string CheckExit()
        {
            string yesNo = ConsoleIO.ReadString("Are you sure you want to exit? All data wil be lost! (y/n)");
            return yesNo;
        }


        /// <summary>
        /// Counts the number of null/empty strings in an array of strings
        /// </summary>
        /// <param name="arr">array of strings</param>
        /// <returns>int count of empties</returns>
        public static int CountNullOrEmpties(Capsule[] arr)
        {
            int count = 0;
            foreach (Capsule item in arr)
            {
                if (item.IsOccupied == false)
                {
                    count++;
                }
            }
            return count;
        }


        /// <summary>
        /// Checks if there are any guests in a Capsule array
        /// </summary>
        /// <param name="hotel">Array of Capsules</param>
        /// <returns>True if there is at least 1 capsule spot containing a guest, false if not</returns>
        public static bool CanCheckOut(List<Capsule> hotel)
        {
            //check there are guests entered
            if (hotel.Count==0)
            {
                Console.WriteLine("There are no guests to check out! Taking you back to the main menu.");
                ConsoleIO.AnyKeyToContinue();
                return false;
            }
            return true;
        }
    }
}
