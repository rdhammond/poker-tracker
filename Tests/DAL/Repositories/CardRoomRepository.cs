using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.DAL.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class CardRoomRepositoryTests
    {
        private readonly List<CardRoomDao> DaoList = new List<CardRoomDao>();
        private readonly DatabaseFactoryMock DbFactMock = new DatabaseFactoryMock();
        private readonly DatabaseWrapperMock DbWrapperMock = new DatabaseWrapperMock();

        public CardRoomRepositoryTests()
        {
            DbWrapperMock.AddList(DaoList);
            DbFactMock.Database = DbWrapperMock.Object;
        }

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
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Test Room" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Another Test" }
            });

            var repository = new CardRoomRepository(DbFactMock.Object);
            var actual = repository.FindAllAsync().Result;
            Assert.IsNotNull(actual);
            Assert.AreSame(DaoList, actual);
        }
    }
}
