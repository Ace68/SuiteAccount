using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : EventBase;
    }
}
