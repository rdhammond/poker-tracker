using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
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
            TestFindAllAsync(
                new[]
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
                },
                (e, a) =>
                {
                    return e.BigBlind == a.BigBlind
                        && e.CardRoomId == a.CardRoomId
                        && e.EndTime == a.EndTime
                        && e.GameId == a.GameId
                        && e.PercentOfTimePlayed == a.PercentOfTimePlayed
                        && e.Id == a.Id
                        && e.Notes == a.Notes
                        && e.SmallBlind == a.SmallBlind
                        && e.StartTime == a.StartTime;
                }
            );
        }

        [TestMethod]
        public void InsertAsyncSingle_Works()
        {
            TestInsertAsync(new SessionDao
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
        public void InsertAsyncMultiple_Works()
        {
            TestInsertAsync(new[]
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
