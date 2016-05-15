﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class GameRepositoryTests
        : ReadOnlyRepositoryTests<GameRepository, GameDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new GameRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            TestFindAllAsync(new[]
            {
                new GameDao { Id = Guid.NewGuid(), Name = "Limit Hold 'Em" },
                new GameDao { Id = Guid.NewGuid(), Name = "No Limit Hold 'Em" }
            });
        }
    }
}
