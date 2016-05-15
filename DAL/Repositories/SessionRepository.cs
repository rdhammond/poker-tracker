using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface ISessionRepository : IRepository<SessionDao>
    { }

    public class SessionRepository : Repository<SessionDao>, ISessionRepository
    {
        public SessionRepository(IDatabase database)
            : base(database)
        { }
    }
}
