using AutoMapper;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Services
{
    public interface ISummaryService : ILookupService<SummaryDao, Summary, ISummaryRepository>
    {
        Task<decimal> GetTotalHourlyRateAsync();
    }

    public class SummaryService : LookupService<SummaryDao, Summary, ISummaryRepository>, ISummaryService
    {
        private ITotalHourlyRateRepository TotalHourlyRateRepo;

        public SummaryService(
            IMapper mapper, ISummaryRepository repository,
            ITotalHourlyRateRepository totalHourlyRateRepo
        )
            : base(mapper, repository)
        {
            TotalHourlyRateRepo = totalHourlyRateRepo;
        }

        public async Task<decimal> GetTotalHourlyRateAsync()
        {
            return await TotalHourlyRateRepo.GetTotalHourlyRateAsync();
        }
    }
}
