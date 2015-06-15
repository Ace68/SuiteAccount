using System;
using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IServiceBus
    {
        void Send<T>(T command) where T : CommandBase;
        void RegisterHandler<T>(Action<T> handler) where T : MessageBase;
    }
}
