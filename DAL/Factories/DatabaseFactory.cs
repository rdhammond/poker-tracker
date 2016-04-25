using PokerTracker.DAL.Wrappers;

namespace PokerTracker.DAL.Factories
{
    public interface IDatabaseFactory
    {
        IDatabaseWrapper Create();
    }

    public class DatabaseFactory : IDatabaseFactory
    {
        public const string CONN_STR_NAME = "PokerTracker";

        public IDatabaseWrapper Create()
        {
            return new DatabaseWrapper(CONN_STR_NAME);
        }
    }
}
