using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.Factories;
using System.Configuration;
using Moq;

namespace PokerTracker.Tests.DAL.Factories
{
    [TestClass]
    public class DatabaseFactoryTests
    {
        private IDatabaseFactory DbFact;

        [TestInitialize]
        public void SetUp()
        {
            DbFact = new DatabaseFactory();
        }

        [TestMethod]
        public void Create_Works()
        {
            Assert.IsNotNull(DbFact.Create());
        }
    }
}
