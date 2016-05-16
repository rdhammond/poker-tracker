using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;
using PokerTracker.BLL.Objects;
using System.Collections.Generic;
using PokerTracker.Tests.Comparers.Objects;

namespace PokerTracker.Tests.BLL.Services
{
    [TestClass]
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
            var expected = new[] {
                new GameDao { Id = Guid.NewGuid(), Name = "Limit Hold 'Em" },
                new GameDao { Id = Guid.NewGuid(), Name = "Razz" }
            };
            RepoMock.DaoList.AddRange(expected);

            var actual = _service.GetAllAsync().Result;
            AssertDaoToListWithId(expected, actual, new IdNameComparer<GameDao, Game>());
        }
    }
}
