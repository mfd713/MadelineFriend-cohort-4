using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShopKeepExercise
{
    public class Traveler : IEncounter
    {
        private Random _rand;
        public const int APPEAR_CHANCE = 10;
        //private const int HAGGLE_CHANCE = 2;
        public void InteractWithShopkeep(Shopkeeper protag)
        {
            if(_rand.Next(0, APPEAR_CHANCE) == _rand.Next(0, 100)) //has a 1 in AppearChance probability of happening
            {
                Dictionary<Item, int> copy = protag.Cart.Inventory;
                Item itemToTake = copy.Keys.ElementAt(_rand.Next(0, copy.Count - 1));

                if (itemToTake != null)
                {
                    Console.WriteLine($"A traveler appears and is interested in your wears! They offer to buy {itemToTake.Name}" +
                        $"for its full price of {itemToTake.Value} gold");
                    string userChoice = ConsoleIO.PromptString("Do you want to sell this item? [y/n]");

                    if(userChoice == "y")
                    {
                        //protag.Gold += itemToTake.Value;
                        Console.WriteLine("Sold!");
                        protag.Cart.RemoveInventoryItem(protag.Cart.Inventory, itemToTake);
                    }
                    else
                    {
                        Console.WriteLine($"You choose not to part with your beloved {itemToTake.Name} and continue on your way.");
                    }

                }
                else
                {
                    Console.WriteLine($"You encounter a traveler, but because you have nothing to sell, you merely exchange" +
                        $"plesantries and continue on your way.");

                }
            }
        }
        
        public bool Purchase(Dictionary<Item, int> Inventory)
        {
            return true;
        }
    }
}
