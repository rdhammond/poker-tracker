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
    public class TimeEntryRepositoryTests : RepositoryTests<TimeEntryDao,TimeEntryRepository>
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
            });
        }

        [TestMethod]
        public void SaveAsyncSingle_Works()
        {
            TestSaveAsync(new TimeEntryDao
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
        public void SaveAsyncMultiple_Works()
        {
            TestSaveAsync(new[]
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
