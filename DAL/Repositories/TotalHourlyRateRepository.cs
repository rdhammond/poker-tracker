using PokerTracker.DAL.DAO;
using System.Linq;
using System.Threading.Tasks;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface ITotalHourlyRateRepository : IReadOnlyRepository<TotalHourlyRateDao>
    {
        Task<decimal> GetTotalHourlyRateAsync();
    }

    public class TotalHourlyRateRepository : Repository<TotalHourlyRateDao>, ITotalHourlyRateRepository
    {
        public TotalHourlyRateRepository(IDatabase database)
            : base(database)
        { }

        public async Task<decimal> GetTotalHourlyRateAsync()
        {
            return (await FindAllAsync()).First().TotalHourlyRate;
        }
    }
}
