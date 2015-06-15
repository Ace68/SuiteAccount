namespace SuiteAccount.Infrastructure.Core
{
    public static class EventStoreCore
    {
        public const string EventClrTypeHeader = "SuiteEventName";             // EventClrTypeName
        public const string AggregateClrTypeHeader = "SuiteAggregateName";     // AggregateClrTypeName
        public const string CommitIdHeader = "CommitId";
        public const int WritePageSize = 500;
        public const int ReadPageSize = 500;
    }
}
