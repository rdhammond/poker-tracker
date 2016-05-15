using PokerTracker.Services;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace PokerTracker.DAL.Factories
{
    public interface IDbConnectionFactory
    {
        DbConnection Create();
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfigurationService _configSvc;

        public DbConnectionFactory(IConfigurationService configSvc)
        {
            _configSvc = configSvc;
        }

        public DbConnection Create()
        {
            DbConnection connection;

            switch (_configSvc.ProviderName)
            {
                case "System.Data.SqlClient":
                    connection = new SqlConnection(_configSvc.ConnectionString);
                    break;

                default:
                    throw new ArgumentException(string.Format(
                        "Unknown database provider type '{0}' from config file.",
                        _configSvc.ProviderName
                    ));
            }

            return connection;
        }
    }
}
