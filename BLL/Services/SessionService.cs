using PokerTracker.BLL.Objects;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PokerTracker.DAL.DAO;
using System.Linq;

namespace PokerTracker.BLL.Services
{
    public interface ISessionService
    {
        Task SaveSessionAsync(
            Session session,
            DateTime endTime,
            decimal hoursPlayed,
            string optionalNotes = null
        );
    }

    public class SessionService : ISessionService
    {
        private readonly IMapper Mapper;
        private readonly IDatabaseFactory DbFactory;
        private readonly ISessionRepository SessionRepo;
        private readonly ITimeEntryRepository TimeEntryRepo;

        public SessionService(
            IMapper mapper,
            IDatabaseFactory dbFactory,
            ISessionRepository sessionRepo,
            ITimeEntryRepository timeEntryRepo
        )
        {
            Mapper = mapper;
            DbFactory = dbFactory;
            SessionRepo = sessionRepo;
            TimeEntryRepo = timeEntryRepo;
        }

        private IEnumerable<TimeEntryDao> FinalizeTimeEntries(Session session)
        {
            int? lastValue = null;

            foreach (var timeEntry in session.TimeEntries.OrderBy(x => x.RecordedAt))
            {
                var dao = Mapper.Map<TimeEntryDao>(timeEntry);
                dao.SessionId = session.Id;

                if (!lastValue.HasValue)
                {
                    lastValue = dao.StackSize;
                    yield return dao;
                    continue;
                }

                dao.StackDifferential = dao.StackSize - lastValue.Value;
                lastValue = dao.StackSize;
                yield return dao;
            }
        }

        public async Task SaveSessionAsync(Session session, DateTime endTime, decimal hoursActive, string optionalNotes = null)
        {
            using (var database = DbFactory.Create())
            using (var transaction = await database.GetTransactionAsync())
            {
                var sessionDao = Mapper.Map<SessionDao>(session);
                sessionDao.EndTime = endTime;
                sessionDao.HoursActive = hoursActive;
                sessionDao.Notes = optionalNotes;
                await SessionRepo.SaveAsync(sessionDao, database);

                await TimeEntryRepo.SaveAsync(FinalizeTimeEntries(session), database);
                transaction.Complete();
            }
        }
    }
}
