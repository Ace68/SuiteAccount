using Ninject.Modules;
using SuiteAccount.Infrastructure.Providers;
using SuiteAccount.Providers;

namespace SuiteAccount.Infrastructures.Modules
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountProvider>().To<AccountProvider>().InSingletonScope();
        }
    }
}
