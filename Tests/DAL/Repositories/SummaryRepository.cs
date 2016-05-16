using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.Comparers.Dao;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class SummaryRepositoryTests
        : ReadOnlyRepositoryTests<SummaryRepository, SummaryDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new SummaryRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var entities = new[]
            {
                new SummaryDao
                {
                    Cardroom = "Horseshoe Casino",
                    DayOfMonth = 4,
                    EndTime = DateTime.Now,
                    Game = "No Limit Hold 'Em",
                    HourlyRate = 24,
                    HourlyRateBB = 1.2m,
                    HoursPlayed = 1,
                    Limit = "$20/40",
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddHours(-1),
                    WinLoss = 24,
                    WinLossBB = 1.2m
                },
                new SummaryDao
                {
                    Cardroom = "Golden Nugget",
                    DayOfMonth = 12,
                    EndTime = DateTime.Now.AddDays(-1),
                    Game = "Limit Hold 'Em",
                    HourlyRate = 40,
                    HourlyRateBB = 2m,
                    HoursPlayed = 12,
                    Limit = "$3/$6",
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1).AddHours(-2),
                    WinLoss = 160,
                    WinLossBB = 50.33m
                }
            };
            DatabaseMock.DaoList.AddRange(entities);

            var actual = Repo.FindAllAsync().Result;
            AssertListWithId(x => x.SessionId, DatabaseMock.DaoList, actual, new SummaryComparer());
        }
    }
}
