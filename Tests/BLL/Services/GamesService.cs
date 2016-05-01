using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;

namespace PokerTracker.Tests.BLL.Services
{
    public class GamesServiceTests
        : LookupServiceTests<IGameRepository, GameDao, GameRepositoryMock>
    {
        private IGamesService _service;

        [TestInitialize]
        public void SetUp()
        {
            Setup();
            _service = new GamesService(Mapper, RepoMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            var daos = new[] {
                new GameDao { Id = Guid.NewGuid(), Name = "Limit Hold 'Em" },
                new GameDao { Id = Guid.NewGuid(), Name = "Razz" }
            };

            DaoList.AddRange(daos);

            var objects = _service.GetAllAsync().Result
                .ToDictionary(x => x.Id);

            AssertListEquals(daos, objects);
        }
    }
}
