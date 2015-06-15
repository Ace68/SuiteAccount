using System;
using SuiteAccount.QueryModel.Dtos;

namespace SuiteAccount.QueryModel.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Facade.QueryModelFacade>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Facade.QueryModelFacade context)
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
