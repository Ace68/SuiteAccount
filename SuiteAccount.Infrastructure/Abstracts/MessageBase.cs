using System;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public abstract class MessageBase : IMessageBase
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}
