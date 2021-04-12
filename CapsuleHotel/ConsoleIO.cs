using System;
using System.Collections.Generic;
using System.Text;

namespace CapsuleHotel
{
    public static class ConsoleIO
    {
        /// <summary>
        /// Displays welcome message and prompts user for the number of capsules available
        /// </summary>
        /// <returns>int capacityNumber</returns>
       public static int StartupMenu()
        {
            Console.WriteLine("Welcome to the Hotel Capsulefornia! Due to a large volume of customer complaints, guests are now allowed to both check out and leave.");
            return ReadInt("Enter the number of capsules available");
        }

        public static Guest GetGuestInfo()
        {
            string first, last;
            DateTime time;

            first = ReadString("Guest first name");
            last = ReadString("Guest last name");
            time = DateTime.Now;
            return new Guest(first, last, time);
        }

        /// <summary>
        /// Prompts the user to enter a checkout day
        /// </summary>
        /// <returns>DateTime value of their checkout day/time</returns>
        public static DateTime GetCheckoutDay()
        {
            string input = ReadString("What day are you checking out?");
            return DateTime.Parse(input);
        }

        /// <summary>
        /// Displays the total cost of a stay in the capsule hotel
        /// </summary>
        /// <param name="checkin">DateTime that a guest was checked in</param>
        /// <param name="checkout">DateTime that a guest is checking out</param>
        /// <param name="cost">Cost per night of a capsule</param>
        public static void PrintTotalCost(Guest guest, DateTime checkout, decimal cost)
        {
            TimeSpan length = checkout.Subtract(guest.CheckInTime);
            //Stay must be at least one day, partial days are rounded up to whole number
            int days = Math.Max(1, (int)Math.Ceiling(length.TotalDays));

            Console.WriteLine($"Ok! {guest.FirstName} {guest.LastName} was checked out." +
                $" The total cost for {days} day(s) is {days*cost}");
        }
        /// <summary>
        /// Prints guest list to the console
        /// </summary>
        /// <param name="arr">string array of guest list</param>
        public static void ViewGuestList(Capsule[] arr)
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
            if (arr.Length - 5 - 1 < centerIndex)
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
        /// Given an array, index, and StringBuilder, adds the array item at that index to the StringBuilder. Adds "unoccupied" if the array item is null
        /// </summary>
        /// <param name="arr">string array full of guest names</param>
        /// <param name="i">index you want in the array</param>
        /// <param name="sBuild">a string builder that you are adding to</param>
        static void AddToViewList(Capsule[] arr, int i, StringBuilder sBuild)
        {
            if (!arr[i].isOccupied)
            {
                sBuild.Append($"\n{i + 1}: [unoccupied]");
            }
            else
            {
                sBuild.Append($"\n{i + 1}: {arr[i].Occupant.FirstName} {arr[i].Occupant.LastName}");
            }
        }

        static public void PrintErrorMessage()
        {
            Console.WriteLine("Something went horribly wrong!");
            AnyKeyToContinue();
        }  
            
        
        //get an int from the user
        //Input: string; a message used to prompt the user
        //output: int
        public static int ReadInt(string prompt)
        {
            int result;
            bool valid;
            do
            {
                valid = int.TryParse(ReadString(prompt), out result);
            } while (!valid);
            return result;
        }
        /// <summary>
        /// get an int from the user between a specified min and max value, inclusive
        /// </summary>
        /// <param name="prompt">string to prompt user</param>
        /// <param name="min">minimum int allowed</param>
        /// <param name="max">maximum int allowed</param>
        /// <returns>int value of user input</returns>
       public  static int ReadInt(string prompt, int min, int max)
        {
            int result;
            bool valid;
            do
            {
                valid = int.TryParse(ReadString(prompt), out result);
                if (valid)
                {
                    valid = result >= min;
                    if (valid)
                    {
                        valid = result <= max;
                    }
                }
            } while (!valid);
            return result;
        }

        //get a string from the user
        //Input: string; a message used to prompt the user
        //Output: string
       public static string ReadString(string prompt)
        {
            string result;
            do
            {
                Console.Write($"{prompt}: ");
                result = Console.ReadLine();
            } while (String.IsNullOrEmpty(result));
            return result;
        }

        //pause the console and allow user to press any key to continue
        public static void AnyKeyToContinue()
        {

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
