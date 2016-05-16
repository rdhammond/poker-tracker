using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface IStatisticsRepository : IReadOnlyRepository<StatisticsDao>
    { }

    public class StatisticsRepository : ReadOnlyRepository<StatisticsDao>, IStatisticsRepository
    {
        public StatisticsRepository(IDatabase database)
            : base(database)
        { }
    }
}
