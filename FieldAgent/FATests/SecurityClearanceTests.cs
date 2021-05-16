using FieldAgent.DAL;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FieldAgent.Entities;
using FieldAgent;
using System.Collections.Generic;

namespace FAtests
{
    public class SecurityClearanceTests
    {
        private FieldAgentsDbContext context;
        private SecurityClearanceEFRepo securityRepo;

        [SetUp]
        public void Setup()
        {
            context = FieldAgentsDbContext.GetDbContext();

            securityRepo = new SecurityClearanceEFRepo(context);
        }

        [Test]
        public void GetShouldReturnClearance()
        {
           Response<SecurityClearance> response = securityRepo.Get(5);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Black Ops", response.Data.SecurityClearanceName);
        }

        [Test]
        public void GetShouldReturnFailforInvalidId()
        {
            Response<SecurityClearance> response = securityRepo.Get(6);

            Assert.IsFalse(response.Success);
            Assert.Null(response.Data);
            Assert.AreEqual("Clearance with ID 6 not found", response.Message);
        }

        [Test]
        public void GetAllShouldReturnClearanceList()
        {

            Response<List<SecurityClearance>> response = securityRepo.GetAll();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(5, response.Data.Count);
        }

        [Test]
        public void GetAllShouldReturnEmptyList()
        {
            var inMemOptions = new DbContextOptionsBuilder<FieldAgentsDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;

            var emptyContext = new FieldAgentsDbContext(inMemOptions);
            emptyContext.Database.EnsureDeleted();
            emptyContext.Database.EnsureCreated();

            SecurityClearanceEFRepo emptyRepo = new SecurityClearanceEFRepo(emptyContext);

            var response = emptyRepo.GetAll();

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("No Security Clearances found", response.Message);
        }
    }
}
