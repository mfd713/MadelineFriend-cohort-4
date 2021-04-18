using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;

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

        /// <summary>
        /// Prompts the user for a valid Date
        /// </summary>
        /// <param name="message">Message used to prompt the user</param>
        /// <returns>DateTime representing the user's input</returns>
        public static DateTime PromptDateTime(string message)
        {
            string input = PromptString(message);
            bool isValid;
            DateTime date;
            do
            {
                isValid = DateTime.TryParse(input, out date);
            } while (!isValid);

            return date;
        }

        /// <summary>
        /// Returns a Material based on the user's choice
        /// </summary>
        /// <param name="message">Message to prompt the user</param>
        /// <returns>A MaterialType enum</returns>
        public static MaterialType PromptMaterialType(string message)
        {
            int userChoice = PromptInt(message, 1, 5) - 1;

            switch (userChoice)
            {
                case 0:
                    return MaterialType.MulticrystallineSilicon;
                case 1:
                    return MaterialType.MonocrystallineSilicon;
                case 2:
                    return MaterialType.AmorphousSilicon;
                case 3:
                    return MaterialType.CadmiumTelluride;
                default:
                    return MaterialType.CopperIndiumGalliumSelenide;
            }
        }
        
        /// <summary>
        /// Prompts user for whether the panel is tracking the sun, and translates to a boolean
        /// </summary>
        /// <param name="message">Message to prompt the user</param>
        /// <returns>Boolean of user's input</returns>
        public static bool PromptIsTracking(string message)
        {
            string input;
            do
            {
                input = PromptString(message);
            } while (input != "y" || input != "n");
            
            if(input == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Prints solar panel info to the console
        /// </summary>
        /// <param name="panel">Solar panel to be printed</param>
        public static void PrintPanel(SolarPanel panel)
        {
            string trackingTranslation;
            if(panel.IsTracking == true)
            {
                trackingTranslation = "yes";
            }
            else
            {
                trackingTranslation = "no";
            }

            string materialTranslation;
            switch ((int)panel.Material)
            {
                case 0:
                    materialTranslation = "Multicrystalline Silicon";
                    break;
                case 1:
                    materialTranslation = "Monocrystalline Silicon";
                    break;
                case 2:
                    materialTranslation = "Amorphous Silicon";
                    break;
                case 3:
                    materialTranslation = "Cadmium Telluride";
                    break;
                default:
                    materialTranslation = "Copper Indium Gallium Selenide";
                    break;
            }

            Console.WriteLine($"\nSection: {panel.Section}\nRow: {panel.Row}\nColumn: {panel.Column}\n" +
                $"Date Installed: {panel.DateInstalled.ToString("d")}\nMaterial Type: {materialTranslation}\nTracking: {trackingTranslation}");
        }

        /// <summary>
        /// Used to pause the console until the user wants to continue
        /// </summary>
        public static void AnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
