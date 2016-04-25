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
    public class SessionRepositoryTests : RepositoryTests<SessionDao,SessionRepository>
    {
        [TestCleanup]
        public void TearDown()
        {
            DaoList.Clear();
        }

        [TestMethod]
        public void FindAll_Works()
        {
            TestFindAllAsync(new[]
            {
                new SessionDao
                {
                    Id = Guid.NewGuid(),
                    BigBlind = 3,
                    CardRoomId = Guid.NewGuid(),
                    GameId = Guid.NewGuid(),
                    SmallBlind = 1,
                    StartTime = DateTime.Now
                },
                new SessionDao
                {
                    Id = Guid.NewGuid(),
                    BigBlind = 3,
                    CardRoomId = Guid.NewGuid(),
                    GameId = Guid.NewGuid(),
                    SmallBlind = 1,
                    StartTime = DateTime.Now
                }
            });
        }

        [TestMethod]
        public void SaveAsyncSingle_Works()
        {
            TestSaveAsync(new SessionDao
            {
                Id = Guid.NewGuid(),
                BigBlind = 4,
                CardRoomId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                SmallBlind = 2,
                StartTime = DateTime.Now.AddDays(-2)
            });
        }

        [TestMethod]
        public void SaveAsyncMultiple_Works()
        {
            TestSaveAsync(new[]
            {
                new SessionDao
                {
                    Id = Guid.NewGuid(),
                    BigBlind = 20,
                    CardRoomId = Guid.NewGuid(),
                    GameId = Guid.NewGuid(),
                    SmallBlind = 10,
                    StartTime = DateTime.Now.AddHours(-2)
                },
                new SessionDao
                {
                    Id = Guid.NewGuid(),
                    BigBlind = 2,
                    CardRoomId = Guid.NewGuid(),
                    GameId = Guid.NewGuid(),
                    SmallBlind = 1,
                    StartTime = DateTime.Now
                }
            });
        }
    }
}
