using NUnit.Framework;

namespace SuiteAccount.Configuration.Test
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        public void GetQueryContextConnectionString()
        {
            Assert.AreEqual(@"Data Source=.\SQLEXPRESS;Initial Catalog=SuiteAccount;Integrated Security=SSPI", SuiteAccountConfiguration.QueryContext);
        }
    }
}
