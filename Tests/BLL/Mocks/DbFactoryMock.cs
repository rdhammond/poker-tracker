using Moq;
using PokerTracker.DAL.Factories;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class DatabaseFactoryMock : Mock<IDatabaseFactory>
    {
        public readonly DatabaseWrapperMock DatabaseWrapperMock = new DatabaseWrapperMock();

        public DatabaseFactoryMock()
        {
            Setup(x => x.Create()).Returns(() => DatabaseWrapperMock.Object);
        }
    }
}
