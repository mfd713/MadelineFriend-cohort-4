using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
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

        //get a string from the user
        //Input: string; a message used to prompt the user
        //Output: string
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

        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayStats(Shopkeeper keeper)
        {
            Console.WriteLine("****Stats****");
            Console.WriteLine($"Distance Traveled: {keeper.Distance}");
            Console.WriteLine($"Inventory:");
            ConsoleIO.PrintInventory(keeper);
            Console.WriteLine($"Total Gold: {keeper.Cart.Gold}");
            Console.WriteLine("********");

        }

        public static void PrintInventory(Shopkeeper keeper)
        {
            Console.WriteLine("========");
            foreach (var item in keeper.Cart.Inventory)
            {
                Console.WriteLine($"Item: {item.Key.Name}\nValue: {item.Key.Value}\nQuantity: {item.Value}");
                Console.WriteLine("========");

            }
        }
        public static void AnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

}