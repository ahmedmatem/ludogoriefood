namespace LudogorieFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectSlidesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slides", "PictureName", c => c.String());
            AddColumn("dbo.Slides", "NextPicture", c => c.Int());
            AddColumn("dbo.Slides", "PrevPicture", c => c.Int());
            DropColumn("dbo.Slides", "Name");
            DropColumn("dbo.Slides", "PictureUrl");
            DropColumn("dbo.Slides", "Next");
            DropColumn("dbo.Slides", "Prev");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slides", "Prev", c => c.Int());
            AddColumn("dbo.Slides", "Next", c => c.Int());
            AddColumn("dbo.Slides", "PictureUrl", c => c.String());
            AddColumn("dbo.Slides", "Name", c => c.String());
            DropColumn("dbo.Slides", "PrevPicture");
            DropColumn("dbo.Slides", "NextPicture");
            DropColumn("dbo.Slides", "PictureName");
        }
    }
}
