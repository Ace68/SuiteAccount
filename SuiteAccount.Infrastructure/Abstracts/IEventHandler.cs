using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IEventHandler<in T> where T : EventBase
    {
        void Handle(T @event);
    }
}
