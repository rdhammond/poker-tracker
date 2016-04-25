using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class GameRepositoryTests : RepositoryTests<CardRoomDao>
    {
        [TestCleanup]
        public void TearDown()
        {
            DaoList.Clear();
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            DaoList.AddRange(new[]
            {
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Five Card Stud" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Texas Hold 'Em" },
            });

            var repository = new CardRoomRepository(DbFactMock.Object);
            var actual = repository.FindAllAsync().Result;
            Assert.IsNotNull(actual);
            Assert.AreSame(DaoList, actual);
        }
    }
}
