using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gomoku.Players;

namespace Gomoku.Game
{ 
    public static class ConsoleIO
    {
        /// <summary>
        /// Shows the intro menu to the user and prompts them to set player types (and names if applicable)
        /// </summary>
        /// <returns>Array of IPlayers with a length of 2, player 1 in [0] and player 2 in [1]</returns>
        public static IPlayer[] SetupMenu()
        {
            IPlayer[] players = new IPlayer[2];
            Console.WriteLine("Let's play Gomoku!\n**********");

            int p1Type = PromptInt("Player 1 is\n1. Human\n2. Random Player\nSelect[1-2]", 1, 2);
            switch (p1Type)
            {
                case 1:
                    string p1Name = PromptString("Player 1, what is your name?");
                    players[0] = new HumanPlayer(p1Name);
                    break;
                case 2:
                    players[0] = new RandomPlayer();
                    break;
            }

            int p2Type = PromptInt("Player 2 is\n1. Human\n2. Random Player\nSelect[1-2]", 1, 2);
            switch (p2Type)
            {
                case 1:
                    string p2Name = PromptString("Player 2, what is your name?");
                    players[1] = new HumanPlayer(p2Name);
                    break;
                case 2:
                    players[1] = new RandomPlayer();
                    break;
            }
            return players;
        }

        public static void DisplayBoard(GameRunner gr)
        {
            
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Console.Write(gr.Board[i,j]);
                }
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Get an int from the user
        /// </summary>
        /// <param name="prompt">a message to prompt the user</param>
        /// <returns>int that the user entered</returns>
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
        /// Gets a string from the user, validates that the string isn't null/empty
        /// </summary>
        /// <param name="prompt">a message to prompt the user</param>
        /// <returns>The user's input in string form</returns>
        public static string PromptString(string prompt)
        {
            string result;
            do
            {
                Console.Write($"{prompt}: ");
                result = Console.ReadLine();
            } while (string.IsNullOrEmpty(result));
            return result;
        }
    }
}
