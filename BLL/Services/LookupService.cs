﻿using AutoMapper;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Services
{
    public interface ILookupService<TOut>
    {
        Task<TOut[]> GetAllAsync();
    }

    public abstract class LookupService<TIn, TOut, TRepo>
        : ILookupService<TOut>
        where TIn : IDao
        where TRepo : IReadOnlyRepository<TIn>
    {
        protected readonly IMapper _mapper;
        protected readonly TRepo _repo;

        protected LookupService(IMapper mapper, TRepo repository)
        {
            _mapper = mapper;
            _repo = repository;
        }

        public async Task<TOut[]> GetAllAsync()
        {
            return (await _repo.FindAllAsync())
                .Select(x => _mapper.Map<TOut>(x))
                .ToArray();
        }
    }
}
