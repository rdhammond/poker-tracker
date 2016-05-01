using PokerTracker.BLL.Objects;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Threading.Tasks;

namespace PokerTracker.Tests.WCF.Mocks
{
    public class SummaryServiceMock : LookupServiceMock<
        ISummaryService,
        SummaryDao,
        Summary,
        ISummaryRepository
    >
    {
        public decimal TotalHourlyRate { get; set; }

        public SummaryServiceMock()
        {
            Setup(x => x.GetAllAsync())
                .Returns(() => Task.FromResult(List.ToArray()));

            Setup(x => x.GetTotalHourlyRateAsync())
                .Returns(() => Task.FromResult(TotalHourlyRate));
        }
    }
}
