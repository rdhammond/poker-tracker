using AsyncPoco;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        Task SaveAsync(T entity);
        //Task SaveAsync(T entity, IDatabaseWrapper database);
        Task SaveAsync(T entity, Database database);
        Task SaveAsync(IEnumerable<T> entities);
        //Task SaveAsync(IEnumerable<T> entities, IDatabaseWrapper database);
        Task SaveAsync(IEnumerable<T> entities, Database database);
    }

    public abstract class Repository<T> : ReadOnlyRepository<T>, IRepository<T>
    {
        protected Repository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }

        public async Task SaveAsync(T entity)
        {
            using (var database = await DbFactory.CreateAsync())
            {
                await SaveAsync(entity, database as Database);
            }
        }

        //public Task SaveAsync(T entity, IDatabaseWrapper database)
        public async Task SaveAsync(T entity, Database database)
        {
            await database.InsertAsync(entity);
        }

        public async Task SaveAsync(IEnumerable<T> entities)
        {
            using (var database = await DbFactory.CreateAsync())
            {
                await SaveAsync(entities, database as Database);
            }
        }

        //public async Task SaveAsync(IEnumerable<T> entities, IDatabaseWrapper database)
        public async Task SaveAsync(IEnumerable<T> entities, Database database)
        {
            foreach (var entity in entities)
            {
                await database.SaveAsync(entity);
            }
        }
    }
}
