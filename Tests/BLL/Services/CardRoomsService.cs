using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;
using System.Collections.Generic;
using PokerTracker.BLL.Objects;

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

        private static void AssertListEquals(CardRoomDao[] expected, Dictionary<Guid, CardRoom> actual)
        {
            Assert.AreEqual(expected.Length, actual.Count);

            foreach (var cardRoom in expected)
            {
                Assert.IsTrue(actual.ContainsKey(cardRoom.Id));
                Assert.AreEqual(cardRoom.Name, actual[cardRoom.Id].Name);
            }
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
