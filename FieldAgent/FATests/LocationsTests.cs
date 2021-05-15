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
    public class LocationsTests
    {
        private LocationEFRepo locationRepo;
        private FieldAgentsDbContext context;
        private Location testLocation;

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

            locationRepo = new LocationEFRepo(context);
            testLocation = new Location
            {
                LocationName = "Starbucks",
                Street1 = "2700 N Clark St",
                City = "Chicago",
                PostalCode = "60614",
                CountryCode = "01"
            };
        }

        [Test]
        public void InsertShouldAddLocationtoDB()
        {
            Response<Location> response = locationRepo.Insert(testLocation);

            Assert.IsTrue(response.Success);
            Assert.NotZero(response.Data.LocationId);
        }

        [Test]
        public void GetShouldReturnLocation()
        {
            locationRepo.Insert(testLocation);

            Response<Location> response = locationRepo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Starbucks", response.Data.LocationName);
        }

        [Test]
        public void GetWithInvalidIDShouldReturnFail()
        {
            locationRepo.Insert(testLocation);

            Response<Location> response = locationRepo.Get(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Location with ID 2 not found", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void GetAgencyShouldReturnLocationList()
        {
            Agency agency = new Agency
            {
                AgencyId = 1,
                ShortName = "Test",
                LongName = "Testley",
                Locations = new List<Location>()
            };

            testLocation.AgencyId = 1;
            agency.Locations.Add(testLocation);

            locationRepo.Insert(testLocation);
            Response<List<Location>> response = locationRepo.GetByAgency(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("Starbucks", response.Data[0].LocationName);
        }

        [Test]
        public void GetWithInvalidAgencyShouldReturnFail()
        {
            Agency agency = new Agency
            {
                AgencyId = 1,
                ShortName = "Test",
                LongName = "Testley",
                Locations = new List<Location>()
            };

            testLocation.AgencyId = 1;
            agency.Locations.Add(testLocation);

            locationRepo.Insert(testLocation);
            Response<List<Location>> response = locationRepo.GetByAgency(2);

            Assert.IsFalse(response.Success);
            Assert.Zero(response.Data.Count);
            Assert.AreEqual("No Locations with Agency ID 2 found", response.Message);
        }

        [Test]
        public void UpdateShouldUpdateLocation()
        {
            Location toUpdate = locationRepo.Insert(testLocation).Data;

            toUpdate.City = "Temecula";
            Response response = locationRepo.Update(toUpdate);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("Temecula", locationRepo.Get(1).Data.City);
        }

        [Test]
        public void UpdateShouldReturnFailForInvalidLocation()
        {
            locationRepo.Insert(testLocation);
            Location toUpdate = new Location
            {
                City = "Temecula",
                LocationId = 2
            };

            Response response = locationRepo.Update(toUpdate);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Location with ID 2 not found", response.Message);
            Assert.AreEqual("Chicago", locationRepo.Get(1).Data.City);
        }

        [Test]
        public void DeleteShouldDeleteLocation()
        {
            locationRepo.Insert(testLocation);

            Response response = locationRepo.Delete(1);

            Assert.IsTrue(response.Success);
            Assert.IsNull(locationRepo.Get(1).Data);
        }

        [Test]
        public void DeleteShouldReturnFailForInvalidLocation()
        {
            locationRepo.Insert(testLocation);

            Response response = locationRepo.Delete(2);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Location with ID 2 not found", response.Message);
            Assert.AreEqual("Chicago", locationRepo.Get(1).Data.City);
        }
    }
}
