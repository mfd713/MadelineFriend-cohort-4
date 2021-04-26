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
        private int TheifChance;
        private int TheifRobAmount;
        private int ProtectionPrice;
        public void Setup()
        {
            //TODO: change encounters
            Encounter encounter = new Traveler();
            Protag = InitializeProtagonist();
            Engine = new GameEngine(Protag, encounter);
            ConsoleIO.Display($"And now, {Protag.Name}, your journey begins!");
            ConsoleIO.AnyKeyToContinue();
            TheifChance = 10; 
            TheifRobAmount = 100;
            ProtectionPrice = 60;
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
                    Engine.SetEncounter(new Traveler());
                    mostRecentEncounter = Engine.RunEncounter();
                }

                if (mostRecentEncounter.Message != null && mostRecentEncounter.Message.Length > 0)
                {
                    ConsoleIO.Display(mostRecentEncounter.Message);
                    ConsoleIO.AnyKeyToContinue();
                }

                //Every 500 miles, chance for a thief encounter
                if (Protag.Distance % 500 == 0)
                {
                    if (random.Next(1, 100) < TheifChance)
                    {
                        Thief theif = new Thief();
                        theif.RobAmount = TheifRobAmount;
                        Engine.SetEncounter(theif);

                        mostRecentEncounter = Engine.RunEncounter();
                        //increase theif rob amount after encounter
                        TheifRobAmount += 10;
                    }
                }

                if (mostRecentEncounter.Message != null && mostRecentEncounter.Message.Length > 0)
                {
                    ConsoleIO.Display(mostRecentEncounter.Message);
                    ConsoleIO.AnyKeyToContinue();
                }

                //Every 600 miles, increase thief chance but also allow protection purchace
                if(Protag.Distance % 600 == 0)
                {
                    TheifChance += 2;
                    string getProtection = ConsoleIO.PromptString($"Would you like to purchase protection for {ProtectionPrice} gold? [y/n]");
                    if(getProtection.ToLower() == "y")
                    {
                        //spent money
                        Protag.Cart.Gold -= ProtectionPrice;
                        //increase level
                        Protag.Cart.ProtectionLvl += 1;

                        ConsoleIO.Display($"Your protection level is now {Protag.Cart.ProtectionLvl}");
                    }
                    else
                    {
                        ConsoleIO.Display("You decide against protection");
                    }
                }

                

                //show stats at turn end
                ConsoleIO.DisplayStats(Protag);
                ConsoleIO.AnyKeyToContinue();

                //game is over if Gold hits 0
                gameIsOver = Protag.Cart.Gold <= 0;
            } while (!gameIsOver);

            ConsoleIO.Display("Game over!");
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
                    inventory.Add(new Item("Sword NFT", 100), 1);
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