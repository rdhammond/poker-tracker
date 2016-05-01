using Moq;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public abstract class ReadOnlyRepositoryMock<TRepo,TEntity>
        : Mock<TRepo>
        where TRepo : class, IReadOnlyRepository<TEntity>
    {
        private readonly List<TEntity> _daoList = new List<TEntity>();

        public List<TEntity> DaoList
        {
            get { return _daoList; }
        }

        public ReadOnlyRepositoryMock()
        {
            Setup(x => x.FindAllAsync())
                .Returns(() => Task.FromResult(DaoList));
        }
    }
}
