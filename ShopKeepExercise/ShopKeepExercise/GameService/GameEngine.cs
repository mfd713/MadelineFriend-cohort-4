using System;
using System.Collections.Generic;
using System.Text;
using ShopKeepExercise.Core;

namespace ShopKeepExercise
{
    public class GameEngine
    {
        private Shopkeeper protag;
        private Encounter encounter;


        public GameEngine(Shopkeeper protag, Encounter encounter)
        {
            this.protag = protag;
            this.encounter = encounter;
        }

        public void Travel()
        {
            protag.Distance += 100;
        }

        public Shopkeeper GetProtagInfo()
        {
            return protag;
        }

        public void SetEncounter(Encounter encounter)
        {
            this.encounter = encounter;
        }
        public Result RunEncounter()
        {
            //if there are no items in the cart, encounter should be nothing case
            if(protag.Cart.Inventory.Count == 0)
            {
                return new Result { IsSuccess = false, Message = encounter.NoItems};
            }

            encounter.InteractWithShopkeep(protag);
            return new Result { IsSuccess = true, Message = "" };
        }
    }
}