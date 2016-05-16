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
        private DatabaseMock<TEntity> _databaseMock;

        protected DatabaseMock<TEntity> DatabaseMock
        {
            get { return _databaseMock; }
        }

        protected TRepository Repo { get; set; }

        public virtual void SetUp()
        {
            _databaseMock = new DatabaseMock<TEntity>();
        }

        protected static void AssertListWithId<T>(IEnumerable<T> expected, IEnumerable<T> actual, EqualityComparer<T> comparer)
            where T : IdDao
        {
            AssertListWithId(x => x.Id, expected, actual, comparer);
        }

        protected static void AssertListWithId<T>(Func<T,Guid> getId, IEnumerable<T> expected, IEnumerable<T> actual, EqualityComparer<T> comparer)
        {
            Assert.AreEqual(expected.Count(), actual.Count());

            var expectedDict = expected.ToDictionary(x => getId(x));

            foreach (var a in actual)
            {
                var e = expectedDict[getId(a)];
                Assert.IsNotNull(e);
                Assert.IsTrue(comparer.Equals(e, a));
            }
        }
    }
}
