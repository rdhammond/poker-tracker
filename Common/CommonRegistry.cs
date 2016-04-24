using PokerTracker.Common.Services;
using StructureMap;

namespace PokerTracker.Common
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IConfigService>().Use<ConfigService>().Singleton();
        }
    }
}
