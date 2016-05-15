using AutoMapper;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using System.Collections.Generic;

namespace PokerTracker.Tests.BLL.Services
{
    public abstract class LookupServiceTests<TRepo, TDao, TRepoMock>
        where TDao : IDao
        where TRepo : class, IReadOnlyRepository<TDao>
        where TRepoMock : ReadOnlyRepositoryMock<TRepo, TDao>, new()
    {
        private IMapper _mapper;
        private TRepoMock _repoMock;

        protected IMapper Mapper { get { return _mapper; } }
        protected TRepoMock RepoMock {  get { return _repoMock; } }

        protected List<TDao> DaoList
        {
            get { return _repoMock.DaoList; }
        }

        protected void Setup()
        {
            _mapper = MapperFactory.Create();
            _repoMock = new TRepoMock();
        }
    }
}
