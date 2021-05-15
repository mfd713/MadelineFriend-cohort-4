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
    public class AgencyTests
    {
        private FieldAgentsDbContext context;
        private AgencyEFRepo agencyRepo;

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

            agencyRepo = new AgencyEFRepo(context);
        }

        [Test]
        public void InsertShouldAddAgencyToDatabase()
        {
            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            Response<Agency> response = agencyRepo.Insert(toAdd);

            Assert.IsTrue(response.Success);
            Assert.NotZero(response.Data.AgencyId);
        }

        [Test]
        public void GetAllShouldReturnAllAgenciesInDB()
        {
            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            agencyRepo.Insert(toAdd);
            Response<List<Agency>> response = agencyRepo.GetAll();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("Test", response.Data[0].ShortName);
        }

        [Test]
        public void GetAllOnEmptyDBShouldReturnFalse()
        {
            Response<List<Agency>> response = agencyRepo.GetAll();

            Assert.IsFalse(response.Success);
            Assert.AreEqual(0, response.Data.Count);
            Assert.AreEqual("No rows found", response.Message);
        }

        [Test]
        public void GetShouldReturnAgencyMatchingID()
        {
            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            agencyRepo.Insert(toAdd);
            Response<Agency> response = agencyRepo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Testley", response.Data.LongName);
        }

        [Test]
        public void GetWithInvalidIDShouldReturnFalse()
        {
            Response<Agency> response = agencyRepo.Get(2);

            Assert.IsNull(response.Data);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Agency with ID 2 not found", response.Message);
        }

        [Test]
        public void UpdateShouldUpdateValidAgency()
        {
            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            Agency toUpdate = agencyRepo.Insert(toAdd).Data;

            toUpdate.ShortName = "ABC";

            Response response = agencyRepo.Update(toUpdate);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(String.IsNullOrEmpty(response.Message));
        }

        [Test]
        public void UpdateWithNoIdShouldNotUpdate()
        {

            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            agencyRepo.Insert(toAdd);
            Agency toUpdate = new Agency
            {
                ShortName = "ABC",
                LongName = "Testley",
                AgencyId = 2
            };

            Response response = agencyRepo.Update(toUpdate);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Agency with ID 2 not found", response.Message);
        }

        [Test]
        public void DeleteShouldDelete()
        {
            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            agencyRepo.Insert(toAdd);

            Response response = agencyRepo.Delete(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(0, agencyRepo.GetAll().Data.Count);
        }

        [Test]
        public void DeleteShouldNotDeleteInvalidId()
        {
            Agency toAdd = new Agency
            {
                ShortName = "Test",
                LongName = "Testley"
            };

            agencyRepo.Insert(toAdd);

            Response response = agencyRepo.Delete(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(1, agencyRepo.GetAll().Data.Count);
            Assert.AreEqual("Agency with ID 2 not found", response.Message);
        }
    }
}