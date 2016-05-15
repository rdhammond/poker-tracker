using Moq;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;
using System.Data.Common;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DbConnectionFactoryMock<T> : Mock<IDbConnectionFactory>
        where T : IDao
    {
        public DbConnectionFactoryMock(DbConnection connection)
        {
            Setup(x => x.Create()).Returns(connection);
        }
    }
}
