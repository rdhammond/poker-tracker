using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class CardRoomRepositoryTests 
        : ReadOnlyRepositoryTests<CardRoomRepository, CardRoomDao>
    {
        [TestInitialize]
        public void SetUp()
        {
            Setup();
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
