using System;
using SuiteAccount.SqlModel.Dtos;

namespace SuiteAccount.SqlModel.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Facade.SqlModelFacade>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Facade.SqlModelFacade context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.DtoAccount.AddOrUpdate(
                p => p.UserName,
                new DtoAccount { Id = Guid.NewGuid(), UserName = "Admin", Password = "InfoCopy"}
            );
        }
    }
}
