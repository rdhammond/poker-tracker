using PokerTracker.Tests.DAL.Mocks;
using System.Collections.Generic;

namespace PokerTracker.Tests.DAL.Repositories
{
    public abstract class RepositoryTests<T>
    {
        protected readonly List<T> DaoList = new List<T>();
        protected readonly DatabaseFactoryMock DbFactMock = new DatabaseFactoryMock();
        protected readonly DatabaseWrapperMock DbWrapperMock = new DatabaseWrapperMock();

        protected RepositoryTests()
        {
            DbWrapperMock.AddList(DaoList);
            DbFactMock.Database = DbWrapperMock.Object;
        }
    }
}
