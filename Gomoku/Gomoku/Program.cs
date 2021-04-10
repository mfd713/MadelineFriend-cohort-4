using System;
using Gomoku.Game;
namespace Gomoku
{
    class Program
    {
        static void Main(string[] args)
        {
            GameRunner gr = new GameRunner();
            string playAgain;

            do
            {
                gr.SetUp();
                gr.Run();
                playAgain = ConsoleIO.PromptString("Do you want to play again? [y/n]");
            } while (playAgain == "y");

        }
    }
}
