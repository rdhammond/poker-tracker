using Moq;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Wrappers;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DatabaseFactoryMock : Mock<IDatabaseFactory>
    {
        public DatabaseFactoryMock(IDatabaseWrapper database)
        {
            Setup(x => x.CreateAsync()).Returns(() => Task.FromResult(database));
        }
    }
}
