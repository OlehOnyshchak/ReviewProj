namespace ReviewProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shrek : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Bans",
                c => new
                    {
                        BanId = c.Int(nullable: false, identity: true),
                        AdminId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        BanDuration = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.BanId)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AdminId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EnterpriseContacts",
                c => new
                    {
                        EntId = c.Int(nullable: false),
                        Contact = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.EntId, t.Contact })
                .ForeignKey("dbo.Enterprises", t => t.EntId, cascadeDelete: true)
                .Index(t => t.EntId);
            
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        EntId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 128),
                        Name = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        Rating = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntId)
                .ForeignKey("dbo.Owners", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsBanned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EnterpriseImages",
                c => new
                    {
                        EntImId = c.Int(nullable: false, identity: true),
                        EntId = c.Int(nullable: false),
                        MyImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntImId)
                .ForeignKey("dbo.Enterprises", t => t.EntId, cascadeDelete: true)
                .ForeignKey("dbo.MyImages", t => t.MyImageId, cascadeDelete: true)
                .Index(t => t.EntId)
                .Index(t => t.MyImageId);
            
            CreateTable(
                "dbo.MyImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Image = c.Binary(storeType: "image"),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.OwnerEnterprises",
                c => new
                    {
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        EntId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerId, t.EntId })
                .ForeignKey("dbo.Enterprises", t => t.EntId, cascadeDelete: true)
                .ForeignKey("dbo.Owners", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.EntId);
            
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsBanned = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Nationality = c.String(),
                        Rating = c.Double(nullable: false),
                        MyImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.MyImages", t => t.MyImageId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MyImageId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ReviewerId = c.String(maxLength: 128),
                        EntId = c.Int(nullable: false),
                        Mark = c.Double(nullable: false),
                        Description = c.String(),
                        TotalRating = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Enterprises", t => t.EntId, cascadeDelete: true)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.EntId);
            
            CreateTable(
                "dbo.ReviewVoters",
                c => new
                    {
                        ReviewId = c.Int(nullable: false),
                        VoterId = c.String(nullable: false, maxLength: 128),
                        VoteDelta = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReviewId, t.VoterId })
                .ForeignKey("dbo.Reviews", t => t.ReviewId, cascadeDelete: true)
                .ForeignKey("dbo.Reviewers", t => t.VoterId, cascadeDelete: true)
                .Index(t => t.ReviewId)
                .Index(t => t.VoterId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReviewVoters", "VoterId", "dbo.Reviewers");
            DropForeignKey("dbo.ReviewVoters", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.Reviews", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.Reviews", "EntId", "dbo.Enterprises");
            DropForeignKey("dbo.Reviewers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviewers", "MyImageId", "dbo.MyImages");
            DropForeignKey("dbo.OwnerEnterprises", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.OwnerEnterprises", "EntId", "dbo.Enterprises");
            DropForeignKey("dbo.EnterpriseImages", "MyImageId", "dbo.MyImages");
            DropForeignKey("dbo.EnterpriseImages", "EntId", "dbo.Enterprises");
            DropForeignKey("dbo.EnterpriseContacts", "EntId", "dbo.Enterprises");
            DropForeignKey("dbo.Enterprises", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Owners", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bans", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bans", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Admins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReviewVoters", new[] { "VoterId" });
            DropIndex("dbo.ReviewVoters", new[] { "ReviewId" });
            DropIndex("dbo.Reviews", new[] { "EntId" });
            DropIndex("dbo.Reviews", new[] { "ReviewerId" });
            DropIndex("dbo.Reviewers", new[] { "MyImageId" });
            DropIndex("dbo.Reviewers", new[] { "UserId" });
            DropIndex("dbo.OwnerEnterprises", new[] { "EntId" });
            DropIndex("dbo.OwnerEnterprises", new[] { "OwnerId" });
            DropIndex("dbo.EnterpriseImages", new[] { "MyImageId" });
            DropIndex("dbo.EnterpriseImages", new[] { "EntId" });
            DropIndex("dbo.Owners", new[] { "UserId" });
            DropIndex("dbo.Enterprises", new[] { "OwnerId" });
            DropIndex("dbo.EnterpriseContacts", new[] { "EntId" });
            DropIndex("dbo.Bans", new[] { "UserId" });
            DropIndex("dbo.Bans", new[] { "AdminId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReviewVoters");
            DropTable("dbo.Reviews");
            DropTable("dbo.Reviewers");
            DropTable("dbo.OwnerEnterprises");
            DropTable("dbo.MyImages");
            DropTable("dbo.EnterpriseImages");
            DropTable("dbo.Owners");
            DropTable("dbo.Enterprises");
            DropTable("dbo.EnterpriseContacts");
            DropTable("dbo.Bans");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Admins");
        }
    }
}
