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
    }
}
