using PokerTracker.BLL.Services;

namespace PokerTracker.BLL
{
    public class BLLRegistry : StructureMap.Registry
    {
        public BLLRegistry()
        {
            For<ICardRoomsService>().Use<CardRoomsService>();
            For<IGamesService>().Use<GamesService>();
            For<ISessionService>().Use<SessionService>();
            For<ISummaryService>().Use<SummaryService>();
        }
    }
}
