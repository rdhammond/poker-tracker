using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class TotalHourlyRateRepositoryMock
        : ReadOnlyRepositoryMock<ITotalHourlyRateRepository, TotalHourlyRateDao>
    {
        public TotalHourlyRateRepositoryMock()
        {
            Setup(x => x.GetTotalHourlyRateAsync())
                .Returns(() => Task.FromResult(DaoList.First().TotalHourlyRate));
        }
    } 
}
