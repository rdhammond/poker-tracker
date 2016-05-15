using Moq;
using PokerTracker.Services;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class ConfigurationServiceMock : Mock<IConfigurationService>
    {
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }

        public ConfigurationServiceMock()
        {
            SetupGet(x => x.ProviderName).Returns(() => ProviderName);
            SetupGet(x => x.ConnectionString).Returns(() => ConnectionString);
        }
    }
}
