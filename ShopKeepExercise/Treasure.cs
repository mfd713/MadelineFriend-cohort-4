using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public class Treasure : IEncounter
    {
        private Random _rand;
        public int AppearChance { get; set; }
        public void GenerateActionChance()
        {
            throw new NotImplementedException();
        }
    }
}
