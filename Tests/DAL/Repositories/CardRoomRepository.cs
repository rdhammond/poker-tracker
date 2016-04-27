using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class CardRoomRepositoryTests : RepositoryTests<CardRoomDao,CardRoomRepository>
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
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Atlantic City" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Golden Nugget" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Horseshoe Casino" }
            });
        }
    }
}
