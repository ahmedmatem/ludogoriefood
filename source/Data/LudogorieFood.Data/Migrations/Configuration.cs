namespace LudogorieFood.Data.Migrations
{

    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<LudogorieFood.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
            ContextKey = "LudogorieFood.Data.ApplicationDbContext";
        }
    }
}
