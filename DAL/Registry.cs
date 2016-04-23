using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Services;

namespace PokerTracker.DAL
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<ISessionServiceFactory>().Use<SessionServiceFactory>().Singleton();
            For<ISummaryService>().Use<SummaryService>();
            For<IIdService>().Use<IdService>();
        }
    }
}
