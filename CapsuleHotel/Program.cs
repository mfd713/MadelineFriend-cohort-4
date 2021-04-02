﻿using System;
using System.Text;

namespace CapsuleHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            //run start menu to get guest array length
            int capacity = StartupMenu();
            string[] guestList = new string[capacity];
            string[] fullList = { "Ada", "Bill", "Carson" };

            Console.WriteLine($"Ok! There are {guestList.Length} capsules ready for booking.");
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

                int menuChoice = ReadInt("Please enter the number of the menu item you wish to do");
                //switch statement starts the method of the menu item the user chooses
                switch (menuChoice)
                {
                    case -1:
                        break;
                    case 1:
                        break;
                    case 2:
                        CheckIn(guestList);
                        break;
                    case 3:
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
        //get an int from the user
        //Input: string; a message used to prompt the user
        //output: int
        static int ReadInt(string prompt)
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
        static int ReadInt(string prompt, int min, int max)
        {
            int result;
            bool valid;
            do
            {
                valid = int.TryParse(ReadString(prompt), out result);
                valid = result >= min;
                valid = result <= max;
            } while (!valid);
            return result;
        }

        //get a string from the user
        //Input: string; a message used to prompt the user
        //Output: string
        static string ReadString(string prompt)
        {
            string result = "";
            do
            {
                Console.Write($"{prompt}: ");
                result = Console.ReadLine();
            } while (String.IsNullOrEmpty(result));
            return result;
        }

        //pause the console and allow user to press any key to continue
        static void AnyKeyToContinue()
        {
            ConsoleKeyInfo consoleKey;
            do
            {
                Console.WriteLine("Press any key to continue...");
                consoleKey = Console.ReadKey();
            } while (false);
        }

        /// <summary>
        /// Displays welcome message and prompts user for the number of capsules available
        /// </summary>
        /// <returns>int capacityNumber</returns>
        static int StartupMenu()
        {
            Console.WriteLine("Welcome to the Hotel California! Due to a large volume of customer complaints, guests are now allowed to both check out and leave.");
            return ReadInt("Enter the number of capsules available");
        }

        /// <summary>
        /// Confirms that the user really wants to exit, and exits if they do. Otherwise returns to main menu
        /// </summary>
        /// <returns>string yesNo</returns>
        static string CheckExit()
        {
            string yesNo = ReadString("Are you sure you want to exit? All data wil be lost! (y/n)");
            return yesNo;
        }
        /// <summary>
        /// Promps the user for a guest name & capsule number and adds the guest to the list if both entries are valid
        /// </summary>
        /// <param name="arr">string[] guestList</param>
        static void CheckIn(string[] arr)
        {
            string guestName = "";
            int capsuleNumber = 0;
            //confirm at least one open spot
            if (CountNullOrEmpties(arr)==0)
            {
                Console.WriteLine("The guest list is full. Please remove a guest before adding a new one.");
                AnyKeyToContinue();
                return;
            }
            //promt user for guest name
            guestName = ReadString("Enter the guest name");
            //prompt user for spot number
            capsuleNumber = ReadInt("Enter the capsule number", 1, arr.Length);
            //re-prompt if spot is occupied
            while(IsCapsuleOccupied(arr, capsuleNumber-1))
            {
                capsuleNumber = ReadInt("Oops! That capsule is occupied. Please enter another");
            }

            //set if spot is open
            arr[capsuleNumber-1] = guestName;
            Console.WriteLine($"Success! {guestName} was added to Capusle # {capsuleNumber}");
            AnyKeyToContinue();
        }
        /// <summary>
        /// Checks if the strin in a specified array spot is filled. Returns true if filled, false if empty
        /// </summary>
        /// <param name="arr">string[] guestList</param>
        /// <param name="spot">string[] spotNumber</param>
        /// <returns>bool filled/not filled</returns>
        static bool IsCapsuleOccupied(string[] arr, int spot)
        {
            return !(String.IsNullOrEmpty(arr[spot]));
        }
        /// <summary>
        /// Counts the number of null/empty strings in an array of strings
        /// </summary>
        /// <param name="arr">array of strings</param>
        /// <returns>int count of empties</returns>
        static int CountNullOrEmpties(string[] arr)
        {
            int count = 0;
            foreach (string item in arr)
            {
                if (String.IsNullOrEmpty(item))
                {
                    count++;
                }
            }
            return count;
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
            centerIndex = ReadInt("Please enter the capsule you wish to view", 1, arr.Length) - 1;

            //print header showing Capsule # Guest Name
            Console.WriteLine($"Capsule # : Guest Name");

            //print 11 pairs (or as many on side as possible given the array scope)
            if((centerIndex -5 >= 0) && (arr.Length - 1 - 5 >= centerIndex))
            {
                for(int i = centerIndex-5; i < 11; i++)
                {
                    content.Append($"{i}: {arr[i]}");
                }
            }
            //pause until move on
        }
    }
}