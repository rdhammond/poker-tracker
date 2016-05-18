using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.Comparers.Dao;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Repositories
{
    using SwingComparer = Comparers.Dao.SwingComparer;

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

        [TestMethod]
        public void GetBiggestDownswingAsync_Works()
        {
            AssertBiggestSwing_Works(Repo.GetBiggestDownswingAsync);
        }

        [TestMethod]
        public void GetBiggestDownswingAsync_ReturnsNullIfZero()
        {
            AssertBiggestSwing_ReturnsNullIfZero(Repo.GetBiggestDownswingAsync);
        }

        [TestMethod]
        public void GetBiggestUpswingAsync_Works()
        {
            AssertBiggestSwing_Works(Repo.GetBiggestUpswingAsync);
        }

        [TestMethod]
        public void GetBiggestUpswingAsync_ReturnsNullIfZero()
        {
            AssertBiggestSwing_ReturnsNullIfZero(Repo.GetBiggestUpswingAsync);
        }

        private void AssertBiggestSwing_Works(Func<Task<SwingDao>> func)
        {
            var expected = new SwingDao
            {
                BiggestSwing = 100,
                StartTime = DateTime.Now.AddMonths(-1),
                EndTime = DateTime.Now.AddMonths(-1).AddHours(2)
            };
            DatabaseMock.Swing = expected;

            var actual = func().Result;

            Assert.IsTrue(
                new SwingComparer().Equals(expected, actual)
            );
        }

        public void AssertBiggestSwing_ReturnsNullIfZero(Func<Task<SwingDao>> func)
        {
            var zeroSwing = new SwingDao
            {
                BiggestSwing = 0,
                StartTime = DateTime.Now.AddMonths(-1),
                EndTime = DateTime.Now.AddMonths(-1).AddHours(2)
            };
            DatabaseMock.Swing = zeroSwing;

            Assert.IsNull(func().Result);
        }
    }
}
