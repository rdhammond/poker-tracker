using AutoMapper;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Services
{
    public interface ILookupService<TIn, TOut, TRepo>
        where TRepo : IReadOnlyRepository<TIn>
    {
        Task<IList<TOut>> GetAllAsync();
    }

    public abstract class LookupService<TIn, TOut, TRepo> : ILookupService<TIn, TOut, TRepo>
        where TRepo : IReadOnlyRepository<TIn>
    {
        private readonly IMapper Mapper;
        protected readonly TRepo Repository;

        protected LookupService(IMapper Mapper, TRepo repository)
        {
            Repository = repository;
        }

        public async Task<IList<TOut>> GetAllAsync()
        {
            return (await Repository.FindAllAsync())
                .Select(x => Mapper.Map<TOut>(x))
                .ToList();
        }
    }
}
