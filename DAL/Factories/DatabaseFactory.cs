using AsyncPoco;
using PokerTracker.Common.Services;

namespace PokerTracker.DAL.Factories
{
    public interface IDatabaseFactory
    {
        Database Create();
    }
    
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IConfigService Config;

        public DatabaseFactory(IConfigService config)
        {
            Config = config;
        }

        public Database Create()
        {
            return new Database(Config.ConnectionString);
        }
    }
}
