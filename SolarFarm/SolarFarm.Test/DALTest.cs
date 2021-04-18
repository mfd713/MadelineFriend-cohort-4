using NUnit.Framework;
using SolarFarm.DAL;
using SolarFarm.Core;
using System.IO;
using System.Collections.Generic;
using System;

namespace SolarFarm.Test
{
    public class DALTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorShouldLoadFileIntoDictionary()
        {
            //arrange
            string testFile = "TestDataConstructor.csv";
            ISolarPanelRepository repo = new FileSolarPanelRepository(testFile);

            //act
            Dictionary<string, SolarPanel> panelList = repo.ReadAll();

            //Assert
            Assert.IsNotNull(panelList);
            Assert.IsNotNull(panelList["Main-1-1"]);
            Assert.AreEqual(1, panelList.Count);
            Assert.AreEqual(DateTime.Parse("01/01/2020"), panelList["Main-1-1"].DateInstalled);
            Assert.AreEqual(MaterialType.MulticrystallineSilicon, panelList["Main-1-1"].Material);
            Assert.IsTrue(panelList["Main-1-1"].IsTracking);

        }


        [Test]
        public void CreateSholdCreateAddPanelToDictionaryAndSave()
        {
            //arrange
            DateTime date = DateTime.Parse("02/02/2019");
            SolarPanel panel = new SolarPanel
            {
                Section = "TestSection",
                Row = 8,
                Column = 8,
                DateInstalled = date,
                Material = MaterialType.AmorphousSilicon,
                IsTracking = false
            };
            string testFileName = $"Test{DateTime.Now.Ticks}.csv";
            FileSolarPanelRepository repo = new FileSolarPanelRepository(testFileName);

            //act
            repo.Create(panel);
            Dictionary<string, SolarPanel> panelList = repo.ReadAll();

            //assert
            Assert.IsNotNull(panelList);
            Assert.IsNotNull(panelList["TestSection-8-8"]);
            Assert.AreEqual(1, panelList.Count);
            Assert.AreEqual(DateTime.Parse("02/02/2019"), panelList["TestSection-8-8"].DateInstalled);
            Assert.AreEqual(MaterialType.AmorphousSilicon, panelList["TestSection-8-8"].Material);
            Assert.IsFalse(panelList["TestSection-8-8"].IsTracking);
            File.Delete(testFileName);
        }


        [Test]
        public void DeleteShouldRemovePanelAndSaveFile()
        {
            //Arrange
            string testFile = "TestDataDeleteBefore.csv";
            ISolarPanelRepository beforeRepo = new FileSolarPanelRepository(testFile);

            string afterFile = "TestDataDeleteAfter.csv";
            ISolarPanelRepository afterRepo = new FileSolarPanelRepository(afterFile);

            SolarPanel panel = new SolarPanel
            {
                Section = "DeleterTest",
                Row = 2,
                Column = 3,
                DateInstalled = DateTime.Parse("03/03/2020"),
                Material = MaterialType.CadmiumTelluride,
                IsTracking = true
            };

            beforeRepo.Create(panel);

            //Act
            Dictionary<string, SolarPanel> panelList = beforeRepo.ReadAll();
            beforeRepo.Delete(panel);

            //Assert
            Assert.IsFalse(panelList.ContainsKey("DeleterTest-2-3"));
            Assert.AreEqual(0, panelList.Count);
            Assert.AreEqual(afterRepo.ReadAll(), beforeRepo.ReadAll());

            File.Delete(testFile);
        }

        [Test]
        public void UpdateShouldUpdateToEnteredPanel()
        {
            //arrange
            string testFile = "TestDataUpdate.csv";
            ISolarPanelRepository repo = new FileSolarPanelRepository(testFile);

            SolarPanel panel = new SolarPanel
            {
                Section = "Main",
                Row = 1,
                Column = 1,
                DateInstalled = DateTime.Parse("02/06/2018"),
                Material = MaterialType.CopperIndiumGalliumSelenide,
                IsTracking = false
            };

            //act
            repo.Update($"Main-1-1", panel);
            Dictionary<string, SolarPanel> panelList = repo.ReadAll();

            //assert
            Assert.AreEqual(panelList["Main-1-1"],panel);
        }    
    }
}