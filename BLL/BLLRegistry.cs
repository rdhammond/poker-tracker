using PokerTracker.BLL.Services;

namespace PokerTracker.BLL
{
    public class BLLRegistry : StructureMap.Registry
    {
        public BLLRegistry()
        {
            For<IGamesService>().Use<GamesService>();
            For<ICardRoomsService>().Use<CardRoomsService>();
            For<ISessionService>().Use<SessionService>();
        }
    }
}
