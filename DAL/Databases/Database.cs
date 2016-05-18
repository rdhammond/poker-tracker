using Dapper;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Extensions;
using PokerTracker.DAL.Factories;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PokerTracker.DAL.Databases
{
    public interface IDatabase
    {
        Task<List<T>> FetchAllAsync<T>() where T : IDao;
        Task InsertAsync<T>(T dao) where T : IDao;
        Task InsertAsync<T>(IEnumerable<T> daos) where T : IDao;
        Task<IEnumerable<T>> RunAsync<T>(string storedProc, object parameters = null) where T : IDao;
        Task PulseTestAsync();
    }

    public class Database : IDatabase
    {
        private readonly IDbConnectionFactory _dbConnectionFact;

        public Database(IDbConnectionFactory dbConnectionFact)
        {
            _dbConnectionFact = dbConnectionFact;
        }
        
        public static TransactionScope BeginTransaction()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        private async Task<DbConnection> ConnectAsync()
        {
            var connection = _dbConnectionFact.Create();

            try
            {
                await connection.OpenAsync();
            }
            catch
            {
                connection.Dispose();
                throw;
            }

            if (Transaction.Current != null)
                connection.EnlistTransaction(Transaction.Current);

            return connection;
        }

        public static string SelectAllSql<T>()
            where T : IDao
        {
            return string.Format("SELECT * FROM [{0}]", typeof(T).GetTableName());
        }

        public async Task<List<T>> FetchAllAsync<T>()
            where T : IDao
        {
            using (var connection = await ConnectAsync())
            {
                return (await connection.QueryAsync<T>(SelectAllSql<T>()))
                    .ToList();
            }
        }

        public static string InsertSql<T>()
            where T : IDao
        {
            var properties = typeof(T).GetPublicPropertyNames().ToArray();

            return string.Format(
                "INSERT INTO [{0}]({1}) VALUES({2})",
                typeof(T).GetTableName(),
                string.Join(",", from p in properties select "[" + p + "]"),
                string.Join(",", from p in properties select "@" + p)
            );
        }

        public async Task InsertAsync<T>(T dao)
            where T : IDao
        {
            await InsertAsync<T>(new[] { dao });
        }

        public async Task InsertAsync<T>(IEnumerable<T> daos)
            where T : IDao
        {
            using (var connection = await ConnectAsync())
            {
                await connection.ExecuteAsync(InsertSql<T>(), daos);
            }
        }

        public async Task<IEnumerable<T>> RunAsync<T>(string storedProc, object parameters = null)
            where T : IDao
        {
            using (var connection = await ConnectAsync())
            {
                return await connection.QueryAsync<T>(
                    storedProc,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task PulseTestAsync()
        {
            using (var connection = await ConnectAsync())
            { }
        }
    }
}
