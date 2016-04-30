using PokerTracker.BLL;
using StructureMap;

namespace PokerTracker.WCF
{
    public class WCFRegistry : Registry
    {
        public WCFRegistry()
        {
            IncludeRegistry<BLLRegistry>();
        }
    }
}