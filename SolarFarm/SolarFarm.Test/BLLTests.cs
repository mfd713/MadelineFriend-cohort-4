using NUnit.Framework;
using SolarFarm.DAL;
using SolarFarm.Core;
using SolarFarm.BLL;
using System.IO;
using System.Collections.Generic;
using System;

namespace SolarFarm.Test
{
    public class BLLTests
    {


        [TestCase("",1,2,"01/01/2020","AmorphousSilicon","true")]
        [TestCase("Test",-59,2,"01/01/2020", "AmorphousSilicon", "true")]
        [TestCase("Test",1,261,"01/01/2020", "AmorphousSilicon", "true")]
        [TestCase("Test",1,2,"10/01/2021", "AmorphousSilicon", "true")]
        [TestCase("Main",1,2, "03/04/2021", "CadmiumTelluride", "false")]

        public void CreateShouldNotHappenIfPanelIsInvalid(string section, int row, int column, string date, string material,string isTracking)
        {
            //arrange
            SolarPanel panel = new SolarPanel
            {
                Section = section,
                Row = row,
                Column = column,
                DateInstalled = DateTime.Parse(date),
                Material = Enum.Parse<MaterialType>(material),
                IsTracking = bool.Parse(isTracking)
            };
            ISolarPanelRepository repo = new FileSolarPanelRepository("TestDataBLLBadCreate.csv");
            SolarPanelService service = new SolarPanelService(repo);

            //act
            SolarPanelResult result = service.Create(panel);

            //assert
            Assert.IsFalse(result.Success);
            Assert.IsNotEmpty(result.Message);
            Assert.IsNotNull(result.Message);
            Assert.AreEqual(panel, result.Data);
            Assert.AreEqual(1, repo.ReadAll().Count);
        }

        [Test]
        public void CreateShouldHappenIfPanelIsValid()
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
            SolarPanelService service = new SolarPanelService(repo);

            //act
            SolarPanelResult result = service.Create(panel);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Success. TestSection-8-8 added.", result.Message);
            Assert.AreEqual(panel, result.Data);
            File.Delete(testFileName);
        }

        [Test]
        public void ReadBySectionShouldReturnAllPanelsInSection()
        {
            //arrange
            FileSolarPanelRepository repo = new FileSolarPanelRepository("TestDataBLLRead.csv");
            SolarPanelService service = new SolarPanelService(repo);
            Dictionary<string, SolarPanel> firstSection;
            Dictionary<string, SolarPanel> secondSection;
            ListOfPanelsResult firstResult = new ListOfPanelsResult();
            ListOfPanelsResult secondResult = new ListOfPanelsResult();

            //act
            firstResult = service.ReadBySection("Test1");
            secondResult = service.ReadBySection("Test2");

            firstSection = firstResult.Data;
            secondSection = secondResult.Data;

            //assert
            Assert.IsNotNull(firstSection);
            Assert.IsNotNull(secondSection);
            Assert.AreEqual(3, firstSection.Count);
            Assert.AreEqual(2, secondSection.Count);
            Assert.IsTrue(firstSection["Test1-100-3"].IsTracking);
            Assert.IsFalse(secondSection["Test2-1-3"].IsTracking);
        }

        [Test]
        public void ReadBySectionShouldReturnNullAndFalseIfNotPresent()
        {
            //arrange
            FileSolarPanelRepository repo = new FileSolarPanelRepository("TestDataBLLRead.csv");
            SolarPanelService service = new SolarPanelService(repo);
            Dictionary<string, SolarPanel> notASection;
            ListOfPanelsResult result = new ListOfPanelsResult();

            //act
            result = service.ReadBySection("Test3");
            notASection = result.Data;

            //assert
            Assert.IsNull(notASection);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("No panels found in that section", result.Message);
        }

        [Test]
        public void ReadSinglePanelShouldReturnGivenPanelIfPresent()
        {
            //arrange
            FileSolarPanelRepository repo = new FileSolarPanelRepository("TestDataBLLRead.csv");
            SolarPanelService service = new SolarPanelService(repo);
            SolarPanel resultingPanel;
            SolarPanel existingPanel = new SolarPanel
            {
                Section = "Test1",
                Row = 50,
                Column = 2,
                DateInstalled = DateTime.Parse("02 / 05 / 2020"),
                Material = MaterialType.AmorphousSilicon,
                IsTracking = false
            };
            SolarPanelResult result = new SolarPanelResult();

            //act
            result = service.ReadSinglePanel("Test1", 50, 2);
            resultingPanel = result.Data;

            //assert
            Assert.AreEqual(existingPanel, resultingPanel);
        }

        [Test]
        public void ReadSinglePanelShouldReturnFalseAndNullIfNotPresent()
        {
            //arrange
            FileSolarPanelRepository repo = new FileSolarPanelRepository("TestDataBLLRead.csv");
            SolarPanelService service = new SolarPanelService(repo);
            SolarPanel resultingPanel;
            SolarPanelResult result = new SolarPanelResult();


            //act
            result = service.ReadSinglePanel("Test1", 51, 2);
            resultingPanel = result.Data;

            //assert
            Assert.IsNull(resultingPanel);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Message, "Panel not found");
        }

        [Test]
        public void UpdatePanelShouldUpdateIfUpdatesValid()
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

            SolarPanel upatePanel = new SolarPanel
            {
                Section = "TestSection",
                Row = 8,
                Column = 8,
                DateInstalled = DateTime.Parse("02/02/2018"),
                Material = MaterialType.MulticrystallineSilicon,
                IsTracking = true
            };
            string testFileName = $"Test{DateTime.Now.Ticks}.csv";
            FileSolarPanelRepository repo = new FileSolarPanelRepository(testFileName);
            SolarPanelService service = new SolarPanelService(repo);

            //act
            service.Create(panel);
            SolarPanelResult result = service.Update(upatePanel);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Success.", result.Message);
            Assert.AreEqual(upatePanel, result.Data);
            Assert.AreEqual(1, repo.ReadAll().Count);
            File.Delete(testFileName);
        }

        [Test]

        public void UpdatePanelShouldReturnNullAndFalseIfUpdatesInvalid()
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

            SolarPanel upatePanel = new SolarPanel
            {
                Section = "TestSection",
                Row = 8,
                Column = 8,
                DateInstalled = DateTime.Parse("10/01/2021"),
                Material = MaterialType.MulticrystallineSilicon,
                IsTracking = true
            };
            string testFileName = $"Test{DateTime.Now.Ticks}.csv";
            FileSolarPanelRepository repo = new FileSolarPanelRepository(testFileName);
            SolarPanelService service = new SolarPanelService(repo);

            //act
            service.Create(panel);
            SolarPanelResult result = service.Update(upatePanel);

            //assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Date Installed cannot be in the future", result.Message);
            Assert.IsNull(result.Data);
            Assert.AreEqual(1, repo.ReadAll().Count);
            Assert.AreEqual(panel, service.ReadSinglePanel("TestSection", 8, 8).Data);
            File.Delete(testFileName);
        }
        [Test]
        public void DeletePanelShouldDeleteGivenPanelAndReturnResult()
        {
            //arrange
            string testFileName = $"Test{DateTime.Now.Ticks}.csv";
            FileSolarPanelRepository repo = new FileSolarPanelRepository(testFileName);
            SolarPanelService service = new SolarPanelService(repo);
            SolarPanel toDeletePanel = new SolarPanel
            {
                Section = "TestSection",
                Row = 8,
                Column = 8,
                DateInstalled = DateTime.Parse("10/01/2021"),
                Material = MaterialType.MulticrystallineSilicon,
                IsTracking = true
            };

            //act
            service.Create(toDeletePanel);
            SolarPanelResult result = service.Delete(toDeletePanel);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual($"{toDeletePanel.GetKey()} removed.", result.Message);
            Assert.AreEqual(toDeletePanel, result.Data);
        }
    }
}
