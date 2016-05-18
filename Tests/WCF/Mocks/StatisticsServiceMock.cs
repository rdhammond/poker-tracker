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
        public Swing BiggestUpswing { get; set; }
        public Swing BiggestDownswing { get; set; }

        public StatisticsServiceMock()
        {
            Setup(x => x.GetAsync())
                .Returns(() => Task.Run(() =>
                {
                    var result = List.First();
                    result.BiggestUpswing = BiggestUpswing;
                    result.BiggestDownswing = BiggestDownswing;
                    return result;
                }));
        }
    }
}
