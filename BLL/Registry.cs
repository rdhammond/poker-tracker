namespace PokerTracker.BLL
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<ISessionManager>().Use<SessionManager>();
        }
    }
}
