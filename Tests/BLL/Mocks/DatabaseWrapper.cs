using AsyncPoco;
using Moq;
using PokerTracker.DAL.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class DatabaseWrapperMock : Mock<IDatabaseWrapper>
    {
        public Stack<TransactionMock> Transactions = new Stack<TransactionMock>();

        public bool AllTransactionComplete
        {
            get
            {
                return Transactions.All(x => x.IsComplete);
            }
        }

        public bool AllTransactionsDisposed
        {
            get
            {
                return Transactions.All(x => x.IsDisposed);
            }
        }
        
        public DatabaseWrapperMock()
        {
            Setup(x => x.GetTransactionAsync())
                .Returns(() => GetTransactionAsync());
        }

        private async Task<ITransaction> GetTransactionAsync()
        {
            return await Task.Run(() =>
            {
                var transaction = new TransactionMock();
                Transactions.Push(transaction);
                return transaction.Object;
            });
        }
    }
}
