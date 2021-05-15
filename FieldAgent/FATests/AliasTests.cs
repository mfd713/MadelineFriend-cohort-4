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
    public class AliasTests
    {
        private AliasEFRepo aliasRepo;
        private FieldAgentsDbContext context;
        private Alias testAlias;

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

            aliasRepo = new AliasEFRepo(context);
            testAlias = new Alias
            {
                AliasName = "Swordfish",
                Persona = "Non Grata"
            };
        }

        [Test]
        public void InsertShouldAddAliasToDB()
        {
            Response<Alias> response = aliasRepo.Insert(testAlias);

            Assert.IsTrue(response.Success);
            Assert.NotZero(response.Data.AliasId);
        }

        [Test]
        public void GetShouldReturnAlias()
        {
            aliasRepo.Insert(testAlias);

            Response<Alias> response = aliasRepo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Swordfish", response.Data.AliasName);
        }

        [Test]
        public void GetWithInvalidIDShouldReturnFail()
        {
            aliasRepo.Insert(testAlias);

            Response<Alias> response = aliasRepo.Get(2);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.AreEqual("Alias with ID 2 not found", response.Message);
        }

        [Test]
        public void GetByAgentShouldReturnAlias()
        {
            Agent testAgent = new Agent
            {
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = DateTime.Parse("01/01/90"),
                Height = 175M,
                Missions = new List<Mission>(),
                Aliases = new List<Alias>(),
                AgentId = 1
            };

            testAlias.Agent = testAgent;

            aliasRepo.Insert(testAlias);

            Response<List<Alias>> response = aliasRepo.GetByAgent(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("Swordfish", response.Data[0].AliasName);
        }

        [Test]
        public void GetByAgentWithInvalidIDShouldReturnFail()
        {
            Agent testAgent = new Agent
            {
                FirstName = "Tester",
                LastName = "Testington",
                DateOfBirth = DateTime.Parse("01/01/90"),
                Height = 175M,
                Missions = new List<Mission>(),
                Aliases = new List<Alias>(),
                AgentId = 1
            };

            testAlias.Agent = testAgent;

            aliasRepo.Insert(testAlias);

            Response<List<Alias>> response = aliasRepo.GetByAgent(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(0, response.Data.Count);
            Assert.AreEqual("Aliases for Agent ID 2 not found", response.Message);
        }

        [Test]
        public void UpdateShouldUpdateAlias()
        {
            Alias toUpdate = aliasRepo.Insert(testAlias).Data;

            toUpdate.AliasName = "Cycl0ne";
            Response response = aliasRepo.Update(toUpdate);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Cycl0ne", aliasRepo.Get(1).Data.AliasName);
        }

        [Test]
        public void UpdateShouldFailForInvalidAlias()
        {
            aliasRepo.Insert(testAlias);

            Alias toUpdate = new Alias
            {
                AliasId = 2,
                AliasName = "Cycl0ne"
            };

            Response response = aliasRepo.Update(toUpdate);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Could not update Alias with ID 2", response.Message);
        }

        [Test]
        public void DeleteShouldDeleteAlias()
        {
            aliasRepo.Insert(testAlias);

            Response response = aliasRepo.Delete(1);

            Assert.IsTrue(response.Success);
            Assert.IsNull(aliasRepo.Get(1).Data);
        }

        [Test]
        public void DeleteShouldReturnFailForInvalidAlias()
        {
            aliasRepo.Insert(testAlias);

            Response response = aliasRepo.Delete(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Could not delete Alias with ID 2", response.Message);
            Assert.IsNotNull(aliasRepo.Get(1));
        }
    }
}