using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Objects;
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
        private IPokerTrackerService Service;

        [TestInitialize]
        public void SetUp()
        {
            CardRoomsSvcMock = new CardRoomsServiceMock();
            GamesSvcMock = new GamesServiceMock();
            SessionSvcMock = new SessionServiceMock();
            SummarySvcMock = new SummaryServiceMock();

            Service = new PokerTrackerService(
                CardRoomsSvcMock.Object,
                GamesSvcMock.Object,
                SessionSvcMock.Object,
                SummarySvcMock.Object
            );
        }

        #region AssertLookupDictionary

        private void AssertLookupDictionary<TExpected>(
            IEnumerable<TExpected> expected,
            Dictionary<Guid, string> actual
        )
            where TExpected : IdNameObject
        {
            AssertLookupDictionary(
                expected,
                actual,
                (e, a) => Assert.AreEqual(e.Name, a)
            );
        }

        private void AssertLookupDictionary<TExpected, TValue>(
            IEnumerable<TExpected> expected,
            Dictionary<Guid, TValue> actual,
            Action<TExpected, TValue> assertEqual
        )
            where TExpected : IdObject
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count);

            foreach (var expectedItem in expected)
            {
                var actualItem = actual[expectedItem.Id];
                Assert.IsNotNull(actualItem);
                assertEqual(expectedItem, actualItem);
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
        public void CreateSession_Works()
        {
            var session = Service.CreateSession();
            Assert.IsInstanceOfType(session, typeof(Session));
            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void SaveSessionAsync_Works()
        {
            Service
                .SaveSessionAsync(new Session(), DateTime.Now, 4, "Test Note")
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

            AssertLookupDictionary(expected, actual, (e, a) =>
            {
                Assert.AreEqual(e.Cardroom, a.Cardroom);
                Assert.AreEqual(e.DayOfMonth, a.DayOfMonth);
                Assert.AreEqual(e.DayOfWeek, a.DayOfWeek);
                Assert.AreEqual(e.EndTime, a.EndTime);
                Assert.AreEqual(e.Game, a.Game);
                Assert.AreEqual(e.HourlyRate, a.HourlyRate);
                Assert.AreEqual(e.HourlyRateBB, a.HourlyRateBB);
                Assert.AreEqual(e.HoursPlayed, a.HoursPlayed);
                Assert.AreEqual(e.Limit, a.Limit);
                Assert.AreEqual(e.Id, a.Id);
                Assert.AreEqual(e.StartTime, a.StartTime);
                Assert.AreEqual(e.WinLoss, a.WinLoss);
                Assert.AreEqual(e.WinLossBB, a.WinLossBB);
            });
        }

        [TestMethod]
        public void GetTotalHourlyRate_Works()
        {
            SummarySvcMock.TotalHourlyRate = 14.1m;

            var actual = Service.GetTotalHourlyRateAsync().Result;
            Assert.AreEqual(SummarySvcMock.TotalHourlyRate, actual);
        }
    }
}
