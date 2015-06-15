using Ninject.Modules;
using SuiteAccount.Domain.Events;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.QueryModel.Denormalizer.DomainEventsHandlers;

namespace SuiteAccount.Infrastructures.Modules
{
    public class EventHandlersModule : NinjectModule
    {
        public override void Load()
        {
            #region Account
            Bind<IEventHandler<AccountCreated>>().To<AccountEventsHandler>().InSingletonScope();
            Bind<IEventHandler<EmailUpdated>>().To<AccountEventsHandler>().InSingletonScope();
            #endregion
        }
    }
}
