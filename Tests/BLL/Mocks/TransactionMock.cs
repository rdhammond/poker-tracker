using AsyncPoco;
using Moq;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class TransactionMock : Mock<ITransaction>
    {
        private bool _isCompleted, _isDisposed;

        public bool IsCompleted {  get { return _isCompleted; } }
        public bool IsDisposed {  get { return _isDisposed; } }

        public TransactionMock()
        {
            Setup(x => x.Complete()).Callback(() => _isCompleted = true);
            Setup(x => x.Dispose()).Callback(() => _isDisposed = true);
        }
    }
}