using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class StatisticsRepositoryMock
        : ReadOnlyRepositoryMock<IStatisticsRepository, StatisticsDao>
    {
        public SwingDao BiggestDownswing { get; set; }
        public SwingDao BiggestUpswing { get; set; }

        public StatisticsRepositoryMock()
        {
            Setup(x => x.GetBiggestDownswingAsync()).Returns(() => Task.FromResult(BiggestDownswing));
            Setup(x => x.GetBiggestUpswingAsync()).Returns(() => Task.FromResult(BiggestUpswing));
        }
    }
}
