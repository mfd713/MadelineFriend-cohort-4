using System;
using System.Collections.Generic;
using System.Text;
using ShopKeepExercise.Core;
namespace ShopKeepExercise.UI
{
    public class GameRunner
    {
        public Shopkeeper Protag { get; set; }
        public GameEngine Engine { get; set; }
        private const int _TRAVELER_CHANCE = 10;
        public void Setup()
        {
            //TODO: change encounters
            Encounter encounter = new Traveler();
            Protag = InitializeProtagonist();
            Engine = new GameEngine(Protag, encounter);
            ConsoleIO.Display($"And now, {Protag.Name}, your journey begins!");
            ConsoleIO.AnyKeyToContinue();
        }
        public void Run()
        {
            bool gameIsOver = false;
            Random random = new Random();
            Result mostRecentEncounter = new Result();

            do
            {
                Console.Clear();
                //perform travel
                Engine.Travel();

                //if chance is right, do a traveler encounter
                if (random.Next(1, 100) < _TRAVELER_CHANCE)
                {
                    mostRecentEncounter = Engine.RunEncounter();
                }

                if (mostRecentEncounter.Message != null && mostRecentEncounter.Message.Length > 0)
                {
                    ConsoleIO.Display(mostRecentEncounter.Message);
                    ConsoleIO.AnyKeyToContinue();
                }

                //show stats at turn end
                ConsoleIO.DisplayStats(Protag);
                ConsoleIO.AnyKeyToContinue();

                //game is over if Gold hits 0
                gameIsOver = Protag.Cart.Gold <= 0;
            } while (!gameIsOver);
        }

        private Shopkeeper InitializeProtagonist()
        {
            Cart cart = new Cart();
            cart.Gold = 100;
            cart.ProtectionLvl = 0;

            Dictionary<Item, int> inventory = new Dictionary<Item, int>();
            //set up inventory based on choice
            int inventoryChoice = ConsoleIO.PromptInt("What type of shopkeeper would you like to be?\n" +
                "1. Weapons Dealer\n2. Cabbage Merchant\n", 1, 2);
            switch (inventoryChoice)
            {
                case 1:
                    inventory.Add(new Item("Sword", 100), 1);
                    cart.Inventory = inventory;
                    ConsoleIO.Display($"You are a mighty weapons dealer, your inventory contains 1 sword!");
                    break;
                default:
                    inventory.Add(new Item("Cabbage", 5), 10);
                    cart.Inventory = inventory;
                    ConsoleIO.Display($"You are a mighty cabbage merchant, your inventory contains 10 Cabbages!");
                    break;
            }

            //get&set name
            string name = ConsoleIO.PromptString("But what is your name, brave merchant?");
            return new Shopkeeper(name, cart);
        }
    }
}