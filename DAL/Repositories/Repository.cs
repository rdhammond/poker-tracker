using AsyncPoco;
using PokerTracker.DAL.Factories;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        Task SaveAsync(T entity);
        Task SaveAsync(T entity, Database database);
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

        public async Task SaveAsync(T entity, Database database)
        {
            await database.SaveAsync(entity);
        }
    }
}
