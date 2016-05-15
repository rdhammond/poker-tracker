using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class RepositoryTests<TRepo,TEntity> : ReadOnlyRepositoryTests<TRepo,TEntity>
        where TEntity : IDao
        where TRepo : Repository<TEntity>
    {
        protected void TestInsertAsync(TEntity entity)
        {
            Repo.AddAsync(entity).Wait();
            Assert.IsTrue(DaoList.Contains(entity));
        }

        protected void TestInsertAsync(IEnumerable<TEntity> entities)
        {
            Repo.AddAsync(entities).Wait();
            Assert.IsTrue(entities.All(x => DaoList.Contains(x)));
        }
    }
}
