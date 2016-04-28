﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using System;
using System.Linq;
using PokerTracker.BLL.Objects;

namespace PokerTracker.Tests.BLL.Services
{
    using SessionRepositoryMock = RepositoryMock<ISessionRepository, SessionDao>;
    using TimeEntryRepositoryMock = RepositoryMock<ITimeEntryRepository, TimeEntryDao>;

    [TestClass]
    public class SessionServiceTests
    {
        private DatabaseFactoryMock DbFactMock;
        private DatabaseWrapperMock DbMock;
        private SessionRepositoryMock SessionRepoMock;
        private TimeEntryRepositoryMock TimeEntryRepoMock;
        private SessionService Service;

        [TestInitialize]
        public void SetUp()
        {
            DbFactMock = new DatabaseFactoryMock();
            DbMock = DbFactMock.DatabaseWrapperMock;
            SessionRepoMock = new SessionRepositoryMock();
            TimeEntryRepoMock = new TimeEntryRepositoryMock();

            Service = new SessionService(
                GlobalMapper.Mapper, DbFactMock.Object, SessionRepoMock.Object,
                TimeEntryRepoMock.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            Service = null;
            TimeEntryRepoMock = null;
            SessionRepoMock = null;
            DbFactMock = null;
        }

        [TestMethod]
        public void SaveSessionAsync_NullSessionThrows()
        {
            AssertHelper.Throws(() => 
                Service.SaveSessionAsync(null, DateTime.Now, 1).Wait());
        }

        private void AssertSessionSaved(Session expected, DateTime endTime,
            decimal hoursActive, string notes)
        {
            Assert.IsTrue(SessionRepoMock.DaoList.Count == 1);

            var actual = SessionRepoMock.DaoList.First();
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.BigBlind, actual.BigBlind);
            Assert.AreEqual(expected.CardRoom.Id, actual.CardRoomId);
            Assert.AreEqual(expected.Game.Id, actual.GameId);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.SmallBlind, actual.SmallBlind);
            Assert.AreEqual(expected.StartTime, actual.StartTime);
            Assert.AreEqual(endTime, actual.EndTime);
            Assert.AreEqual(hoursActive, actual.HoursActive);
            Assert.AreEqual(notes, actual.Notes);
        }

        public void AssertTimeEntriesCorrect(Session expectedSession)
        {
            decimal? lastStackSize = null;

            var expectedTimeEntriesDict = expectedSession.TimeEntries.ToDictionary(x => x.Id);
            Assert.AreEqual(expectedTimeEntriesDict.Count, TimeEntryRepoMock.DaoList.Count);

            foreach (var actual in TimeEntryRepoMock.DaoList.OrderBy(x => x.RecordedAt))
            {
                Assert.IsNotNull(actual);

                var expected = expectedTimeEntriesDict[actual.Id];
                Assert.IsNotNull(expected);
                Assert.AreEqual(expectedSession.Id, actual.SessionId);

                Assert.AreEqual(expected.DealerTokes, actual.DealerTokes);
                Assert.AreEqual(expected.RecordedAt, actual.RecordedAt);
                Assert.AreEqual(expected.ServerTips, actual.ServerTips);
                Assert.AreEqual(expected.StackSize, actual.StackSize);

                if (!lastStackSize.HasValue)
                {
                    Assert.IsNull(actual.StackDifferential);
                    lastStackSize = expected.StackSize;
                    continue;
                }

                Assert.IsNotNull(actual.StackDifferential);
                Assert.AreEqual(expected.StackSize - lastStackSize, actual.StackDifferential);

                lastStackSize = expected.StackSize;
            }
        }

        public void AssertTransactionSuccessful()
        {
            Assert.IsTrue(DbMock.Transactions.Count == 1);
            Assert.IsTrue(DbMock.AllTransactionComplete);
            Assert.IsTrue(DbMock.AllTransactionsDisposed);
        }

        [TestMethod]
        public void SaveSessionAsync_NoEntriesWorks()
        {
            const string NOTES = "Test Notes";
            const decimal HOURS_ACTIVE = 2.1m;

            var expectedSession = new Session
            {
                BigBlind = 1,
                CardRoom = new CardRoom { Id = Guid.NewGuid() },
                Game = new Game { Id = Guid.NewGuid() },
                Id = Guid.NewGuid(),
                SmallBlind = 2,
                StartTime = DateTime.Now.AddHours(-1)
            };

            var expectedEndTime = DateTime.Now;
            Service.SaveSessionAsync(expectedSession, expectedEndTime, HOURS_ACTIVE, NOTES).Wait();

            AssertSessionSaved(expectedSession, expectedEndTime, HOURS_ACTIVE, NOTES);
            Assert.IsTrue(!TimeEntryRepoMock.DaoList.Any());
            AssertTransactionSuccessful();
        }

        [TestMethod]
        public void SaveSessionAsync_OneEntryWorks()
        {
            const decimal HOURS_ACTIVE = 1m;

            var expectedSession = new Session
            {
                BigBlind = 2,
                CardRoom = new CardRoom { Id = Guid.NewGuid() },
                Game = new Game { Id = Guid.NewGuid() },
                Id = Guid.NewGuid(),
                SmallBlind = 4,
                StartTime = DateTime.Now.AddHours(-2)
            };

            expectedSession.TimeEntries.AddRange(new[]
            {
                new TimeEntry
                {
                    DealerTokes = 2,
                    Id = Guid.NewGuid(),
                    RecordedAt = DateTime.Now.AddHours(-1).AddMinutes(-30),
                    ServerTips = 1,
                    StackSize = 194
                }
            });

            var expectedEndTime = DateTime.Now.AddHours(-1);
            Service.SaveSessionAsync(expectedSession, expectedEndTime, HOURS_ACTIVE).Wait();

            AssertSessionSaved(expectedSession, expectedEndTime, HOURS_ACTIVE, null);
            AssertTimeEntriesCorrect(expectedSession);
            AssertTransactionSuccessful();
        }

        [TestMethod]
        public void SaveSessionAsync_ThreeOrMoreEntriesWork()
        {
            const decimal HOURS_ACTIVE = 4.11m;

            var expectedSession = new Session
            {
                BigBlind = 20,
                CardRoom = new CardRoom { Id = Guid.NewGuid() },
                Game = new Game { Id = Guid.NewGuid() },
                Id = Guid.NewGuid(),
                SmallBlind = 40,
                StartTime = DateTime.Now.AddDays(-1)
            };

            expectedSession.TimeEntries.AddRange(new[]
            {
                new TimeEntry
                {
                    DealerTokes = 0,
                    Id = Guid.NewGuid(),
                    RecordedAt = DateTime.Now.AddDays(-1).AddHours(1),
                    ServerTips = 0,
                    StackSize = 180
                },
                new TimeEntry
                {
                    DealerTokes = 3,
                    Id = Guid.NewGuid(),
                    RecordedAt = DateTime.Now.AddDays(-1).AddHours(3),
                    ServerTips = 0,
                    StackSize = 150
                },
                new TimeEntry
                {
                    DealerTokes = 1,
                    Id = Guid.NewGuid(),
                    RecordedAt = DateTime.Now.AddDays(-1).AddHours(2),
                    ServerTips = 1,
                    StackSize = 194
                }
            });

            var expectedEndTime = DateTime.Now.AddHours(-1);
            Service.SaveSessionAsync(expectedSession, expectedEndTime, HOURS_ACTIVE).Wait();

            AssertSessionSaved(expectedSession, expectedEndTime, HOURS_ACTIVE, null);
            AssertTimeEntriesCorrect(expectedSession);
            AssertTransactionSuccessful();
        }
    }
}
