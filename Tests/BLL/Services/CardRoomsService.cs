using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;

namespace PokerTracker.Tests.BLL.Services
{
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
            var daos = new[] {
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Golden Nugget" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Caesar's" }
            };
            DaoList.AddRange(daos);

            var objects = _service.GetAllAsync().Result
                .ToDictionary(x => x.Id);

            AssertListEquals(daos, objects);
        }
    }
}
