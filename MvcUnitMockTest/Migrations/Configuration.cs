namespace MvcUnitMockTest.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MvcUnitMockTest.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcUnitMockTest.DataAccess.BankContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MvcUnitMockTest.DataAccess.BankContext";
        }

        protected override void Seed(MvcUnitMockTest.DataAccess.BankContext context)
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

            context.Accounts.AddOrUpdate(
              new Account { Id = 1, Number = "123456-1", Name = "Betalkonto", Sum = 0, Type = "-", Locked = false },
              new Account { Id = 2, Number = "123456-2", Name = "Lönekonto", Sum = 0, Type = "-", Locked = false },
              new Account { Id = 3, Number = "123456-3", Name = "Sparkonto 1", Sum = 0, Type = "-", Locked = false },
              new Account { Id = 4, Number = "123456-4", Name = "Sparkonto 2", Sum = 0, Type = "-", Locked = false }
            );

            context.Transfers.AddOrUpdate(
              new Transfer { Id = 1, IdFrom = 1, IdTo = 2, Sum = 123, Time = DateTime.Now },
              new Transfer { Id = 2, IdFrom = 2, IdTo = 1, Sum = 123, Time = DateTime.Now },
              new Transfer { Id = 3, IdFrom = 1, IdTo = 4, Sum = 123, Time = DateTime.Now },
              new Transfer { Id = 4, IdFrom = 1, IdTo = 3, Sum = 123, Time = DateTime.Now }
            );
            context.SaveChanges();
        }
    }
}
