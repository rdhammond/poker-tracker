using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;

namespace PokerTracker.DAL.Repositories
{
    public interface ISummaryRepository : IReadOnlyRepository<SummaryDao>
    {
    }

    public class SummaryRepository : ReadOnlyRepository<SummaryDao>, ISummaryRepository
    {
        public SummaryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
