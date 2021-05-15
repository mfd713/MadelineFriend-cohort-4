using FieldAgent.DAL;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FieldAgent.Entities;
using FieldAgent;
using System.Collections.Generic;
using System;

namespace FAtests
{

    public class AgentTests
    {
        private AgentEFRepo agentRepo;
        private FieldAgentsDbContext context;
        private Agent testAgent;

        private static FieldAgentsDbContext GetDbContext()
        {
            var builder = new ConfigurationBuilder();

            builder.AddInMemoryCollection();

            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("testDatabase")
                .Options;

            return new FieldAgentsDbContext(options);
        }

        [SetUp]
        public void Setup()
        {
            context = GetDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            agentRepo = new AgentEFRepo(context);
            testAgent = new Agent
            {
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = DateTime.Parse("01/01/90"),
                Height = 175M,
                Missions = new List<Mission>()

            };
        }

        [Test]
        public void InsertShouldAddAgentToDatabase()
        {
            Agent toAdd = new Agent
            {
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = DateTime.Parse("01/01/90"),
                Height = 175M,
            };

            Response<Agent> response = agentRepo.Insert(toAdd);

            Assert.IsTrue(response.Success);
            Assert.NotZero(response.Data.AgentId);
        }

        [Test]
        public void GetMissionsShouldReturnAllMissions()
        {

            testAgent.Missions.Add(new Mission
            {
                MissionId = 1,
                CodeName = "K.I.D.S NEXT DOOR",
                StartDate = DateTime.Parse("01/01/20"),
                ProjectedEndDate = DateTime.Parse("01/01/21"),
            });

            agentRepo.Insert(testAgent);
            Response<List<Mission>> response = agentRepo.GetMissions(1);

            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("K.I.D.S NEXT DOOR", response.Data[0].CodeName);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void GetMissionsOnEmptyDBShouldReturnFalse()
        {
            agentRepo.Insert(testAgent);
            Response<List<Mission>> response = agentRepo.GetMissions(1);

            Assert.AreEqual(0, response.Data.Count);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("No Missions found", response.Message);
        }

        [Test]
        public void GetShouldReturnAgentMatchingID()
        {
            agentRepo.Insert(testAgent);
            Response<Agent> response = agentRepo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Testington", response.Data.LastName);
        }

        [Test]
        public void GetWithInvalidIDShouldReturnFalse()
        {
            Response<Agent> response = agentRepo.Get(2);

            Assert.IsNull(response.Data);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Agent with ID 2 not found", response.Message);
        }

        [Test]
        public void UpdateShouldUpdateValidAgent()
        {

            Agent toUpdate = agentRepo.Insert(testAgent).Data;

            toUpdate.FirstName = "ABC";

            Response response = agentRepo.Update(toUpdate);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(String.IsNullOrEmpty(response.Message));
        }

        [Test]
        public void UpdateWithNoIdShouldNotUpdate()
        {
            agentRepo.Insert(testAgent);
            Agent toUpdate = new Agent
            {
                AgentId = 2
            };

            Response response = agentRepo.Update(toUpdate);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Agent with ID 2 not found", response.Message);
        }

        [Test]
        public void DeleteShouldDelete()
        {
            agentRepo.Insert(testAgent);

            Response response = agentRepo.Delete(1);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(String.IsNullOrEmpty(response.Message));
        }

        [Test]
        public void DeleteShouldNotDeleteInvalidId()
        {

            agentRepo.Insert(testAgent);

            Response response = agentRepo.Delete(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Agent with ID 2 not found", response.Message);
        }
    }
}
