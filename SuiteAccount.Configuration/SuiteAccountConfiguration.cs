using System;
using System.Configuration;

namespace SuiteAccount.Configuration
{
    public static class SuiteAccountConfiguration
    {
        private const string SuiteAccountGroupName = "SuiteAccount/";

        private static string _queryContext;
        public static string QueryContext
        {
            get
            {
                if (String.IsNullOrEmpty(_queryContext))
                    _queryContext = ConfigurationManager.ConnectionStrings["QueryContext"].ConnectionString;

                return _queryContext;
            }
        }

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

        private static SuiteTokenSectionHandler _suiteTokenSection;
        public static SuiteTokenSectionHandler SuiteTokenSection
        {
            get
            {
                return _suiteTokenSection ??
                       (_suiteTokenSection =
                           ConfigurationManager.GetSection(SuiteAccountGroupName + "SuiteToken") as
                               SuiteTokenSectionHandler);
            }
        }
    }
}
