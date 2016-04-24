using AutoMapper;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Services
{
    public interface IIdNameService<TIn, TOut, TRepo>
        where TOut: IdNameObject
        where TRepo : IReadOnlyRepository<TIn>
    {
        Task<IList<TOut>> GetAllAsync();
    }

    public abstract class IdNameService<TIn, TOut, TRepo> : IIdNameService<TIn, TOut, TRepo>
        where TOut: IdNameObject
        where TRepo : IReadOnlyRepository<TIn>
    {
        private readonly IMapper Mapper;
        private readonly TRepo Repository;

        protected IdNameService(IMapper Mapper, TRepo repository)
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
