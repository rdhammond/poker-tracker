using PokerTracker.BLL.Objects;
using PokerTracker.BLL.Services;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.Tests.WCF.Mocks
{
    public class GamesServiceMock
        : LookupServiceMock<
            IGamesService,
            GameDao,
            Game,
            IGameRepository
        >
    { }
}
