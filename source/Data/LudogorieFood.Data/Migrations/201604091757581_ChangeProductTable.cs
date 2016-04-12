namespace LudogorieFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Amount");
        }
    }
}
