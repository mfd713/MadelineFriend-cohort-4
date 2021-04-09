using System;

namespace RogueLike
{
    public class Game
    {
        private char WALL_CHARACTER = '#';
        private char EMPTY_CHARACTER = ' ';
        private char HERO_CHARACTER = '@';
        private Board gameBoard;
        private Hero _hero;
        private Monster _monster;
        /// <summary>
        /// This method is used to setup a new board with hero
        /// </summary>
        public void SetUp()
        {
            gameBoard = new Board(15,15);
            _hero = new Hero();
            Console.WriteLine("What is your hero's name?");
            _hero.Name = Console.ReadLine();
            _hero.Level = 0;
            _hero.Power = 1;
            _hero.Health = 30;
            _hero.Location.X = 5;
            _hero.Location.Y = 5;

            _monster = new Monster();
            _monster.Health = 10;
            _monster.Power = 2;
            _monster.Location.X = 3;
            _monster.Location.Y = 3;
        }

        public void Run()
        {
            // Keep the game running as long as the hero has health
            // Display the board
            while (_hero.Health > 0)
            {
                DisplayBoard();
                // WASD
                ConsoleKeyInfo key = Console.ReadKey();
                int x = _hero.Location.X;
                int y = _hero.Location.Y;
                switch (key.Key)
                {
                    case ConsoleKey.W: // go north
                        y--;
                        break;
                    case ConsoleKey.A: // go west
                        x--;
                        break;
                    case ConsoleKey.S: // go south
                        y++;
                        break;
                    case ConsoleKey.D: // go east
                        x++;
                        break;
                }

                if (gameBoard.Height > y && y > -1)
                {
                    _hero.Location.Y = y;
                }

                if (gameBoard.Width > x && x > -1)
                {
                    _hero.Location.X = x;
                }
                // Check to see if monster is at location
                // if monster is there, attack the monster, take damage
                // if monster is killed, set the monster to null

            }
        }

        public void DisplayBoard()
        {
            Console.Clear();
            for (int row = -1; row <= gameBoard.Height; row++)
            {
                for (int col = -1; col <= gameBoard.Width; col++)
                {
                    bool isHeroHere = _hero.Location.X == col && _hero.Location.Y == row;
                    bool isWall = row == -1 || col == -1 || row == gameBoard.Height || col == gameBoard.Width;

                    // check for the location of the monster

                    if (isWall)
                    {
                        Console.Write(WALL_CHARACTER);

                    }else if (isHeroHere)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(HERO_CHARACTER);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(EMPTY_CHARACTER);
                    }
                }

                Console.WriteLine();
                // new line here?
            }
        }
    }
}