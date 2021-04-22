using System;
using ShopKeepExercise.UI;

namespace ShopKeepExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            GameRunner runner = new GameRunner();

            runner.Setup();
            runner.Run();
        }
    }
}
