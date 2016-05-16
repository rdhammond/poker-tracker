using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class RepositoryTests<TRepo,TEntity> : ReadOnlyRepositoryTests<TRepo,TEntity>
        where TEntity : IdDao
        where TRepo : Repository<TEntity>
    {
        protected void TestInsertAsync(TEntity entity, EqualityComparer<TEntity> comparer)
        {
            Repo.AddAsync(entity).Wait();
            AssertListWithId(DatabaseMock.DaoList, new[] { entity }, comparer);
        }

        protected void TestInsertAsync(IEnumerable<TEntity> entities, EqualityComparer<TEntity> comparer)
        {
            Repo.AddAsync(entities).Wait();
            AssertListWithId(DatabaseMock.DaoList, entities, comparer);
        }
    }
}
