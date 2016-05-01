using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;

namespace PokerTracker.Tests.BLL.Services
{
    public class SummaryServiceTests
        : LookupServiceTests<ISummaryRepository,SummaryDao,SummaryRepositoryMock>
    {
        private TotalHourlyRateRepositoryMock _totalHourlyRateRepoMock;
        private ISummaryService _summarySvc;

        [TestInitialize]
        public void SetUp()
        {
            Setup();
            _totalHourlyRateRepoMock = new TotalHourlyRateRepositoryMock();

            _summarySvc = new SummaryService(
                Mapper,
                RepoMock.Object,
                _totalHourlyRateRepoMock.Object
            );
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var daos = new[] {
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
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    WinLoss = -280,
                    WinLossBB = -20
                }
            };
            DaoList.AddRange(daos);

            var objects = _summarySvc.GetAllAsync().Result
                .ToDictionary(x => x.Id);

            AssertListEquals(daos, objects, (e, a) =>
            {
                return e.Cardroom == a.Cardroom
                    && e.DayOfMonth == a.DayOfMonth
                    && e.DayOfWeek == a.DayOfWeek
                    && e.EndTime == a.EndTime
                    && e.Game == a.Game
                    && e.HourlyRate == a.HourlyRate
                    && e.HourlyRateBB == a.HourlyRateBB
                    && e.HoursPlayed == a.HoursPlayed
                    && e.Id == a.Id
                    && e.Limit == a.Limit
                    && e.StartTime == a.StartTime
                    && e.WinLoss == a.WinLoss
                    && e.WinLossBB == a.WinLossBB;
            });
        }

        [TestMethod]
        public void GetTotalHourlyRate_Works()
        {
            const decimal TEST_HOURLY_RATE = 14.1m;

            _totalHourlyRateRepoMock.DaoList.Add(new TotalHourlyRateDao
            {
                TotalHourlyRate = TEST_HOURLY_RATE
            });

            var actual = _summarySvc.GetTotalHourlyRateAsync().Result;
            Assert.AreEqual(TEST_HOURLY_RATE, actual);
        }
    }
}
