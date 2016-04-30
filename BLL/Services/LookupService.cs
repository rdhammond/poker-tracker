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
        Task<IReadOnlyList<TOut>> GetAllAsync();
    }

    public abstract class LookupService<TIn, TOut, TRepo> : ILookupService<TIn, TOut, TRepo>
        where TRepo : IReadOnlyRepository<TIn>
    {
        private readonly IMapper Mapper;
        protected readonly TRepo Repository;

        protected LookupService(IMapper mapper, TRepo repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public async Task<IReadOnlyList<TOut>> GetAllAsync()
        {
            return (await Repository.FindAllAsync())
                .Select(x => Mapper.Map<TOut>(x))
                .ToList()
                .AsReadOnly();
        }
    }
}
