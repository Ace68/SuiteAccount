using Ninject.Modules;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.Infrastructure.Concretes;

namespace SuiteAccount.Infrastructures.Modules
{
    public class ServiceBusModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServiceBus, IEventBus>().To<SuiteServiceBus>().InSingletonScope();
        }
    }
}
