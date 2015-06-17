using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using SuiteAccount.SqlModel.Dtos;
using SuiteAccount.SqlModel.Persistence.Mappings;

namespace SuiteAccount.SqlModel.Persistence.Facade
{
    public class SqlModelFacade : DbContext
    {
        static SqlModelFacade()
        {
            Database.SetInitializer<SqlModelFacade>(null);
        }

        public SqlModelFacade()
            : base("Name=QueryContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SqlModelFacade>());
        }

        public DbSet<DtoAccount> DtoAccount { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AccountMap());
        }
    }
}
