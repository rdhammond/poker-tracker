using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.Comparers.Dao;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class CardRoomRepositoryTests 
        : ReadOnlyRepositoryTests<CardRoomRepository, CardRoomDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new CardRoomRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var entities = new[]
            {
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Atlantic City" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Golden Nugget" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Horseshoe Casino" }
            };
            DatabaseMock.DaoList.AddRange(entities);

            var actual = Repo.FindAllAsync().Result;
            AssertListWithId(DatabaseMock.DaoList, actual, new IdNameComparer<CardRoomDao>());
        }
    }
}
