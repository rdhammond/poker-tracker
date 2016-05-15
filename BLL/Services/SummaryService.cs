using AutoMapper;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Services
{
    public interface ISummaryService : ILookupService<Summary>
    {
        Task<decimal> GetTotalHourlyRateAsync();
    }

    public class SummaryService
        : LookupService<SummaryDao, Summary, ISummaryRepository>, ISummaryService
    {
        private readonly ITotalHourlyRateRepository _totalHourlyRateRepo;

        public SummaryService(
            IMapper mapper,
            ISummaryRepository repository,
            ITotalHourlyRateRepository totalHourlyRateRepo
        )
            : base(mapper, repository)
        {
            _totalHourlyRateRepo = totalHourlyRateRepo;
        }

        public async Task<decimal> GetTotalHourlyRateAsync()
        {
            return await _totalHourlyRateRepo.GetTotalHourlyRateAsync();
        }
    }
}
