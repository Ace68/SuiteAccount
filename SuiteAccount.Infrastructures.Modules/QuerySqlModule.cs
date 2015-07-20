using Ninject.Modules;

using SuiteAccount.SqlModel.Persistence.Persistors;
using SuiteAccount.SqlModel.Persistors;
using SuiteAccount.SqlModel.Services.Abstracts;
using SuiteAccount.SqlModel.Services.Concretes;

namespace SuiteAccount.Infrastructures.Modules
{
    public class QuerySqlModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            this.Bind<ISqlAccountService>().To<SqlAccountService>().InSingletonScope();
            this.Bind<ISqlSuiteTokenService>().To<SqlSuiteTokenService>().InSingletonScope();
        }
    }
}
