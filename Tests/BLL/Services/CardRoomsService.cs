using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using PokerTracker.BLL.Objects;
using PokerTracker.Tests.Comparers.Objects;

namespace PokerTracker.Tests.BLL.Services
{
    [TestClass]
    public class CardRoomsServiceTests
        : LookupServiceTests<ICardRoomRepository, CardRoomDao, CardRoomRepositoryMock>
    {
        private ICardRoomsService _service;

        [TestInitialize]
        public void SetUp()
        {
            Setup();
            _service = new CardRoomsService(Mapper, RepoMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var expected = new[] {
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Golden Nugget" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Caesar's" }
            };
            RepoMock.DaoList.AddRange(expected);

            var actual = _service.GetAllAsync().Result;
            AssertDaoToListWithId(expected, actual, new IdNameComparer<CardRoomDao,CardRoom>());
        }
    }
}
