using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace SustainableForaging.DAL.Tests
{
    public class ForagerFileRepositoryTest
    {
        [Test]
        public void ShouldFindAll()
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
            List<Forager> all = repo.FindAll();
            Assert.AreEqual(1000, all.Count);
        }

        [Test]
        public void ShouldAdd()
        {
            //arrange
            //create a new file using File class. close the file
            var tempFile = File.Create("temp.csv");
            tempFile.Close();
            ForagerFileRepository repo = new ForagerFileRepository("temp.csv");

            //create a new forager
            Forager toAdd = new Forager
            {
                FirstName = "Testy",
                LastName = "Smith",
                Id = "abc-123",
                State = "WI"
            };

            //act
            //use Add() to add forager to that new file
            repo.Add(toAdd);
            //use FindAll to read back the result
            List<Forager> all = repo.FindAll();

            //assert
            //that the result length is 1
            Assert.AreEqual(1, all.Count);
            //that the result forager is the one created in here
            Assert.AreEqual(toAdd.FirstName, all[0].FirstName);
            Assert.AreEqual(toAdd.LastName, all[0].LastName);
            Assert.AreEqual(toAdd.State, all[0].State);

            //delete file as cleanup
            File.Delete("temp.csv");
        }
    }
}
