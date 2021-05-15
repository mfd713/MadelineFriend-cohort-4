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
    public class AgencyAgentTests
    {
        private AgencyAgentEFRepo agencyAgentRepo;
        private FieldAgentsDbContext context;
        private AgencyAgent testAgencyAgent;

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

            agencyAgentRepo = new AgencyAgentEFRepo(context);
            testAgencyAgent = new AgencyAgent
            {
                AgentId = 1,
                AgencyId = 1,
                ActivationDate = new DateTime(20, 1, 1),
                IsActive = true,
                SecurityClearanceId = 1
            };
        }

        [Test]
        public void InsertshouldAddAgencyAgentEntry()
        {
            Response<AgencyAgent> response = agencyAgentRepo.Insert(testAgencyAgent);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.AgencyId);
            Assert.AreEqual(1, response.Data.AgentId);
        }

        [Test]
        public void GetShouldReturnAgencyAgent()
        {
            agencyAgentRepo.Insert(testAgencyAgent);
            Response<AgencyAgent> response = agencyAgentRepo.Get(1, 1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.AgencyId);
            Assert.AreEqual(1, response.Data.AgentId);
            Assert.IsTrue(response.Data.IsActive);
        }

        [Test]
        public void GetShouldNotFindAgencyAgent()
        {
            agencyAgentRepo.Insert(testAgencyAgent);
            Response<AgencyAgent> response = agencyAgentRepo.Get(2, 1);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.AreEqual("Could not find AgencyAgent with IDs 2, 1", response.Message);
        }

        [Test]
        public void GetAgencyShouldReturnAgencyAgentList()
        {
            agencyAgentRepo.Insert(testAgencyAgent);

            var response = agencyAgentRepo.GetByAgency(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual(1, response.Data[0].AgencyId);
        }

        [Test]
        public void GetAgencyShouldFailForInvalidID()
        {
            agencyAgentRepo.Insert(testAgencyAgent);

            var response = agencyAgentRepo.GetByAgency(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(0, response.Data.Count);
            Assert.AreEqual("Could not find AgencyAgents with AgencyID 2", response.Message);
        }

        [Test]
        public void GetAgentShouldReturnAgentAgentList()
        {
            agencyAgentRepo.Insert(testAgencyAgent);

            var response = agencyAgentRepo.GetByAgent(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual(1, response.Data[0].AgentId);
        }

        [Test]
        public void GetAgentShouldFailForInvalidID()
        {
            agencyAgentRepo.Insert(testAgencyAgent);

            var response = agencyAgentRepo.GetByAgent(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(0, response.Data.Count);
            Assert.AreEqual("Could not find AgencyAgents with AgentID 2", response.Message);
        }

        [Test]
        public void UpdateShouldUpdateValidAgencyAgent()
        {

            AgencyAgent toUpdate = agencyAgentRepo.Insert(testAgencyAgent).Data;

            toUpdate.DeactivationDate = new DateTime(21,01,02);
            toUpdate.IsActive = false;

            Response response = agencyAgentRepo.Update(toUpdate);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(String.IsNullOrEmpty(response.Message));
        }

        [Test]
        public void UpdateWithNoIdShouldNotUpdate()
        {
            agencyAgentRepo.Insert(testAgencyAgent);
            AgencyAgent toUpdate = new AgencyAgent
            {
                AgentId = 2,
                AgencyId = 2
            };

            Response response = agencyAgentRepo.Update(toUpdate);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("AgencyAgent not found", response.Message);
        }

        [Test]
        public void DeleteShouldDeleteAgencyAgent()
        {
            agencyAgentRepo.Insert(testAgencyAgent);

            Response response = agencyAgentRepo.Delete(1,1);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(String.IsNullOrEmpty(response.Message));
        }

        [Test]
        public void DeleteShouldNotDeleteInvalidAgencyAgentIds()
        {

            agencyAgentRepo.Insert(testAgencyAgent);

            Response response = agencyAgentRepo.Delete(2,1);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("AgencyAgent with ID 2, 1 not found", response.Message);
        }
    }
}
