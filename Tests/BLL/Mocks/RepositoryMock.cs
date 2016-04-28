using Moq;
using PokerTracker.DAL.Repositories;
using PokerTracker.DAL.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class RepositoryMock<TRepo,TEntity> : ReadOnlyRepositoryMock<TRepo,TEntity>
        where TRepo : class, IRepository<TEntity>
    {
        public RepositoryMock()
            : base()
        {
            Setup(x => x.SaveAsync(It.IsAny<TEntity>()))
                .Returns<TEntity>(e => Task.Run(() => DaoList.Add(e)));

            Setup(x => x.SaveAsync(It.IsAny<TEntity>(), It.IsAny<IDatabaseWrapper>()))
                .Returns<TEntity,IDatabaseWrapper>((e,d) => Task.Run(() => DaoList.Add(e)));

            Setup(x => x.SaveAsync(It.IsAny<IEnumerable<TEntity>>(),
                It.IsAny<IDatabaseWrapper>())
            )
                .Returns<IEnumerable<TEntity>, IDatabaseWrapper>((l, d) =>
                     Task.Run(() => DaoList.AddRange(l)));
        }
    }
}
