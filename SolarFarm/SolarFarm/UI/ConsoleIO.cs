using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.UI
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
                valid = int.TryParse(PromptString(prompt), out result);
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
                valid = int.TryParse(PromptString(prompt), out result);
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
        /// Get a string from the user. Will repeat prompt until it is not null/empty
        /// </summary>
        /// <param name="prompt">String to prompt the user</param>
        /// <returns>Validated string entered by the reader</returns>
        public static string PromptString(string prompt)
        {
            string result;
            do
            {
                Console.Write($"{prompt}: ");
                result = Console.ReadLine();
            } while (String.IsNullOrEmpty(result));
            return result;
        }
        /// <summary>
        /// Displays a message by writing to the console and inserting a line after.
        /// </summary>
        /// <param name="message">Message to display to the user</param>
        public static void Display(string message)
        {
            Console.WriteLine(message);
        }


    }
}
