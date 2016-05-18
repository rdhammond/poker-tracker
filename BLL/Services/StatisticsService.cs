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
            var statisticsTask = Repository.FindAllAsync();
            var biggestUpswingTask = Repository.GetBiggestUpswingAsync();
            var biggestDownswingTask = Repository.GetBiggestDownswingAsync();

            await Task.WhenAll(statisticsTask, biggestUpswingTask, biggestDownswingTask);

            var statistics = Mapper.Map<Statistics>(statisticsTask.Result.FirstOrDefault());

            if (statistics != null)
            {
                statistics.BiggestUpswing = Mapper.Map<Swing>(biggestUpswingTask.Result);
                statistics.BiggestDownswing = Mapper.Map<Swing>(biggestDownswingTask.Result);
            }

            return statistics;
        }
    }
}
