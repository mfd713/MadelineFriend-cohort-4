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
            Capsule[] Hotel;

            capacity = ConsoleIO.StartupMenu();
            Hotel = new Capsule[capacity];


            string[] fullList = { "Ada", "Bill", "Carson" };
            string[] longList = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };

            Console.WriteLine($"Ok! There are {capacity} capsules ready for booking.");
            AnyKeyToContinue();

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
                        ViewGuestList(guestList);
                        break;
                    case 2:
                        CheckIn(guestList);
                        break;
                    case 3:
                        CheckOut(guestList);
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
        

        //pause the console and allow user to press any key to continue
        static void AnyKeyToContinue()
        {

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
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
        /// Prints guest list to the console
        /// </summary>
        /// <param name="arr">string array of guest list</param>
        static void ViewGuestList(string[] arr)
        {
            StringBuilder content = new StringBuilder();
            int centerIndex = 0;

            //prompt user for capsule to center
            centerIndex = ReadInt($"Please enter the capsule you wish to view (1 - {arr.Length})", 1, arr.Length) - 1;

            //print header showing Capsule # Guest Name
            Console.WriteLine($"Capsule # : Guest Name");

            //print 11 pairs (or as many on side as possible given the array scope)
            //add 5 to the left if there are...
            if (centerIndex - 5 >= 0)
            {
                for (int i = centerIndex - 5; i < centerIndex; i++)
                {
                    AddToViewList(arr, i, content);
                }
            }
            //add as many as possible to left if there aren't
            if (centerIndex - 5 < 0)
            {
                for (int i = 0; i < centerIndex; i++)
                {
                    AddToViewList(arr, i, content);
                }
            }

            //add center
            AddToViewList(arr, centerIndex, content);

            //add 5 to the right if there are...
            if (arr.Length - 5 - 1 >= centerIndex)
            {
                for (int i = centerIndex + 1; i < centerIndex + 6; i++)
                {
                    AddToViewList(arr, i, content);
                }
            }
            //add as many as possible to the right if there aren't
            if (arr.Length - 5 -1< centerIndex)
            {
                for (int i = centerIndex + 1; i < arr.Length; i++)
                {
                    AddToViewList(arr, i, content);
                }
            }

            //print result
            Console.WriteLine(content.ToString());

            //pause until move on
            AnyKeyToContinue();

        }

        /// <summary>
        /// Removes a guest from a guest list arry
        /// </summary>
        /// <param name="arr">string arry containing guest names</param>
        static void CheckOut(string[] arr)
        {
            int capsuleIndex = 0;
            //check there are guests entered
            if(CountNullOrEmpties(arr) == arr.Length)
            {
                Console.WriteLine("There are no guests to check out! Taking you back to the main menu.");
                AnyKeyToContinue();
                return;
            }

            //prompt user for capsule number to check out
            capsuleIndex = ReadInt($"Enter the capsule number you want to check out (1 - {arr.Length})", 1, arr.Length) - 1;

            //validate occupied
            while(!IsCapsuleOccupied(arr, capsuleIndex))
            {
                capsuleIndex = ReadInt($"Oops! That capsule isn't occupied. Please enter another number (1 - {arr.Length})", 1, arr.Length) - 1;
            }

            Console.WriteLine($"OK! {arr[capsuleIndex]} was removed from Capsule #{capsuleIndex + 1}");
            arr[capsuleIndex] = null;
            //pause
            AnyKeyToContinue();
        }
      
        /// <summary>
        /// Given an array, index, and StringBuilder, adds the array item at that index to the StringBuilder. Adds "unoccupied" if the array item is null
        /// </summary>
        /// <param name="arr">string array full of guest names</param>
        /// <param name="i">index you want in the array</param>
        /// <param name="sBuild">a string builder that you are adding to</param>
        static void AddToViewList(string[] arr, int i, StringBuilder sBuild)
        {
            if (String.IsNullOrEmpty(arr[i]))
            {
                sBuild.Append($"\n{i + 1}: [unoccupied]");
            }
            else
            {
                sBuild.Append($"\n{i + 1}: {arr[i]}");
            }
        }
    }
}
