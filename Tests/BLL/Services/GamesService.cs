using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;

namespace PokerTracker.Tests.BLL.Services
{
    using GameRepositoryMock = ReadOnlyRepositoryMock<GameRepository, GameDao>;

    public class GamesServiceTests
    {
        private readonly GameRepositoryMock RepoMock = new GameRepositoryMock();

        private IGamesService Service;

        [TestInitialize]
        public void SetUp()
        {
            Service = new GamesService(GlobalMapper.Mapper, RepoMock.Object);
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
                new GameDao { Id = Guid.NewGuid(), Name = "Limit Hold 'Em" },
                new GameDao { Id = Guid.NewGuid(), Name = "Razz" }
            });

            var actual = Service.GetAllAsync().Result;
            Assert.AreSame(RepoMock.DaoList, actual);
        }
    }
}
