namespace PokerTracker.Common
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<IConfig>().Use<Config>().Singleton();
        }
    }
}
