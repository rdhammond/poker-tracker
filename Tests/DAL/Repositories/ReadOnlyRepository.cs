using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.DAL.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class ReadOnlyRepositoryTests<TRepository, TEntity>
        where TEntity : IDao
        where TRepository : ReadOnlyRepository<TEntity>
    {
        private List<TEntity> _daoList;
        private DatabaseMock<TEntity> _databaseMock;

        public List<TEntity> DaoList
        {
            get { return _daoList; }
        }

        protected DatabaseMock<TEntity> DatabaseMock
        {
            get { return _databaseMock; }
        }

        protected TRepository Repo { get; set; }

        public virtual void SetUp()
        {
            _daoList = new List<TEntity>();
            _databaseMock = new DatabaseMock<TEntity>(_daoList);
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
        
        protected void TestFindAllAsync<T>(IEnumerable<T> expected)
            where T : IdNameDao, TEntity
        {
            DaoList.AddRange(expected);

            var actual = Repo.FindAllAsync().Result
                .Cast<T>()
                .ToDictionary(x => x.Id);

            AssertListEquals(expected, actual);
        }

        protected void TestFindAllAsync<T>(
            IEnumerable<T> expected,
            Func<T,T,bool> equals
        )
            where T : IdDao, TEntity
        {
            DaoList.AddRange(expected);

            var actual = Repo.FindAllAsync().Result
                .Cast<T>()
                .ToDictionary(x => x.Id);

            AssertListEquals(expected, actual, equals);
        }

        #endregion
    }
}
