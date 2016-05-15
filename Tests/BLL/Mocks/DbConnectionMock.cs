using Moq;
using System.Data;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class DbConnectionMock : Mock<IDbConnection>
    {
        public DbConnectionMock()
        {
            Setup(x => x.Dispose());
        }
    }
}
