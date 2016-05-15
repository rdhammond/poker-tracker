using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.Factories;
using PokerTracker.Tests.DAL.Mocks;
using System.Configuration;
using System.Data.SqlClient;

namespace PokerTracker.Tests.DAL.Factories
{
    [TestClass]
    public class DbConnectionFactoryTests
    {
        private ConfigurationServiceMock _configSvcMock;
        private IDbConnectionFactory _dbConnFact;

        [TestInitialize]
        public void SetUp()
        {
            _configSvcMock = new ConfigurationServiceMock();
            _dbConnFact = new DbConnectionFactory(_configSvcMock.Object);
        }

        [TestMethod]
        public void Create_MSSql_Works()
        {
            var config = ConfigurationManager.ConnectionStrings["SqlClientTest"];
            _configSvcMock.ProviderName = config.ProviderName;
            _configSvcMock.ConnectionString = config.ConnectionString;

            using (var connection = _dbConnFact.Create())
            {
                Assert.IsNotNull(connection);
                Assert.IsInstanceOfType(connection, typeof(SqlConnection));
            }
        }

        [TestMethod]
        public void UnknownType_Throws()
        {
            _configSvcMock.ProviderName = "lol dunno";
            _configSvcMock.ConnectionString = "";

            AssertHelper.Throws(() =>
            {
                using (_dbConnFact.Create())
                { }
            });
        }
    }
}
