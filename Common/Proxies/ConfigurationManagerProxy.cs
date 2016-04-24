using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Common.Proxies
{
    public interface IConfigurationManagerProxy
    {
        ConnectionStringSettingsCollection ConnectionStrings { get; }
    }

    public class ConfigurationManagerProxy : IConfigurationManagerProxy
    {
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }
    }
}
