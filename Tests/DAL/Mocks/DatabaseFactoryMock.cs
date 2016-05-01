using Moq;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Wrappers;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DatabaseFactoryMock : Mock<IDatabaseFactory>
    {
        public DatabaseFactoryMock(IDatabaseWrapper database)
        {
            Setup(x => x.Create()).Returns(database);
        }
    }
}
