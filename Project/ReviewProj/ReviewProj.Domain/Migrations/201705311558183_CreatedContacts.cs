namespace ReviewProj.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedContacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        EmailOrPhone = c.String(nullable: false),
                        Enterprise_EntId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Enterprises", t => t.Enterprise_EntId, cascadeDelete: true)
                .Index(t => t.Enterprise_EntId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Enterprise_EntId", "dbo.Enterprises");
            DropIndex("dbo.Contacts", new[] { "Enterprise_EntId" });
            DropTable("dbo.Contacts");
        }
    }
}
