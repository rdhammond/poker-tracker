using Moq;
using PokerTracker.BLL.Objects;
using PokerTracker.BLL.Services;
using System;
using System.Threading.Tasks;

namespace PokerTracker.Tests.WCF.Mocks
{
    public class SessionServiceMock : Mock<ISessionService>
    {
        public SessionServiceMock()
        {
            Setup(x => x.SaveSessionAsync(It.IsAny<Session>()))
                .Returns(() => Task.Delay(1))
                .Verifiable();
        }
    }
}
