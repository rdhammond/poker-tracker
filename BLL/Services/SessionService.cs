using PokerTracker.BLL.Objects;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PokerTracker.DAL.DAO;

namespace PokerTracker.BLL.Services
{
    public interface ISessionService
    {
        Task SaveSessionAsync(Session session, DateTime endTime, decimal hoursPlayed, string optionalNotes = null);
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

        public async Task SaveSessionAsync(Session session, DateTime endTime, decimal hoursActive, string optionalNotes = null)
        {
            using (var database = DbFactory.Create())
            using (var transaction = await database.GetTransactionAsync())
            {
                var sessionDao = Mapper.Map<SessionDao>(session);
                await SessionRepo.StartSessionAsync(sessionDao, database);

                foreach (var timeEntryDao in Mapper.Map<IEnumerable<TimeEntryDao>>(session.TimeEntries))
                {
                    timeEntryDao.SessionId = session.Id;
                    await TimeEntryRepo.SaveAsync(timeEntryDao, database);
                }

                await SessionRepo.FinalizeSessionAsync(sessionDao, endTime, hoursActive, optionalNotes, database);
                transaction.Complete();
            }
        }
    }
}
