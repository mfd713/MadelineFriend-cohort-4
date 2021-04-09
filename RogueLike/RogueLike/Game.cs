using System;

namespace RogueLike
{
    public class Game
    {
        private const char WALL_CHARACTER = '#';
        private const char EMPTY_CHARACTER = ' ';
        private char HERO_CHARACTER = '@';
        private const char MONSTER_CHARACTER = '!';
        private const char TREASURE_CHARACTER = '$';
        private Board gameBoard;
        private Hero _hero;
        private Monster _monster;
        private Treasure _treasure;
        private Random _rng = new Random();
        /// <summary>
        /// This method is used to setup a new board with hero
        /// </summary>
        public void SetUp()
        {
            gameBoard = new Board(15,30);
            _hero = new Hero();
            Console.WriteLine("What is your hero's name?");
            _hero.Name = Console.ReadLine();
            Console.WriteLine("What does your hero look like?");
            HERO_CHARACTER = Console.ReadLine()[0];
            _hero.Level = 0;
            _hero.Power = 1;
            _hero.Health = 30;
            _hero.MaxHealth = 30;
            _hero.Location.X = _rng.Next(0, gameBoard.Height);
            _hero.Location.Y = _rng.Next(0, gameBoard.Height);

            _monster = new Monster();
            _monster.Health = 10;
            _monster.Power = 2;
            _monster.Location.X = _rng.Next(0, gameBoard.Height);
            _monster.Location.Y = _rng.Next(0, gameBoard.Height);

            _treasure = new Treasure();
            _treasure.Location = new Location();
            _treasure.Location.X = -1;
            _treasure.Location.Y = -1;
        }

        public void Run()
        {
            // Keep the game running as long as the hero has health
            // Display the board
            while (true)
            {
                Console.Clear();
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

                _monster.MonsterFollow(_hero.Location);
                
                if (!(_monster.Location.X == x && _monster.Location.Y == y) &&
                    !(_treasure.Location.X == x && _treasure.Location.Y == y))
                {
                    if (gameBoard.Height > y && y > -1)
                    {
                        _hero.Location.Y = y;
                    }
                    else 
                    {
                        Console.Beep(391, 100);
                    }
                    if (gameBoard.Width > x && x > -1)
                    {
                        _hero.Location.X = x;
                    }
                    else 
                    {
                        Console.Beep(391, 100);
                    }
                }
                else if (_monster.Location.X == x && _monster.Location.Y == y) // Check to see if monster is at location
                {
                    Console.Clear();
                    Console.WriteLine("Your enter Glorious combat!");
                    Battle(_monster, _hero); // if monster is there, attack the monster, take damage
                    AnyKeyToCont();
                }
                else if (_treasure.Location.X == x && _treasure.Location.Y == y) 
                {
                    Console.Clear();
                    _treasure.GetLoot(_hero);
                    PlayTreasureSound();
                    _monster.SpawnMonster(gameBoard.Width, gameBoard.Height, _hero);
                    Console.ReadKey();
                }
            }
        }
        public void Battle(Monster _monster, Hero _hero)
        {
            //attack until Monster health == 0 or Hero health == 0
            while(_monster.Health > 0 && _hero.Health > 0)
            {
                
                _monster.Health -= _hero.Power;
                Console.WriteLine($"You strike at the monster for {_hero.Power} damage. It  has {_monster.Health} health!");
                _hero.Health -= _monster.Power * _rng.Next(1,2);
                if (_monster.Health <= 0)
                {
                    Console.WriteLine($"The monster strikes at you with its dying breath! You now have {_hero.Health}.");
                }
                else
                {
                    Console.WriteLine($"The monster strikes at you for {_monster.Power} damage. You now have {_hero.Health}.");
                }

            }

            if(_monster.Health <= 0)
            {
                DefeatMonster(_monster);
            }
            if (_hero.Health <= 0)
            {
                HeroDown(_hero);
            }
        }

        //eliminate monster after defeating
        public void DefeatMonster(Monster _monster)
        {
            // spawn treasure
            _treasure.Location.X = _rng.Next(0, gameBoard.Width);
            _treasure.Location.Y = _rng.Next(0, gameBoard.Height);

            //spawn monster off board
            bool isTopLeft = _rng.Next(0, 2) == 0;
            if (isTopLeft)
            {
                _monster.Location.X = _rng.Next(-3, -1);
                _monster.Location.Y = _rng.Next(-3, -1);
            }
            else 
            {
                _monster.Location.X = _rng.Next(gameBoard.Width, gameBoard.Width + 3);
                _monster.Location.Y = _rng.Next(gameBoard.Height, gameBoard.Height + 3);
            }
            _monster.Power = _hero.Level + 1;
            _monster.Health = 10 + (_rng.Next(1, 5) * _hero.Level);
        }


        public void HeroDown(Hero _hero)
        {
            PlayDefeatSound();
            Console.WriteLine($"{_hero.Name} has been fatally wounded.");
            GameOptions(_hero);
            
        }

        public void GameOptions(Hero _hero)
        {
            Console.WriteLine($"Enter \'C\' to revive {_hero.Name}, \'N\' to begin a new game, or \'Q\' to quit,");

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.C:
                    //revive hero
                    _hero.Revive();
                    break;
                case ConsoleKey.N:
                    //new game
                    this.SetUp();
                    this.Run();
                    break;
                case ConsoleKey.Q:
                    //quit
                    Quit(_hero);
                    break;
            }
        }

        public void DisplayBoard()
        {
            // draw board
            for (int row = -1; row <= gameBoard.Height; row++)
            {
                for (int col = -1; col <= gameBoard.Width; col++)
                {
                    bool isHeroHere = _hero.Location.X == col && _hero.Location.Y == row;
                    bool isWall = row == -1 || col == -1 || row == gameBoard.Height || col == gameBoard.Width;
                    bool isMonster = _monster.Location.X == col && _monster.Location.Y == row;
                    bool isTreasure = _treasure.Location.X == col && _treasure.Location.Y == row;

                    // check for the location of the monster

                    if (isMonster)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(MONSTER_CHARACTER);
                        Console.ResetColor();
                    }
                    else if (isWall)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(WALL_CHARACTER);
                        Console.ResetColor();
                    }
                    else if (isHeroHere)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(HERO_CHARACTER);
                        Console.ResetColor();
                    }
                    else if (isTreasure)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(TREASURE_CHARACTER);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(EMPTY_CHARACTER);
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            // draw stats
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"#  Name    : {_hero.Name}");
            Console.WriteLine($"#  Level    : {_hero.Level}");
            Console.WriteLine($"#  Dosh     : {_hero.Dosh:C}");
            Console.WriteLine($"#  Health   : {_hero.Health} / {_hero.MaxHealth}");
            Console.WriteLine($"#  Location : x:{_hero.Location.X}, y:{_hero.Location.Y}");
            Console.ResetColor();
            for (int i = 0; i < gameBoard.Width + 2; i++) 
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(WALL_CHARACTER);
                Console.ResetColor();
            }
        }

        public void Quit(Hero _hero)
        {
            Console.Write("Are you sure you want to quit? [y/n]: ");
            string quitConfirm = Console.ReadLine();

            switch (quitConfirm)
            {
                case "Y":
                case "y":
                    //Console.WriteLine(""); (game summary?)
                    System.Environment.Exit(0);
                    break;
                case "N":
                case "n":
                    //other options
                    GameOptions(_hero);
                    break;
            }
        }

        public static void PlayTreasureSound() 
        {
            int tempo = 90;
            Console.Beep(391, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(369, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(311, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(220, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(207, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(329, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(415, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(523, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
        }

        public static void PlayDefeatSound()
        {
            int tempo = 130;
            Console.Beep(523, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(415, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
            Console.Beep(329, tempo);
            System.Threading.Thread.Sleep(tempo + 10);
        }

        public static void AnyKeyToCont()
        {
            Console.WriteLine("Your combat has ended! Press Any key to continue");
            Console.ReadKey();
        }
    }
}