using System;
using System.Text;

namespace CapsuleHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            //run start menu to get guest array length
            int capacity = 0;

            const decimal PER_NIGHT_PRICE = 33.33M;

            capacity = ConsoleIO.StartupMenu();
            Capsule[] Hotel = new Capsule[capacity];

            for (int i = 0; i < capacity; i++)
            {
                Hotel[i] = new Capsule();
            }

            string[] fullList = { "Ada", "Bill", "Carson" };
            string[] longList = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };

            Console.WriteLine($"Ok! There are {Hotel.Length} capsules ready for booking.");
            ConsoleIO.AnyKeyToContinue();

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
                        if (CanCheckIn(Hotel))
                        {
                            int capsuleIndex = ConsoleIO.ReadInt($"Which capsule would you like to check a guest into? (1 - {Hotel.Length})", 1, Hotel.Length) -1 ;
                            if (Hotel[capsuleIndex].isOccupied)
                            {
                                Console.WriteLine("That capsule is already occupeid! Taking you back to the menu...");
                                ConsoleIO.AnyKeyToContinue();
                                break;
                            }
                            Guest aGuest = ConsoleIO.GetGuestInfo();
                            if (Hotel[capsuleIndex].CheckIn(aGuest))
                            {
                                Console.WriteLine($"{aGuest.FirstName} {aGuest.LastName} was successfully checked in");
                                ConsoleIO.AnyKeyToContinue();
                            }
                            else
                            {
                                ConsoleIO.PrintErrorMessage();
                            }
                        }
                        break;
                    case 3:
                        if (CanCheckOut(Hotel))
                        {
                            int capsuleIndex = ConsoleIO.ReadInt($"Which capsule would you like to check a guest out of? (1 - {Hotel.Length})", 1, Hotel.Length) - 1;
                            Guest aGuest = Hotel[capsuleIndex].CheckOut();
                            if (aGuest != null)
                            {
                                Console.WriteLine($"{aGuest.FirstName} {aGuest.LastName} was successfully checked out. The cost was {aGuest.LengthOfStay*PER_NIGHT_PRICE:C}");
                                ConsoleIO.AnyKeyToContinue();
                            }
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
        /// Checks if the strin in a specified array spot is filled. Returns true if filled, false if empty
        /// </summary>
        /// <param name="arr">string[] guestList</param>
        /// <param name="spot">string[] spotNumber</param>
        /// <returns>bool filled/not filled</returns>
        static bool IsCapsuleOccupied(string[] arr, int spot)
        {
            return !(string.IsNullOrEmpty(arr[spot]));
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
                if (item.isOccupied == false)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Checks if there are any open spaces in a Capsule array to add a guest
        /// </summary>
        /// <param name="arr">Array of Capsules</param>
        /// <returns>True if there is at least 1 space to check in, false if not</returns>
        public static bool CanCheckIn(Capsule[] arr)
        {
            //confirm at least 1 capsule available
            if (CountNullOrEmpties(arr) == 0)
            {
                Console.WriteLine("The guest list is full. please remove a guest before adding a new one.");
                ConsoleIO.AnyKeyToContinue();
                return false;
            }

            return true;
        }


        /// <summary>
        /// Checks if there are any guests in a Capsule array
        /// </summary>
        /// <param name="arr">Array of Capsules</param>
        /// <returns>True if there is at least 1 capsule spot containing a guest, false if not</returns>
        public static bool CanCheckOut(Capsule[] arr)
        {
            //check there are guests entered
            if (CountNullOrEmpties(arr) == arr.Length)
            {
                Console.WriteLine("There are no guests to check out! Taking you back to the main menu.");
                ConsoleIO.AnyKeyToContinue();
                return false;
            }
            return true;
        }
        
        
//prompt user for capsule number to check out
//capsuleIndex = ReadInt($"Enter the capsule number you want to check out (1 - {arr.Length})", 1, arr.Length) - 1;
}
}
