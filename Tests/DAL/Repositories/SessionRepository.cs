using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.Comparers.Dao;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class SessionRepositoryTests : RepositoryTests<SessionRepository,SessionDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new SessionRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAll_Works()
        {
            var entities = new[]
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
            };
            DatabaseMock.DaoList.AddRange(entities);

            var actual = Repo.FindAllAsync().Result;
            AssertListWithId(DatabaseMock.DaoList, actual, new SessionComparer());
        }

        [TestMethod]
        public void InsertAsyncSingle_Works()
        {
            var entity = new SessionDao
            {
                Id = Guid.NewGuid(),
                BigBlind = 4,
                CardRoomId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                SmallBlind = 2,
                StartTime = DateTime.Now.AddDays(-2)
            };

            TestInsertAsync(entity, new SessionComparer());
        }

        [TestMethod]
        public void InsertAsyncMultiple_Works()
        {
            var entities = new[]
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
            };

            TestInsertAsync(entities, new SessionComparer());
        }
    }
}
