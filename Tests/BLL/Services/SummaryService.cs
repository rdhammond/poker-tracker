using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;

namespace PokerTracker.Tests.BLL.Services
{
    using SummaryRepositoryMock = ReadOnlyRepositoryMock<SummaryRepository, SummaryDao>;

    using TotalHourlyRateRepositoryMock = ReadOnlyRepositoryMock<
        TotalHourlyRateRepository, TotalHourlyRateDao>;

    public class SummaryServiceTests
    {
        private readonly SummaryRepositoryMock SummaryRepoMock = new SummaryRepositoryMock();

        private readonly TotalHourlyRateRepositoryMock TotalHourlyRateRepoMock =
            new TotalHourlyRateRepositoryMock();

        private ISummaryService Service;

        public object TotalHourlySummaryRepoMock { get; private set; }

        [TestInitialize]
        public void SetUp()
        {
            Service = new SummaryService(
                GlobalMapper.Mapper, SummaryRepoMock.Object, TotalHourlyRateRepoMock.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            SummaryRepoMock.DaoList.Clear();
            Service = null;
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            SummaryRepoMock.DaoList.AddRange(new[] {
                new SummaryDao
                {
                    Cardroom = "Beltera",
                    DayOfMonth = 14,
                    EndTime = DateTime.Now.AddHours(-2),
                    Game = "No Limit Hold 'Em",
                    HourlyRate = 14,
                    HourlyRateBB = 4.1m,
                    HoursPlayed = 1.4m,
                    Limit = "$1/$2",
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddHours(-8),
                    WinLoss = 140,
                    WinLossBB = 70
                },
                new SummaryDao
                {
                    Cardroom = "Private Room",
                    DayOfMonth = 2,
                    EndTime = DateTime.Now.AddDays(-1).AddHours(2),
                    Game = "Seven Card Stud",
                    HourlyRate = -14,
                    HourlyRateBB = -1.5m,
                    HoursPlayed = 3m,
                    Limit = "$2/$4",
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    WinLoss = -280,
                    WinLossBB = -20
                }
            });

            var actual = Service.GetAllAsync().Result;
            Assert.AreSame(SummaryRepoMock.DaoList, actual);
        }

        [TestMethod]
        public void GetTotalHourlyRate_Works()
        {
            const decimal TEST_HOURLY_RATE = 14.1m;

            TotalHourlyRateRepoMock.DaoList.Add(new TotalHourlyRateDao
            {
                TotalHourlyRate = TEST_HOURLY_RATE
            });

            var actual = Service.GetTotalHourlyRateAsync().Result;
            Assert.AreEqual(TEST_HOURLY_RATE, actual);
        }
    }
}
