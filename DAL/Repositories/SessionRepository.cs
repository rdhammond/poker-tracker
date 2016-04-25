using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;

namespace PokerTracker.DAL.Repositories
{
    public interface ISessionRepository : IRepository<SessionDao>
    {
    }

    public class SessionRepository : Repository<SessionDao>, ISessionRepository
    {
        public SessionRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
