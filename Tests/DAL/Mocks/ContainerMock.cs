using Moq;
using StructureMap;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class ContainerMock<T> : Mock<IContainer>
    {
        public ContainerMock(T intf)
        {
            Setup(x => x.GetInstance<T>()).Returns(intf);
        }
    }
}
