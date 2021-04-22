using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise.GameEngine
{
    public class GameEngine
    {
        private Shopkeeper protag;


        public GameEngine(Shopkeeper protag)
        {
            this.protag = protag;
        }

        public void Travel()
        {
            protag.Distance += 100;
        }

        public Shopkeeper GetProtagInfo()
        {
            return protag;
        }
    }
}
