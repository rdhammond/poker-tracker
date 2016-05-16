using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using PokerTracker.Tests.Comparers.Objects;
using System.Linq;

namespace PokerTracker.Tests.BLL.Services
{
    [TestClass]
    public class StatisticsServiceTests
        : LookupServiceTests<IStatisticsRepository, StatisticsDao, StatisticsRepositoryMock>
    {
        private IStatisticsService _service;

        [TestInitialize]
        public void SetUp()
        {
            Setup();
            _service = new StatisticsService(Mapper, RepoMock.Object);
        }

        [TestMethod]
        public void GetAllAsync_Works()
        {
            var expected = new StatisticsDao
            {
                AvgHourlyRatePerSession = 143.1m,
                HourlyRateStdDev = 14.1m,
                HourlyRateVariance = 77.2m,
                TotalHourlyRate = -148.4m,
                TotalHoursPlayed = 4m
            };
            RepoMock.DaoList.Add(expected);

            var actual = _service.GetAllAsync().Result;
            Assert.AreEqual(1, actual.Length);

            Assert.IsTrue(
                new StatisticsComparer().Equals(expected, actual.First())
            );
        }

        [TestMethod]
        public void GetAsync_Works()
        {
            var expected = new StatisticsDao
            {
                AvgHourlyRatePerSession = 12.2m,
                HourlyRateStdDev = 4.65m,
                HourlyRateVariance = 70m,
                TotalHourlyRate = -12m,
                TotalHoursPlayed = 3.3m
            };
            RepoMock.DaoList.Add(expected);

            var actual = _service.GetAsync().Result;

            Assert.IsTrue(
                new StatisticsComparer().Equals(expected, actual)
            );
        }
    }
}
