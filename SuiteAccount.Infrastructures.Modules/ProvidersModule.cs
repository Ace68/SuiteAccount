using Ninject.Modules;
using SuiteAccount.Providers;
using SuiteAccount.Providers.Abstracts;
using SuiteAccount.Providers.Concretes;

namespace SuiteAccount.Infrastructures.Modules
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAccountProvider>().To<AccountProvider>().InSingletonScope();
            this.Bind<ISuiteTokenProvider>().To<SuiteTokenProvider>().InSingletonScope();
        }
    }
}
