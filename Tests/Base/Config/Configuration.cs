using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.Services;
using System.Configuration;

namespace PokerTracker.Tests.Base.Config
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void ShouldEqualAppConfig()
        {
            var expected = ConfigurationManager.ConnectionStrings[ConfigurationService.CONN_STR_NAME];
            var actual = new ConfigurationService();

            Assert.AreEqual(expected.ConnectionString, actual.ConnectionString);
            Assert.AreEqual(expected.ProviderName, actual.ProviderName);
        }

        [TestMethod]
        public void NoConfig_ShouldThrow()
        {
            AssertHelper.Throws(() => { new ConfigurationService("DoesntExist"); });
        }
    }
}
