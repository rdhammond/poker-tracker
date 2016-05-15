using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class TimeEntryRepositoryTests : RepositoryTests<TimeEntryRepository, TimeEntryDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new TimeEntryRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            TestFindAllAsync(
                new[]
                {
                    new TimeEntryDao
                    {
                        DealerTokes = 2,
                        Id = Guid.NewGuid(),
                        RecordedAt = DateTime.Now,
                        ServerTips = 1,
                        SessionId = Guid.NewGuid(),
                        StackDifferential = -4,
                        StackSize = 12
                    },
                    new TimeEntryDao
                    {
                        DealerTokes = 0,
                        Id = Guid.NewGuid(),
                        RecordedAt = DateTime.Now.AddDays(-2),
                        ServerTips = 1,
                        SessionId = Guid.NewGuid(),
                        StackDifferential = 40,
                        StackSize = 140
                    }
                },
                (e,a) =>
                {
                    return e.DealerTokes == a.DealerTokes
                        && e.Id == a.Id
                        && e.RecordedAt == a.RecordedAt
                        && e.ServerTips == a.ServerTips
                        && e.SessionId == a.SessionId
                        && e.StackDifferential == a.StackDifferential
                        && e.StackSize == a.StackSize;
                }
            );
        }

        [TestMethod]
        public void InsertAsyncSingle_Works()
        {
            TestInsertAsync(new TimeEntryDao
            {
                DealerTokes = 1,
                Id = Guid.NewGuid(),
                RecordedAt = DateTime.Now.AddHours(-1),
                ServerTips = 4,
                SessionId = Guid.NewGuid(),
                StackDifferential = -14,
                StackSize = 200
            });
        }

        [TestMethod]
        public void InsertAsyncMultiple_Works()
        {
            TestInsertAsync(new[]
            {
                new TimeEntryDao
                {
                    DealerTokes = 1,
                    Id = Guid.NewGuid(),
                    RecordedAt = DateTime.Now.AddHours(-1),
                    ServerTips = 4,
                    SessionId = Guid.NewGuid(),
                    StackDifferential = -14,
                    StackSize = 200
                },
                new TimeEntryDao
                {
                    DealerTokes = 4,
                    Id = Guid.NewGuid(),
                    RecordedAt = DateTime.Now.AddHours(-10),
                    ServerTips = 3,
                    SessionId = Guid.NewGuid(),
                    StackDifferential = 12,
                    StackSize = 100
                }
            });
        }
    }
}
