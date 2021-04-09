using System;
using Gomoku.Game;
namespace Gomoku
{
    class Program
    {
        static void Main(string[] args)
        {
            GameRunner gr = new GameRunner();

            gr.SetUp();
            gr.Run();
        }
    }
}
