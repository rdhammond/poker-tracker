using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;

namespace PokerTracker.Tests.BLL.Services
{
    using CardRoomRepositoryMock = ReadOnlyRepositoryMock<CardRoomRepository, CardRoomDao>;

    public class CardRoomsServiceTests
    {
        private readonly CardRoomRepositoryMock RepoMock = new CardRoomRepositoryMock();

        private ICardRoomsService Service;

        [TestInitialize]
        public void SetUp()
        {
            Service = new CardRoomsService(GlobalMapper.Mapper, RepoMock.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            RepoMock.DaoList.Clear();
            Service = null;
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            RepoMock.DaoList.AddRange(new[] {
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Golden Nugget" },
                new CardRoomDao { Id = Guid.NewGuid(), Name = "Caesar's" }
            });

            var actual = Service.GetAllAsync().Result;
            Assert.AreSame(RepoMock.DaoList, actual);
        }
    }
}
