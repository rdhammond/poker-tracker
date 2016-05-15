using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class TotalHourlyRateRepositoryTests
        : ReadOnlyRepositoryTests<TotalHourlyRateRepository, TotalHourlyRateDao>
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            Repo = new TotalHourlyRateRepository(DatabaseMock.Object);
        }

        [TestMethod]
        public void GetTotalHourlyRateAsync_Works()
        {
            const decimal TEST_HOURLY_RATE = -14m;

            DaoList.Add(new TotalHourlyRateDao { TotalHourlyRate = TEST_HOURLY_RATE });

            var actual = Repo.GetTotalHourlyRateAsync().Result;
            Assert.AreEqual(TEST_HOURLY_RATE, actual);
        }
    }
}
