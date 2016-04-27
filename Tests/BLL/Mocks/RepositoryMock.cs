using Moq;
using PokerTracker.DAL.Repositories;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class RepositoryMock<TRepo,TEntity> : ReadOnlyRepositoryMock<TRepo,TEntity>
        where TRepo : Repository<TEntity>
    {
        public RepositoryMock()
        {
            Setup(x => x.SaveAsync(It.IsAny<TEntity>()))
                .Returns<TEntity>(x => Task.Run(() => DaoList.Add(x)));
        }
    }
}
