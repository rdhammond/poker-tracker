using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.DAL.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class ReadOnlyRepositoryTests<TRepository, TEntity>
        where TRepository : ReadOnlyRepository<TEntity>
    {
        private List<TEntity> _daoList;
        private DatabaseFactoryMock _dbFactMock;
        private DatabaseWrapperMock _dbWrapperMock;
        private TRepository _repo;

        protected TRepository Repo { get { return _repo; } }
        protected List<TEntity> DaoList {  get { return _daoList; } }

        protected void Setup()
        {
            _daoList = new List<TEntity>();
            _dbWrapperMock = new DatabaseWrapperMock();
            _dbFactMock = new DatabaseFactoryMock(_dbWrapperMock.Object);
            _repo = CreateRepository();

            _dbWrapperMock.AddList(_daoList);
        }

        protected TRepository CreateRepository()
        {
            return (TRepository)typeof(TRepository)
                .GetConstructor(new[] { typeof(IDatabaseFactory) })
                .Invoke(new[] { _dbFactMock.Object });
        }

        #region AssertListEquals

        private static void AssertListEquals<T>(
            IEnumerable<T> expected,
            Dictionary<Guid,T> actual
        )
            where T : IdNameDao
        {
            AssertListEquals(
                expected,
                actual,
                (s, d) => s.Id == d.Id && s.Name == d.Name
            );
        }

        private static void AssertListEquals<T>(
            IEnumerable<T> expected,
            Dictionary<Guid,T> actual,
            Func<T,T,bool> equals
        )
            where T : IdDao
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count);

            foreach (var expectedItem in expected)
            {
                var actualItem = actual[expectedItem.Id];
                Assert.IsNotNull(actualItem);
                Assert.IsTrue(equals(expectedItem, actualItem));
            }
        }

        #endregion

        #region TestFindAllAsync
        
        protected void TestFindAllAsync<T>(IEnumerable<T> expectedEnum)
            where T : IdNameDao
        {
            var expected = expectedEnum as T[] ?? expectedEnum.ToArray();
            DaoList.AddRange(expected.Cast<TEntity>());

            var actual = _repo.FindAllAsync().Result
                .Cast<T>()
                .ToDictionary(x => x.Id);

            AssertListEquals(expected, actual);
        }

        protected void TestFindAllAsync<T>(
            IEnumerable<T> expectedEnum,
            Func<T,T,bool> equals
        )
            where T : IdDao
        {
            var expected = expectedEnum as T[] ?? expectedEnum.ToArray();
            DaoList.AddRange(expected.Cast<TEntity>());

            var actual = _repo.FindAllAsync().Result
                .Cast<T>()
                .ToDictionary(x => x.Id);

            AssertListEquals(expected, actual, equals);
        }

        #endregion
    }
}
