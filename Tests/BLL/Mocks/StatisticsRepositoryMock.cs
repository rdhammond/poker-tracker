using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class StatisticsRepositoryMock
        : ReadOnlyRepositoryMock<IStatisticsRepository, StatisticsDao>
    { }
}
