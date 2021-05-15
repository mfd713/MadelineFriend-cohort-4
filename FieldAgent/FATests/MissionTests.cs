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
    public class MissionTests
    {
        private FieldAgentsDbContext context;
        private MissionEFRepo missonRepo;
        private Mission testMission;

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

            missonRepo = new MissionEFRepo(context);

            testMission = new Mission
            {
                CodeName = "K.I.D.S NEXT DOOR",
                StartDate = new DateTime(20, 01, 01),
                ProjectedEndDate = new DateTime(21, 01, 02),
                Agents = new List<Agent>()
            };
        }

        [Test]
        public void InstertShouldAddMission()
        {
            Response<Mission> response = missonRepo.Insert(testMission);

            Assert.IsTrue(response.Success);
            Assert.NotZero(response.Data.MissionId);
        }

        [Test]
        public void GetShouldReturnMission()
        {
            missonRepo.Insert(testMission);
            Response<Mission> response = missonRepo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("K.I.D.S NEXT DOOR", response.Data.CodeName);
        }

        [Test]
        public void GetShouldReturnFailForInvalidID()
        {
            missonRepo.Insert(testMission);
            Response<Mission> response = missonRepo.Get(2);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.AreEqual("No Mission with ID 2 found", response.Message);
        }

        [Test]
        public void GetByAgencyShouldReturnMission()
        {
            Agency agency = new Agency
            {
                AgencyId = 1,
                ShortName = "Short"
            };

            testMission.Agency = agency;
            missonRepo.Insert(testMission);

            Response<List<Mission>> response = missonRepo.GetByAgency(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("K.I.D.S NEXT DOOR", response.Data[0].CodeName);
        }

        [Test]
        public void GetByAgencyNotFoundShouldReturnNull()
        {
            Agency agency = new Agency
            {
                AgencyId = 1,
                ShortName = "Short"
            };

            testMission.Agency = agency;
            missonRepo.Insert(testMission);

            Response<List<Mission>> response = missonRepo.GetByAgency(2);

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("No Missions found for agency ID 2", response.Message);
        }

        [Test]
        public void GetByAgentShouldReturnMissions()
        {
            Agent agent = new Agent
            {
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = new DateTime(90, 01, 01),
                Height = 175M,
                Missions = new List<Mission>()
            };

            agent.Missions.Add(testMission);
            testMission.Agents.Add(agent);
            missonRepo.Insert(testMission);
            Response<List<Mission>> response = missonRepo.GetByAgent(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("K.I.D.S NEXT DOOR", response.Data[0].CodeName);
        }

        [Test]
        public void GetByAgentWithInvalidIDShouldReturnFalse()
        {
            Agent agent = new Agent
            {
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = new DateTime(90, 01, 01),
                Height = 175M,
                Missions = new List<Mission>()
            };

            agent.Missions.Add(testMission);
            testMission.Agents.Add(agent);
            missonRepo.Insert(testMission);
            Response<List<Mission>> response = missonRepo.GetByAgent(2);

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("No Missions found for Agent ID 2", response.Message);
        }

        [Test]
        public void UpdateShouldUpdateMission()
        {
            Mission toUpdate = missonRepo.Insert(testMission).Data;

            toUpdate.Notes = "gonna be a tough one";

            Response response = missonRepo.Update(toUpdate);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("gonna be a tough one", missonRepo.Get(1).Data.Notes);
        }

        [Test]
        public void UpdateShouldReturnFailForInvalidID()
        {

            Mission toUpdate = new Mission
            {
                MissionId = 2,
                CodeName = "K.I.D.S NEXT DOOR",
                StartDate = new DateTime(20, 01, 01),
                ProjectedEndDate = new DateTime(21, 01, 02),
                Agents = new List<Agent>()
            };

            Agent agent = new Agent
            {
                AgentId = 1,
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = new DateTime(90, 01, 01),
                Height = 175M,
                Missions = new List<Mission>()
            };

            agent.Missions.Add(toUpdate);
            toUpdate.Agents.Add(agent);

            Response response = missonRepo.Update(toUpdate);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("No Mission with ID 2 found", response.Message);
        }
        [Test]
        public void DeleteShouldDeleteMission()
        {
            missonRepo.Insert(testMission);

            Response response = missonRepo.Delete(1);

            Assert.IsTrue(response.Success);
            Assert.IsNull(missonRepo.Get(1).Data);
        }

        [Test]
        public void DeleteShouldReturnFalseForInvalidID()
        {
            missonRepo.Insert(testMission);

            Response response = missonRepo.Delete(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Mission with ID 2 not found", response.Message);
            Assert.AreEqual("K.I.D.S NEXT DOOR", missonRepo.Get(1).Data.CodeName);
        }
    }
    
}
