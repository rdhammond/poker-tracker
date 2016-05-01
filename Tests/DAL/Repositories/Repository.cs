using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class RepositoryTests<TRepo,TEntity>
        : ReadOnlyRepositoryTests<TRepo,TEntity>
        where TRepo : ReadOnlyRepository<TEntity>
    {
        protected void TestSaveAsync(TEntity entity)
        {
            var repo = (IRepository<TEntity>)Repo;

            repo.SaveAsync(entity).Wait();
            Assert.IsTrue(DaoList.Contains(entity));
        }

        protected void TestSaveAsync(IEnumerable<TEntity> entitiesEnum)
        {
            var entities = entitiesEnum as TEntity[] ?? entitiesEnum.ToArray();
            var repo = (IRepository<TEntity>)Repo;

            repo.SaveAsync(entities).Wait();
            Assert.IsTrue(entities.All(x => DaoList.Contains(x)));
        }
    }
}
