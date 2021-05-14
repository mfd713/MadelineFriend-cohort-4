using FieldAgent.DAL;
using NUnit.Framework;
using FieldAgent.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FieldAgent.Entities;
using FieldAgent;

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
            Agency toAdd = new Agency {
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
            Assert.Fail();
        }
    }
}