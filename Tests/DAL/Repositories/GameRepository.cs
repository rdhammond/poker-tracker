using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.Comparers.Dao;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class GameRepositoryTests
        : ReadOnlyRepositoryTests<GameRepository, GameDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new GameRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var entities = new[]
            {
                new GameDao { Id = Guid.NewGuid(), Name = "Limit Hold 'Em" },
                new GameDao { Id = Guid.NewGuid(), Name = "No Limit Hold 'Em" }
            };
            DatabaseMock.DaoList.AddRange(entities);

            var actual = Repo.FindAllAsync().Result;
            AssertListWithId(DatabaseMock.DaoList, actual, new IdNameComparer<GameDao>());
        }
    }
}
