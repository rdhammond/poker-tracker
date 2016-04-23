using System.Configuration;

namespace PokerTracker.Common
{
    public interface IConfig
    {
        string ConnectionString { get; }
    }

    public class Config : IConfig
    {
        private const string CONN_STR_NAME = "PokerTracker";

        public string ConnectionString { get; private set; }

        public Config()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[CONN_STR_NAME].ConnectionString;
        }
    }
}
