using NUnit.Framework;
using SustainableForaging.BLL.Tests.TestDoubles;
using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;

namespace SustainableForaging.BLL.Tests
{
    public class ReportTests
    {
        [Test]
        public void ShouldGenerateKgPerDayReport()
        {
            Report report = new Report(new ForageRepositoryDouble(), new ItemRepositoryDouble(), new ForagerRepositoryDouble());

            var query = report.GenerateDailyKGReport(DateTime.Parse("6/26/2020")).Value;

            Assert.AreEqual(1.25M, query[ItemRepositoryDouble.ITEM]);
        }

        [Test]
        public void ShouldReturnFalse()
        {
            Report report = new Report(new ForageRepositoryDouble(), new ItemRepositoryDouble(), new ForagerRepositoryDouble());

            var result1 = report.GenerateDailyKGReport(DateTime.Parse("6/27/2020"));

            Assert.IsFalse(result1.Success);

            var result2 = report.GenerateDailyValuePerCategoryReport(DateTime.Parse("6/27/2020"));
            Assert.IsFalse(result2.Success);
        }

        [Test]
        public void ShouldGenerateValuePerCategory()
        {
            Report report = new Report(new ForageRepositoryDouble(), new ItemRepositoryDouble(), new ForagerRepositoryDouble());

            var result = report.GenerateDailyValuePerCategoryReport(DateTime.Parse("6/26/2020")).Value;

            Assert.IsTrue(result.ContainsKey(Category.Edible));
            Assert.AreEqual(9.99M * 1.25M, result[Category.Edible]);
        }

    }
}
