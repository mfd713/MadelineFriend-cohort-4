using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gomoku.Players;

namespace Gomoku.Game
{
    public class GameRunner
    {
        public GomokuEngine G { get; private set; }
        public IPlayer[] Players { get; private set; }
        public void SetUp()
        {
            Players = ConsoleIO.SetupMenu();
            G = new GomokuEngine(Players[0], Players[1]);
        }

        public void Run()
        {
            do //prompt for moves
            {
                Console.WriteLine("start of loop");
                Console.ReadKey();
                Stone test = new Stone(5, 5, true);
                G.Place(test);

                //the most recent move (Place(Stone)) didn't result in a success condition (Result.IsSuccess == false)
            } while (!G.IsOver); 
        }
    }
}
