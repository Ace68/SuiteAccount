using NUnit.Framework;

namespace SuiteAccount.Configuration.Test
{
    [TestFixture]
    public class SuiteTokenConfigurationTest
    {
        [Test]
        public void CheckTokenDuration()
        {
            Assert.AreEqual(240, SuiteAccountConfiguration.SuiteTokenSection.Duration);
        }
    }
}
