using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructure.Abstracts
{
    public interface IEventBus
    {
        void Publish(EventBase @event);
    }
}
