using System;
using System.Collections.Generic;
using NUnit.Framework;
using SustainableForaging.BLL.Tests.TestDoubles;
using SustainableForaging.Core.Models;
using System.IO;
using SustainableForaging.DAL;

namespace SustainableForaging.BLL.Tests
{
    public class ForagerServiceTest
    {

        [Test]
        public void ShouldFindForager()
        {
            //arrange
            ForagerRepositoryDouble repo = new ForagerRepositoryDouble();
            ForagerService service = new ForagerService(repo);

            //act
            List<Forager> result = service.FindByLastName("Sis");

            //assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Jilly", result[0].FirstName);
        }

        [Test]
        public void ShouldNotFindForager()
        {
            ForagerRepositoryDouble repo = new ForagerRepositoryDouble();
            ForagerService service = new ForagerService(repo);

            List<Forager> result = service.FindByLastName("A");

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void CanAddValidForager()
        {
            var tempFile = File.Create("temp.csv");
            tempFile.Close();
            ForagerFileRepository repo = new ForagerFileRepository("temp.csv");
            ForagerService service = new ForagerService(repo);
            Forager forager = new Forager
            {
                FirstName = "Sally",
                LastName = "Smith",
                Id = "abc-123",
                State = "AL"
            };

            Result<Forager> result = service.Add(forager);


            Assert.IsTrue(result.Success);
            Assert.NotNull(result.Value);
            Assert.AreEqual(36, result.Value.Id.Length);

            File.Delete("temp.csv");
        }

        [TestCase("", "Last", "ST")]
        [TestCase("First", "", "ST")]
        [TestCase("First", "Last", "")]
        [TestCase("First", "Last", "ST")]
        public void CannotAddInvalid(string first, string last, string state)
        {
            var tempFile = File.Create("temp.csv");
            tempFile.Close();
            ForagerFileRepository repo = new ForagerFileRepository("temp.csv");
            ForagerService service = new ForagerService(repo);

            Forager forager = new Forager
            {
                Id = "abc-123",
                FirstName = first,
                LastName = last,
                State = state
            };

            Forager noCopy = new Forager
            {
                FirstName = "First",
                LastName = "Last",
                State = "ST"
            };

            service.Add(noCopy);
            Result<Forager> result = service.Add(forager);

            Assert.IsFalse(result.Success);
            //TOD: assert to check the messages that were returned to validate correct reason for failure
            File.Delete("temp.csv");
        }
    }
}
