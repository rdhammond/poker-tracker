using AsyncPoco;
using PokerTracker.Common;
using PokerTracker.DAL.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Services
{
    public interface ISummaryService
    {
        IEnumerable<Summary> GetAll();
        Task<IEnumerable<Summary>> GetAllAsync();
    }

    public class SummaryService : ISummaryService
    {
        private readonly string _connectionString;

        public SummaryService(IConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        public SummaryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Summary> GetAll()
        {
            return AsyncHelper.RunSync(() => GetAllAsync());
        }

        public async Task<IEnumerable<Summary>> GetAllAsync()
        {
            using (var db = new Database(_connectionString))
            {
                return await db.FetchAsync<Summary>(string.Empty);
            }
        }
    }
}
