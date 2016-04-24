using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using PokerTracker.Common.Services;
using PokerTracker.Tests.Common.Mocks;

namespace PokerTracker.Tests.Common.Service
{
    [TestClass]
    public class ConfigServiceTests
    {
        private const string
            BAD_CONN_STR_NAME = "FailStr",
            CONNECTION_STRING = "Data Source=(localdb);Integrated SSPI=true;";

        private static readonly ConfigurationManagerProxyMock ConManProxyMock =
            new ConfigurationManagerProxyMock();

        [TestCleanup]
        public void TestCleanup()
        {
            ConManProxyMock.ConnectionStrings.Clear();
        }

        private static void AddConnectionString(string connStrName, string connStr)
        {
            ConManProxyMock.ConnectionStrings.Add(
                new ConnectionStringSettings(connStrName, connStr)
            );
        }

        private static IConfigService CreateConfigService()
        {
            return new ConfigService(ConManProxyMock.Object);
        }

        [TestMethod]
        public void ConnectionString_Works()
        {
            AddConnectionString(ConfigService.CONN_STR_NAME, CONNECTION_STRING);

            var configSvc = new ConfigService(ConManProxyMock.Object);
            Assert.AreEqual(CONNECTION_STRING, configSvc.ConnectionString);
        }
    }
}
