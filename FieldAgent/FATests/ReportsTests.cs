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
    public class ReportsTests
    {
        private string connectionString;
        private ReportsADORepository reportsRepo;

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<FieldAgentsDbContext>();

            var config = builder.Build();

            string connectionString = config["ConnectionStrings:FieldAgent"];
            return connectionString;
        }

        [SetUp]
        public void Setup()
        {
            connectionString = GetConnectionString();
            reportsRepo = new ReportsADORepository(connectionString);
        }

        [Test]
        public void GetTopAgentsShouldReturnAgentList()
        {
            var response = reportsRepo.GetTopAgents();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(3, response.Data.Count);
            Assert.AreEqual(4, response.Data[0].CompletedMissionCount);
        }

        [Test]
        public void GetAgentsFromBadConnectionShouldReturnFail()
        {
            var emptyRepo = new ReportsADORepository("");
            var response = emptyRepo.GetTopAgents();

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("Database empty", response.Message);
        }

        [Test]
        public void GetPensionListShouldReturnAgents()
        {
            var response = reportsRepo.GetPensionList(2);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.IsTrue(response.Data[0].NameLastFirst.Contains("Lanna"));
        }

        [Test]
        public void GetPensionFromNoPensionersShouldReturnFail()
        {
            var emptyRepo = new ReportsADORepository("");
            var response = emptyRepo.GetPensionList(1);

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("No results for that query", response.Message);
        }

        [Test]
        public void AuditClearanceShouldReturnListOfObjects()
        {
            var response = reportsRepo.AuditClearance(4, 1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.IsTrue(response.Data[0].NameLastFirst.Contains("Bond"));
            Assert.IsNull(response.Data[0].DeactivationDate);
        }

        [Test]
        public void AuditClearanceEmptySearchShouldReturnFail()
        {
            var emptyRepo = new ReportsADORepository("");
            var response = emptyRepo.AuditClearance(1, 1);

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("No results for that query", response.Message);
        }
    }
}
