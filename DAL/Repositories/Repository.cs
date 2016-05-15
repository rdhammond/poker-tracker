using System.Collections.Generic;
using System.Threading.Tasks;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IDao
    {
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
    }

    public abstract class Repository<T> : ReadOnlyRepository<T>, IRepository<T>
        where T : IDao
    {
        protected Repository(IDatabase database)
            : base(database)
        { }

        public async Task AddAsync(T entity)
        {
            await Database.InsertAsync(entity);
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await Database.InsertAsync(entities);
        }
    }
}
