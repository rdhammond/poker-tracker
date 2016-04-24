using Moq;
using PokerTracker.Common.Proxies;
using System.Configuration;

namespace PokerTracker.Tests.Common.Mocks
{
    public class ConfigurationManagerProxyMock : Mock<IConfigurationManagerProxy>
    {
        public readonly ConnectionStringSettingsCollection ConnectionStrings =
            new ConnectionStringSettingsCollection();
        
        protected override object OnGetObject()
        {
            return Of<IConfigurationManagerProxy>(l =>
                l.ConnectionStrings == ConnectionStrings
            );
        }
    }
}
