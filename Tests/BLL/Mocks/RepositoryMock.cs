using Moq;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class RepositoryMock<TRepo,TEntity> : ReadOnlyRepositoryMock<TRepo,TEntity>
        where TEntity : IDao
        where TRepo : class, IRepository<TEntity>
    {
        public RepositoryMock()
            : base()
        {
            Setup(x => x.AddAsync(It.IsAny<TEntity>()))
                .Returns<TEntity>(e => Task.Run(() => DaoList.Add(e))); 

            Setup(x => x.AddAsync(It.IsAny<IEnumerable<TEntity>>()))
                .Returns<IEnumerable<TEntity>>(l => Task.Run(() => DaoList.AddRange(l)));
        }
    }
}
