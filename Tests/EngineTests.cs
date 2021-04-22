using NUnit.Framework;
using ShopKeepExercise.GameEngine;
using ShopKeepExercise;
namespace Tests
{
    public class EngineTests
    {

        [Test]
        public void TravelShouldIncreaseShopkeeperDistanceBy100()
        {
            //arrange
            Shopkeeper shopkeeper = new Shopkeeper("",new Cart());
            GameEngine gameEngine = new GameEngine(shopkeeper);

            //act 
            gameEngine.Travel();
            Shopkeeper result = gameEngine.GetProtagInfo();

            //assert
            Assert.AreEqual(100, result.Distance);
        }
    }
}