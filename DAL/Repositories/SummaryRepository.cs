using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface ISummaryRepository : IReadOnlyRepository<SummaryDao>
    { }

    public class SummaryRepository : ReadOnlyRepository<SummaryDao>, ISummaryRepository
    {
        public SummaryRepository(IDatabase database)
            : base(database)
        { }
    }
}
