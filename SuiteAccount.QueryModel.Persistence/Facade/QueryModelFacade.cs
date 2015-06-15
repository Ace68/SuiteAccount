using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using SuiteAccount.QueryModel.Dtos;
using SuiteAccount.QueryModel.Persistence.Mappings;

namespace SuiteAccount.QueryModel.Persistence.Facade
{
    public class QueryModelFacade : DbContext
    {
        static QueryModelFacade()
        {
            Database.SetInitializer<QueryModelFacade>(null);
        }

        public QueryModelFacade()
            : base("Name=QueryContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<QueryModelFacade>());
        }

        public DbSet<DtoAccount> DtoAccount { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AccountMap());
        }
    }
}
