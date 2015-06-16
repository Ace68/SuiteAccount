using Ninject.Modules;
using SuiteAccount.NoSql.Persistence.Abstracts;
using SuiteAccount.NoSql.Persistence.Entities;
using SuiteAccount.NoSql.Persistence.Repositories;

namespace SuiteAccount.Infrastructures.Modules
{
    public class NoSqlModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDocumentRepository<NoSqlAccount>>().To<AccountRepository>().InSingletonScope();
        }
    }
}
