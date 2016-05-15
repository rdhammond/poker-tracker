using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;
using PokerTracker.BLL.Objects;
using System.Collections.Generic;

namespace PokerTracker.Tests.BLL.Services
{
    [TestClass]
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

        private static void AssertListEquals(SummaryDao[] expected, Dictionary<Guid,Summary> actual)
        {
            Assert.AreEqual(expected.Length, actual.Count);
            
            foreach (var summary in expected)
            {
                Assert.IsTrue(actual.ContainsKey(summary.Id));

                var value = actual[summary.Id];
                Assert.AreEqual(summary.Cardroom, value.Cardroom);
                Assert.AreEqual(summary.DayOfMonth, value.DayOfMonth);
                Assert.AreEqual(summary.DayOfWeek, value.DayOfWeek);
                Assert.AreEqual(summary.EndTime, value.EndTime);
                Assert.AreEqual(summary.Game, value.Game);
                Assert.AreEqual(summary.HourlyRate, value.HourlyRate);
                Assert.AreEqual(summary.HourlyRateBB, value.HourlyRateBB);
                Assert.AreEqual(summary.HoursPlayed, value.HoursPlayed);
                Assert.AreEqual(summary.Id, value.Id);
                Assert.AreEqual(summary.Limit, value.Limit);
                Assert.AreEqual(summary.StartTime, value.StartTime);
                Assert.AreEqual(summary.WinLoss, value.WinLoss);
                Assert.AreEqual(summary.WinLossBB, value.WinLossBB);
            }
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

            AssertListEquals(daos, objects);
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
