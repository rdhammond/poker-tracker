using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.DAL.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class RepositoryTests<TEntity,TRepository>
        where TRepository : ReadOnlyRepository<TEntity>
    {
        protected readonly List<TEntity> DaoList = new List<TEntity>();
        protected readonly DatabaseFactoryMock DbFactMock = new DatabaseFactoryMock();
        protected readonly DatabaseWrapperMock DbWrapperMock = new DatabaseWrapperMock();

        protected RepositoryTests()
        {
            DbWrapperMock.AddList(DaoList);
            DbFactMock.Database = DbWrapperMock.Object;
        }

        private TRepository CreateRepository()
        {
            return (TRepository)typeof(TRepository)
                .GetConstructor(new[] { typeof(IDatabaseFactory) })
                .Invoke(new[] { DbFactMock.Object });
        }

        protected void TestFindAllAsync(IEnumerable<TEntity> entityList)
        {
            DaoList.AddRange(entityList);
            var actual = CreateRepository().FindAllAsync().Result;

            Assert.IsNotNull(actual);
            Assert.AreSame(DaoList, actual);
        }

        protected void TestSaveAsync(TEntity entity)
        {
            var repo = (IRepository<TEntity>)CreateRepository();
            repo.SaveAsync(entity).Wait();

            Assert.IsTrue(DaoList.Contains(entity));
        }

        protected void TestSaveAsync(IEnumerable<TEntity> entities)
        {
            var repo = (IRepository<TEntity>)CreateRepository();
            repo.SaveAsync(entities).Wait();

            Assert.IsTrue(entities.All(x => DaoList.Contains(x)));
        }
    }
}
