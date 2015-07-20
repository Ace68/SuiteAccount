using NUnit.Framework;

namespace SuiteAccount.Configuration.Test
{
    [TestFixture]
    public class EventStoreConfigurationTest
    {
        [Test]
        public void GetEventStoreUri()
        {
            Assert.AreEqual("127.0.0.1", SuiteAccountConfiguration.EventStoreSection.Uri);
        }

        [Test]
        public void GetEventStorePort()
        {
            Assert.AreEqual(2113, SuiteAccountConfiguration.EventStoreSection.Port);
        }

        [Test]
        public void GetEventStoreUser()
        {
            Assert.AreEqual("admin", SuiteAccountConfiguration.EventStoreSection.User);
        }

        [Test]
        public void GetEventStorePassword()
        {
            Assert.AreEqual("changeit", SuiteAccountConfiguration.EventStoreSection.Password);
        }
    }
}
