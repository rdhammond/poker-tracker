using PokerTracker.DAL.DAO;
using PokerTracker.Tests.BLL.Mocks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.Repositories;
using System.Linq;
using PokerTracker.BLL.Objects;
using System.Collections.Generic;

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

        private static void AssertListEqual(GameDao[] expected, Dictionary<Guid, Game> actual)
        {
            Assert.AreEqual(expected.Length, actual.Count);

            foreach (var game in expected)
            {
                Assert.IsTrue(actual.ContainsKey(game.Id));
                Assert.AreEqual(game.Name, actual[game.Id].Name);
            }
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
        }
    }
}
