using NUnit.Framework;

namespace SuiteAccount.Configuration.Test
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        public void GetEventStoreUri()
        {
            Assert.AreEqual("127.0.0.1", SuiteAccountConfiguration.EventStoreSection.Uri);
        }
    }
}
