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
        public string[,] Board { get; set; }
        public void SetUp()
        {
            Players = ConsoleIO.SetupMenu();
            G = new GomokuEngine(Players[0], Players[1]);
            Board = new string[16, 16];

            //initalize blank board
            Board[0, 0] = "  ";
            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {

                    //draw key numbers for tippy top row and far left column, space for -1, -1 spot
                    if (row == 0 && col > 0)
                    {
                        Board[row, col] = $" { col:00}";
                    }
                    else if (col == 0 && row > 0)
                    {
                        Board[row, col] = $"{row:00} ";
                    }

                    else if (col > 0 && row > 0)
                    {
                        Board[row, col] = " _ ";
                    }
                }
            }
        }

        public void Run()
        {
            do //prompt for moves
            {
                //display the board
                ConsoleIO.DisplayBoard(this);

                Stone mostRecent = G.Current.GenerateMove(G.Stones);
                //If player turn, prompt for moves
                if(mostRecent == null)
                {
                    Result ofHumanMove;

                    do
                    {
                        mostRecent = ConsoleIO.GetHumanMove(this);
                        ofHumanMove = G.Place(mostRecent);//Validate move is legal 
                    } while (!ofHumanMove.IsSuccess); //the most recent move (Place(Stone)) didn't result in a success condition
                    //add stone to the board after human moves
                    switch (mostRecent.IsBlack)
                    {
                        case true:
                            Board[mostRecent.Row, mostRecent.Column] = " X ";
                            break;
                        case false:
                            Board[mostRecent.Row, mostRecent.Column] = " O ";
                            break;
                    }
                }
                else //If random turn, generate the move
                {
                    Result ofRandomMove;
                    
                    do
                    {
                        ofRandomMove = G.Place(mostRecent);
                    } while (!ofRandomMove.IsSuccess);

                    //add stone to the board after random move 
                    switch (mostRecent.IsBlack)
                    {
                        case true:
                            Board[mostRecent.Row+1, mostRecent.Column+1] = " X "; //add 1 to prevent placing in the 0th rows
                            break;
                        case false:
                            Board[mostRecent.Row+1, mostRecent.Column+1] = " O ";
                            break;
                    }
                }
                

                //keep running GenerateMove while (!G.Place(Stone).IsSuccess) 

                

                //check if there is a win
                //break if yes
                ConsoleIO.DisplayBoard(this);

            } while (!G.IsOver); //while the game is not over!G.IsOver
            Console.WriteLine($"{G.Winner.Name} has won!");

        }
    }
}
