using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Theif : IEncounter
    {
        private Random _rand;
        public int RobAmount { get; set; }
        public int AppearChance { get; private set; }
        public void GenerateActionChance()
        {
            AppearChance = _rand.Next(1, 11);
        }
    }
}
