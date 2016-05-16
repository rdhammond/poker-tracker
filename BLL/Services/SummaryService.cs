using AutoMapper;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.BLL.Services
{
    public interface ISummaryService : ILookupService<Summary>
    { }

    public class SummaryService
        : LookupService<SummaryDao, Summary, ISummaryRepository>, ISummaryService
    {
        public SummaryService(IMapper mapper, ISummaryRepository repository)
            : base(mapper, repository)
        { }
    }
}
