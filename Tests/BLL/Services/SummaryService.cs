using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;
using PokerTracker.BLL.Objects;
using System.Collections.Generic;
using PokerTracker.Tests.Comparers.Objects;

namespace PokerTracker.Tests.BLL.Services
{
    [TestClass]
    public class SummaryServiceTests
        : LookupServiceTests<ISummaryRepository,SummaryDao,SummaryRepositoryMock>
    {
        private ISummaryService _summarySvc;

        [TestInitialize]
        public void SetUp()
        {
            Setup();
            _summarySvc = new SummaryService(Mapper, RepoMock.Object);
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
            };
            RepoMock.DaoList.AddRange(daos);

            var objects = _summarySvc.GetAllAsync().Result;
            AssertDaoToListWithId<SummaryDao,Summary>(x => x.SessionId, y => y.Id, daos, objects, new SummaryComparer());
        }
    }
}
