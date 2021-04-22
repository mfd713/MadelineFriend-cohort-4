using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShopKeepExercise
{
    public class Traveler : Encounter
    {
        private Random _rand = new Random();
        public override string NoItems => $"You encounter a traveler, but because you have nothing to sell, you merely exchange" +
                        $" plesantries and continue on your way.";
        //private const int HAGGLE_CHANCE = 2;
        public override void InteractWithShopkeep(Shopkeeper protag)
        {
                Dictionary<Item, int> copy = protag.Cart.Inventory;
                Item itemToTake = copy.Keys.ElementAt(_rand.Next(0, copy.Count - 1));

                ConsoleIO.Display($"A traveler appears and is interested in your wears! They offer to buy your {itemToTake.Name}" +
                    $" for its full price of {itemToTake.Value} gold");
                string userChoice = ConsoleIO.PromptString("Do you want to sell this item? [y/n]");

                if (userChoice.ToLower() == "y")
                {
                    protag.Cart.Gold += itemToTake.Value;
                    ConsoleIO.Display("Sold!");
                    protag.Cart.RemoveInventoryItem(protag.Cart.Inventory, itemToTake);
                }else
                {
                    Console.WriteLine($"You choose not to part with your beloved {itemToTake.Name} and continue on your way.");
                }
        }

        public bool Purchase(Dictionary<Item, int> Inventory)
        {
            return true;
        }
    }
}