using Ninject.Modules;
using SuiteAccount.NoSql.Model.Abstracts;
using SuiteAccount.NoSql.Persistence.Persistors;
using SuiteAccount.NoSql.Services.Abstracts;
using SuiteAccount.NoSql.Services.Concretes;

namespace SuiteAccount.Infrastructures.Modules
{
    public class QueryNoSqlModule : NinjectModule
    {
        public override void Load()
        {

            this.Bind<INoSqlUnitOfWork>().To<NoSqlUnitOfWork>().InSingletonScope();

            this.Bind<INoSqlAccountService>().To<NoSqlAccountService>().InSingletonScope();
            this.Bind<INoSqlSuiteTokenService>().To<NoSqlSuiteTokenService>().InSingletonScope();
        }
    }
}
