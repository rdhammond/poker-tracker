using PokerTracker.Config;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Services
{
    public class DataService
    {
        private readonly IConfig Config;

        public DataService(IConfig config)
        {
            Config = config;
        }

        protected async Task RunQueryAsync(Func<SqlConnection,Task> func)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                await func(connection);
            }
        }

        protected async Task<T> RunQueryAsync<T>(Func<SqlConnection,Task<T>> func)
        {
            using (var connection = new SqlConnection(Config.ConnectionString))
            {
                return await func(connection);
            }
        }
    }
}
