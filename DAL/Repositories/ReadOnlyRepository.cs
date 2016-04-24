using PokerTracker.DAL.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        Task<IList<T>> FindAllAsync();
    }

    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T>
    {
        protected readonly IDatabaseFactory DbFactory;

        protected ReadOnlyRepository(IDatabaseFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public async Task<IList<T>> FindAllAsync()
        {
            using (var database = DbFactory.Create())
            {
                return await database.FetchAsync<T>(string.Empty);
            }
        }
    }
}
