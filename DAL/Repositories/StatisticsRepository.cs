using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;
using System.Threading.Tasks;
using System.Linq;

namespace PokerTracker.DAL.Repositories
{
    public interface IStatisticsRepository : IReadOnlyRepository<StatisticsDao>
    {
        Task<SwingDao> GetBiggestDownswingAsync();
        Task<SwingDao> GetBiggestUpswingAsync();
    }

    public class StatisticsRepository : ReadOnlyRepository<StatisticsDao>, IStatisticsRepository
    {
        public StatisticsRepository(IDatabase database)
            : base(database)
        { }

        private async Task<SwingDao> GetBiggestSwingAsync(string storedProc)
        {
            var result = (await Database.RunAsync<SwingDao>(storedProc)).FirstOrDefault();

            return result != null && result.BiggestSwing > 0
                ? result
                : null;
        }

        public async Task<SwingDao> GetBiggestDownswingAsync()
        {
            return await GetBiggestSwingAsync("usp_BiggestDownswing");
        }

        public async Task<SwingDao> GetBiggestUpswingAsync()
        {
            return await GetBiggestSwingAsync("usp_BiggestUpswing");
        }
    }
}
