namespace LudogorieFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TotalRating", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "OverallRating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OverallRating");
            DropColumn("dbo.Products", "TotalRating");
        }
    }
}
