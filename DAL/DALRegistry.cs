using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Repositories;
using StructureMap;

namespace PokerTracker.DAL
{
    public class DALRegistry : Registry
    {
        public DALRegistry()
        {
            For<IDatabaseFactory>().Use<DatabaseFactory>().Singleton();
            For<IGameRepository>().Use<GameRepository>().Singleton();
            For<ICardRoomRepository>().Use<CardRoomRepository>().Singleton();
            For<ISummaryRepository>().Use<SummaryRepository>().Singleton();
            For<ISessionRepository>().Use<SessionRepository>().Singleton();
            For<ITimeEntryRepository>().Use<TimeEntryRepository>().Singleton();
        }
    }
}
