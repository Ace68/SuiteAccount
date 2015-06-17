using Ninject.Modules;

using SuiteAccount.SqlModel.Denormalizer.Abstracts;
using SuiteAccount.SqlModel.Denormalizer.Concretes;
using SuiteAccount.SqlModel.Persistence.Persistors;
using SuiteAccount.SqlModel.Persistors;

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
