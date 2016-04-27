using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.Tests.DAL.Repositories
{
    [TestClass]
    public class TotalHourlyRateRepositoryTests : RepositoryTests<TotalHourlyRateDao, TotalHourlyRateRepository>
    {
        [TestCleanup]
        public void TearDown()
        {
            DaoList.Clear();
        }

        [TestMethod]
        public void FindAllAsync_Works()
        {
            TestFindAllAsync(
               new[] { new TotalHourlyRateDao { TotalHourlyRate = 41.2m } }
           );
        }

        [TestMethod]
        public void GetTotalHourlyRateAsync_Works()
        {
            const decimal TEST_HOURLY_RATE = -14m;

            DaoList.Add(new TotalHourlyRateDao { TotalHourlyRate = TEST_HOURLY_RATE });

            var repo = CreateRepository();
            var actual = repo.GetTotalHourlyRateAsync().Result;

            Assert.AreEqual(TEST_HOURLY_RATE, actual);
        }
    }
}
