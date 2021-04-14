using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShopKeepExercise
{
    public class Theif : IEncounter
    {
        private Random _rand;
        public int RobAmount { get; set; }
        public int AppearChance { get; private set; }
        public void InteractWithShopkeep(Shopkeeper protag)
        {
            if(_rand.Next(0, AppearChance) == _rand.Next(1, 100)) //has a 1 in AppearChance probability of happening
            {
                if(protag.Cart.ProtectionLvl == 0)
                {
                    protag.Gold -= RobAmount;
                    Dictionary<Item, int> copy = protag.Cart.Inventory;
                    Item itemToTake = copy.Keys.ElementAt(_rand.Next(0, copy.Count - 1));

                    if(itemToTake != null)
                    {
                        Console.WriteLine($"Rats! You encounter bandits, and they steal {RobAmount} gold and a {itemToTake.Name}!");

                        protag.Cart.RemoveInventoryItem(protag.Cart.Inventory, itemToTake);
                    }
                    else
                    {
                        Console.WriteLine($"Rats! You encounter bandits, and they steal {RobAmount} gold. Fortunately? you" +
                            $"didn't have any items to steal.");

                    }


                }
                else
                {
                    protag.Cart.ProtectionLvl--;
                    Console.WriteLine($"Bandits attacked, but they were staved off by your protection! Your cart's protection" +
                        $"level is now {protag.Cart.ProtectionLvl}");
                }
            }
            else
            {
                Console.WriteLine("There was talk of bandits on this road, but you pass the next 100 miles safely.");
            }
        }
    }
}
