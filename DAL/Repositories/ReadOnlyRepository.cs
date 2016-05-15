using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface IReadOnlyRepository<T>
        where T : IDao
    {
        Task<List<T>> FindAllAsync();
    }

    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T>
        where T : IDao
    {
        private readonly IDatabase _database;

        protected IDatabase Database
        {
            get { return _database; }
        }

        protected ReadOnlyRepository(IDatabase database)
        {
            _database = database;
        }

        public async Task<List<T>> FindAllAsync()
        {
            return (await Database.FetchAllAsync<T>())
                .ToList();
        }
    }
}
