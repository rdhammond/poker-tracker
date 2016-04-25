using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class SummaryRepositoryTests : RepositoryTests<SummaryDao,SummaryRepository>
    {
        [TestCleanup]
        public void TearDown()
        {
            DaoList.Clear();
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            TestFindAllAsync(new[]
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
            });
        }

        [TestMethod]
        public void TestSaveAsyncSingle_Works()
        {
            TestSaveAsync(new SummaryDao
            {
                Cardroom = "Caesar's",
                DayOfMonth = 2,
                EndTime = DateTime.Now.AddHours(-1),
                Game = "Limit Hold 'Em",
                HourlyRate = 30,
                HourlyRateBB = 2.1m,
                HoursPlayed = 12,
                Limit = "$15/$30",
                SessionId = Guid.NewGuid(),
                StartTime = DateTime.Now.AddHours(-13),
                WinLoss = 150,
                WinLossBB = 10.33m
            });
        }

        [TestMethod]
        public void TestSaveAsyncMultiple_Works()
        {
            TestSaveAsync(new[] {
                new SummaryDao
                {
                    Cardroom = "Caesar's",
                    DayOfMonth = 2,
                    EndTime = DateTime.Now.AddHours(-1),
                    Game = "Limit Hold 'Em",
                    HourlyRate = 30,
                    HourlyRateBB = 2.1m,
                    HoursPlayed = 12,
                    Limit = "$15/$30",
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddHours(-13),
                    WinLoss = 150,
                    WinLossBB = 10.33m
                },
                new SummaryDao
                {
                    Cardroom = "Private Game",
                    DayOfMonth = 9,
                    EndTime = DateTime.Now.AddDays(-2),
                    Game = "Limit Hold 'Em",
                    HourlyRate = 4,
                    HourlyRateBB = 0.4m,
                    HoursPlayed = 4,
                    Limit = "$1/$2",
                    SessionId = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-2).AddHours(-4),
                    WinLoss = 10,
                    WinLossBB = 1.4m
                }
            });
        }
    }
}
