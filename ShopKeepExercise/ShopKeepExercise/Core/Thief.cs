using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShopKeepExercise
{
    public class Thief : Encounter
    {
        private Random _rand = new Random();
        public int RobAmount { get; set; }
        public override string NoItems => $"Rats! You encounter bandits, and they steal {RobAmount} gold. Fortunately? you" +
                            $" didn't have any items to steal.";
        public override void InteractWithShopkeep(Shopkeeper protag)
        {
            if (protag.Cart.ProtectionLvl == 0)
            {
                protag.Cart.Gold -= RobAmount;
                Dictionary<Item, int> copy = protag.Cart.Inventory;
                Item itemToTake = copy.Keys.ElementAt(_rand.Next(0, copy.Count - 1));

                if (itemToTake != null)
                {
                    ConsoleIO.Display($"Rats! You encounter bandits, and they steal {RobAmount} gold and a {itemToTake.Name}!");

                    protag.Cart.RemoveInventoryItem(protag.Cart.Inventory, itemToTake);
                }
                else
                {
                    Console.WriteLine();

                }
            }
            else
            {
                protag.Cart.ProtectionLvl--;
                Console.WriteLine($"Bandits attacked, but they were staved off by your protection! Your cart's protection" +
                    $" level is now {protag.Cart.ProtectionLvl}");
            }
        }
    } 
}