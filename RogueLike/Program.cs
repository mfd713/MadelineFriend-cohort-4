namespace RogueLike
{
    // Creating a Rogue like game to gain treasure and defeat monsters!
    // We'll a hero that is given a name by the player, and will start at level 0
    // level is the number of monsters they kill
    // A hero can have a power of 1
    // the hero's power helps defeat monsters.
    // the hero can attack a creature, and lower the creatures health by the amount of power they have.
    // each treasure they pick up, will randomly increase their power by 1-3
    // each hit to a creature will also reduce the hero's health by 2
    // when defeating a monster, a new treasure appears
    // when you die, ask the player if they would like to start again, with the current power they have acquired

    // Monsters are just random entities that will have health


    // Game
        // Run()
        // Setup()
        // PrintBoard()
        // MoveHero()

    // Board
        // Width
        // Height

    // Hero
        // Location
        // Name
        // Level
        // Health
        // Power
    // Monster
        // Location
        // Health
        // Power
    // Treasure
        // PowerBonus

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.SetUp();
            game.Run();
        }

    }
}
