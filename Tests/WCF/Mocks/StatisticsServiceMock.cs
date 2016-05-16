using PokerTracker.BLL.Objects;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.Tests.WCF.Mocks
{
    public class StatisticsServiceMock
        : LookupServiceMock<IStatisticsService, StatisticsDao, Statistics, IStatisticsRepository>
    {
        public StatisticsServiceMock()
        {
            Setup(x => x.GetAsync()).Returns(() => Task.FromResult(List.First()));
        }
    }
}
