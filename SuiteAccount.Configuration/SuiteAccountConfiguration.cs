using System.Configuration;

namespace SuiteAccount.Configuration
{
    public static class SuiteAccountConfiguration
    {
        private const string SuiteAccountGroupName = "SuiteAccount/";

        private static EventStoreSectionHandler _eventStoreSection;
        public static EventStoreSectionHandler EventStoreSection
        {
            get
            {
                return _eventStoreSection ??
                       (_eventStoreSection =
                           ConfigurationManager.GetSection(SuiteAccountGroupName + "EventStore") as
                               EventStoreSectionHandler);
            }
        }

        private static MongoDbSectionHandler _mongoDbSection;
        public static MongoDbSectionHandler MongoDbSection
        {
            get
            {
                return _mongoDbSection ??
                       (_mongoDbSection =
                           ConfigurationManager.GetSection(SuiteAccountGroupName + "MongoDb") as MongoDbSectionHandler);
            }
        }
    }
}
