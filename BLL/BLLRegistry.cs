using AutoMapper;
using PokerTracker.BLL.Services;
using PokerTracker.DAL;

namespace PokerTracker.BLL
{
    public class BLLRegistry : StructureMap.Registry
    {
        public BLLRegistry()
        {
            IncludeRegistry<DALRegistry>();

            For<ICardRoomsService>().Use<CardRoomsService>();
            For<IGamesService>().Use<GamesService>();
            For<ISessionService>().Use<SessionService>();
            For<ISummaryService>().Use<SummaryService>();

            For<IMapper>().Use(
                () => new MapperConfiguration(x => x.AddProfile<BLLProfile>())
                    .CreateMapper()
            );
        }
    }
}
