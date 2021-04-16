using NUnit.Framework;
using SolarFarm.DAL;
using SolarFarm.Core;
using System.IO;
using System.Collections.Generic;

namespace SolarFarm.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorShouldLoadFileIntoDictionary()
        {
            //arrange
            string testFile = "TestData.csv";
            ISolarPanelRepository repo = new FileSolarPanelRepository(testFile);

            //act
            Dictionary<string, SolarPanel> panelList = repo.ReadAll();

            //Assert
            Assert.IsNotNull(panelList);
            Assert.IsNotNull(panelList["Main-1-1"]);
            Assert.AreEqual(1,panelList.Count);
            Assert.AreEqual(MaterialType.MulticrystallineSilicon,panelList["Main-1-1"].Material);
            Assert.IsTrue(panelList["Main-1-1"].IsTracking);

        }
    }
}