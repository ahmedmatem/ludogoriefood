namespace LudogorieFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PictureName", c => c.String());
            AddColumn("dbo.Products", "Measure", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "NewPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "InStock", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "InStock");
            DropColumn("dbo.Products", "NewPrice");
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Products", "Measure");
            DropColumn("dbo.Products", "PictureName");
        }
    }
}
