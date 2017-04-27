namespace ReviewProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnterpriseDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enterprises", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enterprises", "Description");
        }
    }
}
