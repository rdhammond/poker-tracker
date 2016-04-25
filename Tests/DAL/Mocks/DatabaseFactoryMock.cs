using Moq;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Wrappers;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DatabaseFactoryMock : Mock<IDatabaseFactory>
    {
        public IDatabaseWrapper Database { get; set; }

        public DatabaseFactoryMock()
        {
            Setup(x => x.Create()).Returns(() => Database);
        }
    }
}
