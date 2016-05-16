using PokerTracker.DAL.Databases;
using PokerTracker.DAL.Factories;
using PokerTracker.DAL.Repositories;
using StructureMap;

namespace PokerTracker.DAL
{
    public class DALRegistry : Registry
    {
        public DALRegistry()
        {
            IncludeRegistry<BaseRegistry>();

            For<IDatabase>().Use<Database>().Singleton();
            For<IDbConnectionFactory>().Use<DbConnectionFactory>().Singleton();
            For<ICardRoomRepository>().Use<CardRoomRepository>().Singleton();
            For<IGameRepository>().Use<GameRepository>().Singleton();
            For<ISessionRepository>().Use<SessionRepository>().Singleton();
            For<ISummaryRepository>().Use<SummaryRepository>().Singleton();
            For<ITimeEntryRepository>().Use<TimeEntryRepository>().Singleton();
            For<IStatisticsRepository>().Use<StatisticsRepository>().Singleton();
        }
    }
}
