using System;

namespace RogueLike
{
    public class Hero
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Level { get; set; }
        public int Dosh { get; set; }
        public int Power { get; set; }
        public Location Location { get; set; }

        public Hero()
        {
            Location = new Location();
        }

        public void Revive()
        {
            //set health back up
            Health = MaxHealth;
            Console.WriteLine($"{Name} fell, but was revived for the price of {Dosh:C}.");

            Dosh = 0;
            //Game.AnyKeyToCont();
        }
    }
}