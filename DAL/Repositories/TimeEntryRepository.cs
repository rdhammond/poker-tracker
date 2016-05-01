using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;

namespace PokerTracker.DAL.Repositories
{
    public interface ITimeEntryRepository : IRepository<TimeEntryDao>
    { }

    public class TimeEntryRepository : Repository<TimeEntryDao>, ITimeEntryRepository
    {
        public TimeEntryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
