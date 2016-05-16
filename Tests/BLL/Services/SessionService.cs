using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.Tests.BLL.Mocks;
using System;
using System.Linq;
using PokerTracker.BLL.Objects;
using AutoMapper;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.Tests.Comparers.Objects;

namespace PokerTracker.Tests.BLL._sessionSvcs
{
    [TestClass]
    public class SessionServiceTests
    {
        private IMapper _mapper;
        private DatabaseMock<SessionDao> _databaseMock;
        private SessionRepositoryMock _sessionRepoMock;
        private TimeEntryRepositoryMock _timeEntryRepoMock;
        private ISessionService _sessionSvc;

        [TestInitialize]
        public void SetUp()
        {
            _mapper = MapperFactory.Create();
            _databaseMock = new DatabaseMock<SessionDao>();
            _sessionRepoMock = new SessionRepositoryMock();
            _timeEntryRepoMock = new TimeEntryRepositoryMock();

            _sessionSvc = new SessionService(
                _mapper,
                _sessionRepoMock.Object,
                _timeEntryRepoMock.Object
            );
        }

        [TestMethod]
        public void SaveSessionAsync_NullSessionThrows()
        {
            AssertHelper.Throws(() => 
                _sessionSvc.SaveSessionAsync(null).Wait()
            );
        }

        private void AssertSessionSaved(Session expected)
        {
            Assert.IsTrue(_sessionRepoMock.DaoList.Count == 1);

            var actual = _sessionRepoMock.DaoList.First();
            Assert.IsNotNull(actual);
            Assert.IsTrue(new SessionComparer().Equals(expected, actual));
        }

        public void AssertTimeEntriesCorrect(Session expectedSession)
        {
            decimal? lastStackSize = null;

            var expectedTimeEntriesDict = expectedSession.TimeEntries.ToDictionary(x => x.Id);
            Assert.AreEqual(expectedTimeEntriesDict.Count, _timeEntryRepoMock.DaoList.Count);

            var timeEntryComparer = new TimeEntryComparer();

            foreach (var actual in _timeEntryRepoMock.DaoList.OrderBy(x => x.RecordedAt))
            {
                Assert.IsNotNull(actual);

                var expected = expectedTimeEntriesDict[actual.Id];
                Assert.IsNotNull(expected);
                Assert.AreEqual(expectedSession.Id, actual.SessionId);
                Assert.IsTrue(timeEntryComparer.Equals(expected, actual));

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

        [TestMethod]
        public void SaveSessionAsync_NoEntriesWorks()
        {
            const string NOTES = "Test Notes";
            const int PERCENT_ACTIVE = 40;

            var expectedSession = new Session
            {
                BigBlind = 1,
                CardRoomId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                SmallBlind = 2,
                StartTime = DateTime.Now.AddHours(-1),
                EndTime = DateTime.Now,
                PercentOfTimePlayed = PERCENT_ACTIVE,
                Notes = NOTES
            };

            _sessionSvc
                .SaveSessionAsync(expectedSession)
                .Wait();

            AssertSessionSaved(expectedSession);
            Assert.IsTrue(!_timeEntryRepoMock.DaoList.Any());
        }

        [TestMethod]
        public void SaveSessionAsync_OneEntryWorks()
        {
            const int PERCENT_ACTIVE = 100;

            var expectedSession = new Session
            {
                BigBlind = 2,
                CardRoomId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                SmallBlind = 4,
                StartTime = DateTime.Now.AddHours(-2),
                EndTime = DateTime.Now,
                PercentOfTimePlayed = PERCENT_ACTIVE
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

            _sessionSvc
                .SaveSessionAsync(expectedSession)
                .Wait();

            AssertSessionSaved(expectedSession);
            AssertTimeEntriesCorrect(expectedSession);
        }

        [TestMethod]
        public void SaveSessionAsync_ThreeOrMoreEntriesWork()
        {
            const int PERCENT_ACTIVE = 75;

            var expectedSession = new Session
            {
                BigBlind = 20,
                CardRoomId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                SmallBlind = 40,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(-1).AddHours(2),
                PercentOfTimePlayed = PERCENT_ACTIVE
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

            _sessionSvc
                .SaveSessionAsync(expectedSession)
                .Wait();

            AssertSessionSaved(expectedSession);
            AssertTimeEntriesCorrect(expectedSession);
        }
    }
}