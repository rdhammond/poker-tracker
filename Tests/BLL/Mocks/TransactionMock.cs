using AsyncPoco;
using Moq;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class TransactionMock : Mock<ITransaction>
    {
        public bool IsComplete, IsDisposed;

        public TransactionMock()
        {
            Setup(x => x.Complete()).Callback(() => IsComplete = true);
            Setup(x => x.Dispose()).Callback(() => IsDisposed = true);
        }
    }
}
