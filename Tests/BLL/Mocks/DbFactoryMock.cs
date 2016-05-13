using Moq;
using PokerTracker.DAL.Factories;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class DatabaseFactoryMock : Mock<IDatabaseFactory>
    {
        private readonly DatabaseWrapperMock _databaseWrapperMock =
            new DatabaseWrapperMock();

        public DatabaseWrapperMock DatabaseWrapperMock
        {
            get { return _databaseWrapperMock; }
        }

        public DatabaseFactoryMock()
        {
            Setup(x => x.CreateAsync())
                .Returns(() => Task.FromResult(DatabaseWrapperMock.Object));
        }
    }
}
