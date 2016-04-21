using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Config
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
