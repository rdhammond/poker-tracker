using System.Configuration;

namespace PokerTracker.Services
{
    public interface IConfigurationService
    {
        string ProviderName { get; }
        string ConnectionString { get; }
    }

    public class ConfigurationService : IConfigurationService
    {
        public const string CONN_STR_NAME = "PokerTracker";

        private readonly string _providerName;
        private readonly string _connectionString;

        public string ProviderName
        {
            get { return _providerName; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public ConfigurationService(string connStrName = CONN_STR_NAME)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connStrName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException(string.Format(
                    "app.config is missing a connectionString node named '{0}'.",
                    CONN_STR_NAME
                ));
            }

            _providerName = connectionString.ProviderName;
            _connectionString = connectionString.ConnectionString;
        }
    }
}
