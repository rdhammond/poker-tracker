using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;

namespace PokerTracker.DAL.Repositories
{
    public interface ISummaryRepository : IRepository<SummaryDao>
    {
    }

    public class SummaryRepository : Repository<SummaryDao>, ISummaryRepository
    {
        public SummaryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
