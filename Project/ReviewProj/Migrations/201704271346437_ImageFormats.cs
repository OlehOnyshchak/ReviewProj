namespace ReviewProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFormats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviewers", "AvatarFormat", c => c.String(nullable: false));
            AddColumn("dbo.Resources", "Format", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resources", "Format");
            DropColumn("dbo.Reviewers", "AvatarFormat");
        }
    }
}
