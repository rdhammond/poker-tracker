using System.Configuration;

namespace PokerTracker.Common.Services
{
    public interface IConfigService
    {
        string ConnectionString { get; }
    }

    public class ConfigService : IConfigService
    {
        private const string CONN_STR_NAME = "PokerTracker";

        public string ConnectionString { get; private set; }

        public ConfigService()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[CONN_STR_NAME].ConnectionString;
        }
    }
}
