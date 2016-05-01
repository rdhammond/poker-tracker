using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        Task SaveAsync(T entity);
        Task SaveAsync(T entity, IDatabaseWrapper database);
        Task SaveAsync(IEnumerable<T> entities);
        Task SaveAsync(IEnumerable<T> entities, IDatabaseWrapper database);
    }

    public abstract class Repository<T> : ReadOnlyRepository<T>, IRepository<T>
    {
        protected Repository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }

        public async Task SaveAsync(T entity)
        {
            using (var database = DbFactory.Create())
            {
                await SaveAsync(entity, database);
            }
        }

        public Task SaveAsync(T entity, IDatabaseWrapper database)
        {
            return database.SaveAsync(entity);
        }

        public async Task SaveAsync(IEnumerable<T> entities)
        {
            using (var database = DbFactory.Create())
            {
                await SaveAsync(entities, database);
            }
        }

        public async Task SaveAsync(IEnumerable<T> entities, IDatabaseWrapper database)
        {
            foreach (var entity in entities)
            {
                await database.SaveAsync(entity);
            }
        }
    }
}
