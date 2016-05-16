using AutoMapper;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Services
{
    public interface IStatisticsService : ILookupService<Statistics>
    {
        Task<Statistics> GetAsync();
    }

    public class StatisticsService
        : LookupService<StatisticsDao, Statistics, IStatisticsRepository>, IStatisticsService
    {
        public StatisticsService(IMapper mapper, IStatisticsRepository repository)
            : base(mapper, repository)
        { }

        public async Task<Statistics> GetAsync()
        {
            return (await GetAllAsync()).FirstOrDefault();
        }
    }
}
