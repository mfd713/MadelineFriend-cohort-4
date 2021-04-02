using System;

namespace CapsuleHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            //run start menu to get guest array length
            int capacity = StartupMenu();
            string[] guestList = new string[capacity];
            Console.WriteLine($"Ok! There are {guestList.Length} capsules ready for booking.");
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
        /// <summary>
        /// Displays welcome message and prompts user for the number of capsules available
        /// </summary>
        /// <returns>int capacityNumber</returns>
        static int StartupMenu()
        {
            Console.WriteLine("Welcome to the Hotel California! Due to a large volume of customer complaints, guests are now allowed to both check out and leave.");
            return ReadInt("Enter the number of capsules available");
        }
    }
}
