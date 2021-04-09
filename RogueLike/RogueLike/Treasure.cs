using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Treasure
    {
        public Location Location { get; set; }
        private Random _rng = new Random();

        public void GetLoot(Hero hero) 
        {
            int power = _rng.Next(1, 3);
            int level = 1;
            int dosh = _rng.Next(100, 10000);
            Console.WriteLine($"\n{hero.Name} got the loot, it was 1 level and {power} power!");
            Console.WriteLine($"There was also {dosh:C}!");
            Console.WriteLine("Leveling up has restored your health!");
            hero.Health = hero.MaxHealth;
            hero.Level += level;
            hero.Power += power;
            hero.Dosh += dosh;
            Location.X = -1;
            Location.Y = -1;

        }
    }
}
