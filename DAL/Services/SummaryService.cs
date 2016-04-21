using Dapper;
using PokerTracker.Config;
using PokerTracker.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Services
{
    public interface ISummaryService
    {
        IEnumerable<Summary> GetAll();
        Decimal TotalHourlyRate();

        Task<IEnumerable<Summary>> GetAllAsync();
        Task<Decimal> TotalHourlyRateAsync();
    }

    public class SummaryService : DataService, ISummaryService
    {
        public readonly IConfig Config;

        public SummaryService(IConfig config)
            :base (config)
        { }

        public IEnumerable<Summary> GetAll()
        {
            return AsyncHelper.RunSync(GetAllAsync);
        }

        public async Task<IEnumerable<Summary>> GetAllAsync()
        {
            return await RunQueryAsync(async (connection) => {
                return await connection.QueryAsync<Summary>("SELECT * FROM vw_Summary");
            });
        }

        public Decimal TotalHourlyRate()
        {
            return AsyncHelper.RunSync(TotalHourlyRateAsync);
        }

        public async Task<Decimal> TotalHourlyRateAsync()
        {
            return await RunQueryAsync<Decimal>(async (connection) => {
                return
                    (await connection.QueryAsync<Decimal>("SELECT udf_TotalHourlyRate()"))
                    .FirstOrDefault();
            });
        }
    }
}
