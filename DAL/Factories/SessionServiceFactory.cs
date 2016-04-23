using PokerTracker.Config;
using PokerTracker.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Factories
{
    public interface ISessionServiceFactory
    {
        ISessionService Create();
        ISessionService Create(string connectionString);
    }

    public class SessionServiceFactory : ISessionServiceFactory
    {
        private readonly IConfig Config;

        public ISessionService Create()
        {
            return Create(Config.ConnectionString);
        }

        public ISessionService Create(string connectionString)
        {
            return new SessionService(connectionString);
        }
    }
}
