using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using System.Transactions;
using PokerTracker.DAL.DAO;
using System.Threading;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DbConnectionMock<T> : Mock<DbConnection>
        where T : IDao
    {
        public DbConnectionMock()
        {
            Setup(x => x.OpenAsync(It.Is<CancellationToken>(o => o == CancellationToken.None))).Returns(() => Task.FromResult(true));
            Setup(x => x.EnlistTransaction(It.IsAny<Transaction>())).Verifiable();
        }
    }
}
