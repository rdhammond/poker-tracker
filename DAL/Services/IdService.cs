using Dapper;
using PokerTracker.Config;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Services
{
    public interface IIdService
    {
        Guid? CardRoomIdByName(string name);
        Guid? GameIdByName(string name);

        Task<Guid?> CardRoomIdByNameAsync(string name);
        Task<Guid?> GameIdByNameAsync(string name);
    }

    public class IdService : DataService, IIdService
    {
        public IdService(IConfig config)
            :base (config)
        { }

        public Guid? CardRoomIdByName(string name)
        {
            return AsyncHelper.RunSync(() => CardRoomIdByNameAsync(name));
        }

        public async Task<Guid?> CardRoomIdByNameAsync(string name)
        {
            return await RunQueryAsync(async (connection) =>
            {
                return (await connection.QueryAsync<Guid?>(
                    "SELECT dbo.udf_CardRoomIdByName(@Name)",
                    new { Name = name }
                )).FirstOrDefault();
            });
        }

        public Guid? GameIdByName(string name)
        {
            return AsyncHelper.RunSync(() => GameIdByNameAsync(name));
        }

        public async Task<Guid?> GameIdByNameAsync(string name)
        {
            return await RunQueryAsync(async (connection) =>
            {
                return (await connection.QueryAsync<Guid?>(
                    "SELECT dbo.udf_GameIdByNameAsync(@Name)",
                    new { Name = name }
                )).FirstOrDefault();
            });
        }
    }
}
