using PokerTracker.Services;
using StructureMap;

namespace PokerTracker
{
    public class BaseRegistry : Registry
    {
        public BaseRegistry()
        {
            For<IConfigurationService>().Use<ConfigurationService>().Singleton();
        }
    }
}
