using Moq;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class ReadOnlyRepositoryMock<TRepo,TEntity> : Mock<TRepo>
        where TRepo : ReadOnlyRepository<TEntity>
    {
        public readonly List<TEntity> DaoList = new List<TEntity>();

        public ReadOnlyRepositoryMock()
        {
            Setup(x => x.FindAllAsync())
                .Returns(() => Task.FromResult<IList<TEntity>>(DaoList));
        }
    }
}
