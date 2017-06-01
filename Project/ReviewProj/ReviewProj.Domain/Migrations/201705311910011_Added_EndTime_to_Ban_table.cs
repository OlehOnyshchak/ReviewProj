namespace ReviewProj.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_EndTime_to_Ban_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bans", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bans", "BanDuration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bans", "BanDuration", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Bans", "EndTime");
        }
    }
}
