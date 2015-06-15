using Ninject.Modules;

using SuiteAccount.QueryModel.Denormalizer.Abstracts;
using SuiteAccount.QueryModel.Denormalizer.Concretes;
using SuiteAccount.QueryModel.Persistence.Persistors;
using SuiteAccount.QueryModel.Persistors;

namespace SuiteAccount.Infrastructures.Modules
{
    public class QueryModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            Bind<IAccountDenormalizer>().To<AccountDenormalizer>().InSingletonScope();
        }
    }
}
