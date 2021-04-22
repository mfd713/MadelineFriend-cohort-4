using NUnit.Framework;
using ShopKeepExercise;
using System.Collections.Generic;
using ShopKeepExercise.Core;

namespace Tests
{
    public class EngineTests
    {

        [Test]
        public void TravelShouldIncreaseShopkeeperDistanceBy100()
        {
            //arrange
            Shopkeeper shopkeeper = new Shopkeeper("", new Cart());
            GameEngine gameEngine = new GameEngine(shopkeeper, new Traveler());

            //act 
            gameEngine.Travel();
            Shopkeeper result = gameEngine.GetProtagInfo();

            //assert
            Assert.AreEqual(100, result.Distance);
        }

        [Test]
        public void EncounterNegativeCaseIfItems0()
        {
            //arrange
            Dictionary<Item, int> inv = new Dictionary<Item, int>();

            Cart cart = new Cart
            {
                Gold = 100,
                ProtectionLvl = 0,
                Inventory = inv
            };

            Shopkeeper shopkeeper = new Shopkeeper("", cart);
            GameEngine gameEngine = new GameEngine(shopkeeper, new Traveler());

            //act
            Result result = gameEngine.RunEncounter();

            //assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"You encounter a traveler, but because you have nothing to sell, you merely exchange" +
                        $" plesantries and continue on your way.", result.Message);
        }

        //[Test]
        //public void RunInteractCaseIfItemsMoreThan0() 
        //{
        //    //arrange
        //    Dictionary<Item, int> inv = new Dictionary<Item, int>();

        //    inv.Add(new Item("Sword", 100), 1);

        //    Cart cart = new Cart
        //    {
        //        Gold = 100,
        //        ProtectionLvl = 0,
        //        Inventory = inv
        //    };

        //    Shopkeeper shopkeeper = new Shopkeeper("", cart);
        //    GameEngine gameEngine = new GameEngine(shopkeeper, new Traveler());

        //    //act
        //    Result result = gameEngine.RunEncounter();

        //    //assert
        //    Assert.IsTrue(result.IsSuccess);
        //    Assert.AreEqual(200, shopkeeper.Cart.Gold);
        //    Assert.AreEqual(0, shopkeeper.Cart.Inventory.Count);
        //}
    }
}