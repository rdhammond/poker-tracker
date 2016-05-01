using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class CardRoomRepositoryMock
        : ReadOnlyRepositoryMock<ICardRoomRepository, CardRoomDao>
    { }
}
