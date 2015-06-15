using System;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IMessageBase
    {
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}
