namespace ReviewProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Donkey : DbMigration
    {
        public override void Up()
        {
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
                        StartTime = c.DateTime(nullable: false),
                        BanDuration = c.Time(nullable: false, precision: 7),
                        Admin_Id = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BanId)
                .ForeignKey("dbo.Admins", t => t.Admin_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Admin_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        EntId = c.Int(nullable: false, identity: true),
                        Address_City = c.String(),
                        Address_Street = c.String(),
                        Address_HouseNumber = c.String(),
                        Name = c.String(nullable: false),
                        Rating = c.Double(nullable: false, defaultValue: 0.0),
                        Type = c.Int(nullable: false, defaultValue: 0),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EntId)
                .ForeignKey("dbo.Owners", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        Data = c.Binary(nullable: false, maxLength: 2000000000),
                        Type = c.Int(nullable: false),
                        Enterprise_EntId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResourceId)
                .ForeignKey("dbo.Enterprises", t => t.Enterprise_EntId)
                .Index(t => t.Enterprise_EntId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Mark = c.Double(nullable: false),
                        Description = c.String(),
                        TotalRating = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Reviewer_Id = c.String(nullable: false, maxLength: 128),
                        Enterprise_EntId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Reviewers", t => t.Reviewer_Id)
                .ForeignKey("dbo.Enterprises", t => t.Enterprise_EntId)
                .Index(t => t.Reviewer_Id)
                .Index(t => t.Enterprise_EntId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsBanned = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsBanned = c.Boolean(nullable: false, defaultValue: false),
                        BirthDate = c.DateTime(),
                        Nationality = c.String(),
                        Rating = c.Double(nullable: false, defaultValue: 0.0),
                        Avatar = c.Binary(maxLength: 2000000000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviewers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Owners", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Admins", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bans", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enterprises", "Owner_Id", "dbo.Owners");
            DropForeignKey("dbo.Reviews", "Enterprise_EntId", "dbo.Enterprises");
            DropForeignKey("dbo.Reviews", "Reviewer_Id", "dbo.Reviewers");
            DropForeignKey("dbo.Resources", "Enterprise_EntId", "dbo.Enterprises");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bans", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.Reviewers", new[] { "Id" });
            DropIndex("dbo.Owners", new[] { "Id" });
            DropIndex("dbo.Admins", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "Enterprise_EntId" });
            DropIndex("dbo.Reviews", new[] { "Reviewer_Id" });
            DropIndex("dbo.Resources", new[] { "Enterprise_EntId" });
            DropIndex("dbo.Enterprises", new[] { "Owner_Id" });
            DropIndex("dbo.Bans", new[] { "User_Id" });
            DropIndex("dbo.Bans", new[] { "Admin_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.Reviewers");
            DropTable("dbo.Owners");
            DropTable("dbo.Admins");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Resources");
            DropTable("dbo.Enterprises");
            DropTable("dbo.Bans");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
