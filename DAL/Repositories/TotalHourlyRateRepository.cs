using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface ITotalHourlyRateRepository : IReadOnlyRepository<TotalHourlyRateDao>
    {
        Task<decimal> GetTotalHourlyRateAsync();
    }

    public class TotalHourlyRateRepository
        : Repository<TotalHourlyRateDao>, ITotalHourlyRateRepository
    {
        public TotalHourlyRateRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }

        public async Task<decimal> GetTotalHourlyRateAsync()
        {
            return (await FindAllAsync()).First().TotalHourlyRate;
        }
    }
}
