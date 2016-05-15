using PokerTracker.BLL.Objects;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PokerTracker.DAL.DAO;
using System.Linq;
using PokerTracker.DAL.Databases;

namespace PokerTracker.BLL.Services
{
    public interface ISessionService
    {
        Task SaveSessionAsync(Session session);
    }

    public class SessionService : ISessionService
    {
        private readonly IMapper Mapper;
        private readonly ISessionRepository SessionRepo;
        private readonly ITimeEntryRepository TimeEntryRepo;

        public SessionService(
            IMapper mapper,
            ISessionRepository sessionRepo,
            ITimeEntryRepository timeEntryRepo
        )
        {
            Mapper = mapper;
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

        public async Task SaveSessionAsync(Session session)
        {
            using (var transaction = Database.BeginTransaction())
            {
                var sessionDao = Mapper.Map<SessionDao>(session);
                await SessionRepo.AddAsync(sessionDao);
                await TimeEntryRepo.AddAsync(FinalizeTimeEntries(session));

                transaction.Complete();
            }
        }
    }
}
