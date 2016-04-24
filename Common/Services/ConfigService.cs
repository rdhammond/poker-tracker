using PokerTracker.Common.Proxies;
using System.Configuration;

namespace PokerTracker.Common.Services
{
    public interface IConfigService
    {
        string ConnectionString { get; }
    }

    public class ConfigService : IConfigService
    {
        public const string CONN_STR_NAME = "PokerTracker";

        public string ConnectionString { get; private set; }

        public ConfigService(IConfigurationManagerProxy configManProxy)
        {
            ConnectionString = configManProxy.ConnectionStrings[CONN_STR_NAME].ConnectionString;
        }
    }
}
