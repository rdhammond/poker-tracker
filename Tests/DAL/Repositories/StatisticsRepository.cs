using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.Comparers.Dao;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class StatisticsRepositoryTests
        : ReadOnlyRepositoryTests<StatisticsRepository, StatisticsDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new StatisticsRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var expected = new StatisticsDao
            {
                AvgHourlyRatePerSession = 14.2m,
                HourlyRateStdDev = 1.14m,
                HourlyRateVariance = 2.25m,
                TotalHourlyRate = 11m,
                TotalHoursPlayed = 40m
            };
            DatabaseMock.DaoList.Add(expected);

            var actual = Repo.FindAllAsync().Result;
            Assert.AreEqual(1, actual.Count);

            Assert.IsTrue(
                new StatisticsComparer().Equals(expected, actual.First())
            );
        }
    }
}
