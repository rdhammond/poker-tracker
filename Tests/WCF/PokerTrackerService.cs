using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Objects;
using PokerTracker.Tests.Comparers.Objects;
using PokerTracker.Tests.WCF.Mocks;
using PokerTracker.WCF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.WCF
{
    [TestClass]
    public class PokerTrackerServiceTests
    {
        private CardRoomsServiceMock CardRoomsSvcMock;
        private GamesServiceMock GamesSvcMock;
        private SessionServiceMock SessionSvcMock;
        private SummaryServiceMock SummarySvcMock;
        private StatisticsServiceMock StatisticsSvcMock;
        private IPokerTrackerService Service;

        [TestInitialize]
        public void SetUp()
        {
            CardRoomsSvcMock = new CardRoomsServiceMock();
            GamesSvcMock = new GamesServiceMock();
            SessionSvcMock = new SessionServiceMock();
            SummarySvcMock = new SummaryServiceMock();
            StatisticsSvcMock = new StatisticsServiceMock();

            Service = new PokerTrackerService(
                CardRoomsSvcMock.Object,
                GamesSvcMock.Object,
                SessionSvcMock.Object,
                SummarySvcMock.Object,
                StatisticsSvcMock.Object
            );
        }

        #region AssertLookupDictionary

        private static void AssertLookupDictionary<TExpected>(
            IEnumerable<TExpected> expected,
            Dictionary<Guid, string> actual
        )
            where TExpected : IdNameObject
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count);

            foreach (var expectedItem in expected)
            {
                var actualItem = actual[expectedItem.Id];
                Assert.IsNotNull(actualItem);
                Assert.AreEqual(expectedItem.Name, actualItem);
            }
        }

        private static void AssertLookupDictionary<TExpected>(
            IEnumerable<TExpected> expected,
            Dictionary<Guid,TExpected> actual,
            EqualityComparer<TExpected> comparer
        )
            where TExpected : IdObject
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count);

            foreach (var expectedItem in expected)
            {
                var actualItem = actual[expectedItem.Id];
                Assert.IsNotNull(actualItem);
                Assert.IsTrue(comparer.Equals(expectedItem, actualItem));
            }
        }

        #endregion

        [TestMethod]
        public void GetCardRoomsAsync_Works()
        {
            var expected = new[]
            {
                new CardRoom { Id = Guid.NewGuid(), Name = "Golden Nugget" },
                new CardRoom { Id = Guid.NewGuid(), Name = "Poker Genius" }
            };
            CardRoomsSvcMock.List.AddRange(expected);

            var actual = Service.GetCardRoomsAsync().Result;
            AssertLookupDictionary(expected, actual);
        }

        [TestMethod]
        public void GetGameTypesAsync_Works()
        {
            var expected = new[]
            {
                new Game { Id = Guid.NewGuid(), Name = "Limit Hold 'Em" },
                new Game { Id = Guid.NewGuid(), Name = "Omaha/8" }
            };
            GamesSvcMock.List.AddRange(expected);

            var actual = Service.GetGameTypesAsync().Result;
            AssertLookupDictionary(expected, actual);
        }

        [TestMethod]
        public void SaveSessionAsync_Works()
        {
            Service
                .SaveSessionAsync(new Session())
                .Wait();

            SessionSvcMock.VerifyAll();
        }

        [TestMethod]
        public void GetSessionSummariesAsync_Works()
        {
            var expected = new[]
            {
                new Summary
                {
                    Cardroom = "Test Cardroom",
                    DayOfMonth = 22,
                    DayOfWeek = "M",
                    EndTime = DateTime.Now,
                    Game = "Razz",
                    HourlyRate = 140,
                    HourlyRateBB = 22.1m,
                    HoursPlayed = 4,
                    Limit = "$20/$40",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddHours(-2),
                    WinLoss = 400,
                    WinLossBB = 20
                },
                new Summary
                {
                    Cardroom = "Las Vegas Somewhere",
                    DayOfMonth = 12,
                    DayOfWeek = "U",
                    EndTime = DateTime.Now.AddDays(-1).AddHours(4),
                    Game = "Omaha PLO",
                    HourlyRate = 20,
                    HourlyRateBB = 1.2m,
                    HoursPlayed = 1,
                    Limit = "$1/$2",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    WinLoss = 20,
                    WinLossBB = 4.2m
                }
            };
            SummarySvcMock.List.AddRange(expected);

            var actual = Service.GetSessionSummariesAsync().Result;

            AssertLookupDictionary(expected, actual, new SummaryComparer());
        }

        [TestMethod]
        public void GetStatisticsAsync_Works()
        {
            var expected = new Statistics
            {
                AvgHourlyRatePerSession = 21.1m,
                HourlyRateStdDev = 14m,
                HourlyRateVariance = 11m,
                TotalHourlyRate = -44.14m,
                TotalHoursPlayed = 22.1m
            };
            StatisticsSvcMock.List.Add(expected);

            StatisticsSvcMock.BiggestUpswing = new Swing
            {
                BiggestSwing = 149,
                StartTime = DateTime.Now.AddMinutes(-1),
                EndTime = DateTime.Now
            };

            StatisticsSvcMock.BiggestDownswing = new Swing
            {
                BiggestSwing = -144,
                StartTime = DateTime.Now.AddYears(-1),
                EndTime = DateTime.Now.AddMonths(-6)
            };

            var actual = Service.GetStatisticsAsync().Result;

            Assert.IsTrue(
                new StatisticsComparer().Equals(expected, actual)
                && new SwingComparer().Equals(StatisticsSvcMock.BiggestUpswing, actual.BiggestUpswing)
                && new SwingComparer().Equals(StatisticsSvcMock.BiggestDownswing, actual.BiggestDownswing)
            );
        }

        [TestMethod]
        public void GetStatisticsAsync_NullSwings_Works()
        {
            var expected = new Statistics
            {
                AvgHourlyRatePerSession = 21.1m,
                HourlyRateStdDev = 14m,
                HourlyRateVariance = 11m,
                TotalHourlyRate = -44.14m,
                TotalHoursPlayed = 22.1m
            };
            StatisticsSvcMock.List.Add(expected);

            var actual = Service.GetStatisticsAsync().Result;

            Assert.IsTrue(new StatisticsComparer().Equals(expected, actual));
            Assert.IsNull(actual.BiggestUpswing);
            Assert.IsNull(actual.BiggestDownswing);
        }
    }
}
