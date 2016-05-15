using Moq;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.WCF.Mocks
{
    public interface ILookupServiceMock<TOut>
    {
        List<TOut> List { get; }
    }

    public abstract class LookupServiceMock<TService,TIn,TOut,TRepo>
        : Mock<TService>, ILookupServiceMock<TOut>
        where TIn : IDao
        where TRepo : IReadOnlyRepository<TIn>
        where TService : class, ILookupService<TOut>
    {
        private readonly List<TOut> _list = new List<TOut>();

        public List<TOut> List
        {
            get { return _list; }
        }

        public LookupServiceMock()
        {
            Setup(x => x.GetAllAsync())
                .Returns(() => Task.FromResult(List.ToArray()));
        }
    }
}
