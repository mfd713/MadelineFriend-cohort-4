using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Traveler : IEncounter
    {
        private Random _rand;
        public const int APPEAR_CHANCE = 10;
        public int HaggleChance { get; private set; }
        public void GenerateActionChance()
        {
            HaggleChance = _rand.Next(1, 6);
        }
        
        public bool Purchase(Dictionary<Item, int> Inventory)
        {
            return true;
        }
    }
}
