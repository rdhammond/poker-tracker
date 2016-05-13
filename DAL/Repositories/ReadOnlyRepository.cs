using PokerTracker.DAL.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        Task<List<T>> FindAllAsync();
    }

    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T>
    {
        protected readonly IDatabaseFactory DbFactory;

        protected ReadOnlyRepository(IDatabaseFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public async Task<List<T>> FindAllAsync()
        {
            using (var database = await DbFactory.CreateAsync())
            {
                return await database.FetchAsync<T>(string.Empty);
            }
        }
    }
}
