using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Exceptions;

namespace DWMH
{
    public static class ConsoleIO
    {
        /// <summary>
        /// Prompt the user to enter an int with a message; reapeat until recieve valid input
        /// </summary>
        /// <param name="prompt">A message to prompt the user</param>
        /// <returns>Int entered by the user</returns>
        public static int PromptInt(string prompt)
        {
            int result;
            bool valid;
            do
            {
                valid = int.TryParse(PromptString(prompt,false), out result);
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
        public static int PromptInt(string prompt, int min, int max)
        {
            int result;
            bool valid;
            do
            {
                valid = int.TryParse(PromptString(prompt,false), out result);
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

        /// <summary>
        /// Prompts the user to enter a string and returns their entry
        /// </summary>
        /// <param name="prompt">Message to prompt the user</param>
        /// <param name="emptyOk">Setting for whether an empty entry should be allowed</param>
        /// <returns>String entered by the user</returns>
        public static string PromptString(string prompt, bool emptyOk)
        { 
            string result;
            if (!emptyOk)
            {
                do
                {
                    Console.Write($"{prompt}: ");
                    result = Console.ReadLine();
                } while (String.IsNullOrEmpty(result));
            }
            else
            {
                Console.Write($"{prompt}: ");
                result = Console.ReadLine();
            }
           
            return result;
        }
        /// <summary>
        /// Displays a message by writing to the console and inserting a line after.
        /// </summary>
        /// <param name="message">Message to display to the user</param>
        public static void DisplayLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prompts the user for a valid Date
        /// </summary>
        /// <param name="message">Message used to prompt the user</param>
        /// <param name="emptyOk">Setting for whether an empty string is allowed</param>
        /// <returns>DateTime representing the user's input</returns>
        public static DateTime PromptDateTime(string message, bool emptyOk)
        {
            string input = PromptString(message,emptyOk);
            DateTime date;
            bool isValid;

            if (!emptyOk)
            {
                do
                {
                    isValid = DateTime.TryParse(input, out date);
                } while (!isValid);
            }
            else
            {
                date = default(DateTime);
            }

            return date;
        }



        /// <summary>
        /// Used to pause the console until the user wants to continue
        /// </summary>
        public static void AnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void DisplayReservationList(List<Reservation> reservations)
        {
            DisplayLine("========");
            DisplayLine($"{ reservations[0].Host.LastName}: { reservations[0].Host.City},{ reservations[0].Host.State}");
            DisplayLine("========");

            foreach (var res in reservations)
            {
                DisplayLine($"ID: {res.ID}, {res.StartDate:d} - {res.EndDate:d} Guest: {res.Guest.LastName}," +
                    $" {res.Guest.FirstName}, Email: {res.Guest.Email}\nTotal: {res.Total:C}");
            }
        }

        public static void DisplayReservationSummary(Reservation reservation)
        {
            DisplayLine("");
            DisplayLine($"Summary:\nStart Date: {reservation.StartDate:d}\nEnd Date: {reservation.EndDate:d}" +
                $"\nTotal: {reservation.Total:C}");
            DisplayLine("");
        }

        public static bool PromptYesNo()
        {
            string input = PromptString("Is this correct? [y/n]", false).ToLower();
            if(input == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void DisplayStatus(bool success, string message)
        {
            DisplayStatus(success, new List<string>() { message });
        }

        public static void DisplayStatus(bool success, List<string> messages)
        {
            DisplayLine("");
            DisplayLine(success ? "Success" : "Error");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string message in messages)
            {
                DisplayLine(message);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}