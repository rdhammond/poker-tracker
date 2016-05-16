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
    { }
}
