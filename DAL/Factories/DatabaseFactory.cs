using AsyncPoco;
using PokerTracker.DAL.Wrappers;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Factories
{
    public interface IDatabaseFactory
    {
        Task<IDatabaseWrapper> CreateAsync();
    }

    public class DatabaseFactory : IDatabaseFactory
    {
        public const string CONN_STR_NAME = "PokerTracker";

        public async Task<IDatabaseWrapper> CreateAsync()
        {
            var result = new DatabaseWrapper(CONN_STR_NAME);
            await result.OpenSharedConnectionAsync();
            return result;
        }
    }
}
