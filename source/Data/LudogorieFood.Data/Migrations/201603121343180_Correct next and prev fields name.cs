namespace LudogorieFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correctnextandprevfieldsname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slides", "NextSlideId", c => c.Int());
            AddColumn("dbo.Slides", "PrevSlideId", c => c.Int());
            DropColumn("dbo.Slides", "NextPicture");
            DropColumn("dbo.Slides", "PrevPicture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slides", "PrevPicture", c => c.Int());
            AddColumn("dbo.Slides", "NextPicture", c => c.Int());
            DropColumn("dbo.Slides", "PrevSlideId");
            DropColumn("dbo.Slides", "NextSlideId");
        }
    }
}
