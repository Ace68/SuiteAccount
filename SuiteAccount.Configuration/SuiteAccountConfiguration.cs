using System.Configuration;

namespace SuiteAccount.Configuration
{
    public class SuiteAccountConfiguration
    {
        private static SuiteAccountSectionHandler _suiteAccountSection;

        public static SuiteAccountSectionHandler SuiteAccountSection
        {
            get
            {
                return _suiteAccountSection ??
                       (_suiteAccountSection = ConfigurationManager.GetSection("SuiteAccount/EventStore/Parameters") as SuiteAccountSectionHandler);
            }
        }
    }
}
