namespace Qx.Scsxxt.Extentsion.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Qx.Scsxxt.Extentsion.Entity.MyContext>
    {
        //http://www.cnblogs.com/zuqing/p/5452440.html
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Qx.Scsxxt.Extentsion.Entity.MyContext context)
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
        }
    }
}
