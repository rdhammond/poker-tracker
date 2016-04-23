using PokerTracker.Common;
using PokerTracker.DAL.Services;

namespace PokerTracker.DAL.Factories
{
    public interface ISessionServiceFactory
    {
        ISessionService Create();
        ISessionService Create(string connectionString);
    }

    public class SessionServiceFactory : ISessionServiceFactory
    {
        private readonly string _connectionString;

        public SessionServiceFactory(IConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        public ISessionService Create()
        {
            return Create(_connectionString);
        }

        public ISessionService Create(string connectionString)
        {
            return new SessionService(connectionString);
        }
    }
}
