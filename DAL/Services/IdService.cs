using AsyncPoco;
using PokerTracker.Common;
using PokerTracker.DAL.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Services
{
    public interface IIdService
    {
        IEnumerable<Game> GetGames();
        Task<IEnumerable<Game>> GetGamesAsync();

        IEnumerable<CardRoom> GetCardRooms();
        Task<IEnumerable<CardRoom>> GetCardRoomsAsync();
    }

    public class IdService : IIdService
    {
        private readonly string _connectionString;

        public IdService(IConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        public IdService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Game> GetGames()
        {
            return AsyncHelper.RunSync(() => GetGamesAsync());
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            using (var db = new Database(_connectionString))
            {
                return await db.FetchAsync<Game>(string.Empty);
            }
        }

        public IEnumerable<CardRoom> GetCardRooms()
        {
            return AsyncHelper.RunSync(() => GetCardRoomsAsync());
        }

        public async Task<IEnumerable<CardRoom>> GetCardRoomsAsync()
        {
            using (var db = new Database(_connectionString))
            {
                return await db.FetchAsync<CardRoom>(string.Empty);
            }
        }
    }
}
