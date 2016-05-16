using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using PokerTracker.Tests.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;

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

        protected void Setup()
        {
            _mapper = MapperFactory.Create();
            _repoMock = new TRepoMock();
        }

        protected static void AssertDaoToListWithId<TIn,TOut> (
            IEnumerable<TIn> expected,
            IEnumerable<TOut> actual,
            DaoObjectComparer<TIn,TOut> comparer
        )
            where TIn : IdDao
            where TOut : IdObject
        {
            AssertDaoToListWithId(
                x => x.Id,
                y => y.Id,
                expected,
                actual,
                comparer
            );
        }

        protected static void AssertDaoToListWithId<TIn,TOut>(
            Func<TIn,Guid> getDaoId,
            Func<TOut,Guid> getObjectId,
            IEnumerable<TIn> expected,
            IEnumerable<TOut> actual,
            DaoObjectComparer<TIn,TOut> comparer
        )
            where TIn : IDao
        {
            Assert.AreEqual(expected.Count(), actual.Count());

            var expectedDict = expected.ToDictionary(x => getDaoId(x));
            
            foreach (var a in actual)
            {
                var e = expectedDict[getObjectId(a)];
                Assert.IsTrue(comparer.Equals(e, a));
            }
        }
    }
}
