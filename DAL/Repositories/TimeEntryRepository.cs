using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface ITimeEntryRepository : IRepository<TimeEntryDao>
    { }

    public class TimeEntryRepository : Repository<TimeEntryDao>, ITimeEntryRepository
    {
        public TimeEntryRepository(IDatabase database)
            : base(database)
        { }
    }
}
