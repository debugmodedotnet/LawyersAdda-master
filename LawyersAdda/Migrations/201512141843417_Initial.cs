namespace LawyersAdda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 100),
                        ModifiedBy = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        CourtImageUrl = c.String(),
                        City = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lawyers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 100),
                        ModifiedBy = c.String(nullable: false, maxLength: 100),
                        ImageUrl = c.String(),
                        Bio = c.String(),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        AlternatePhoneNumber = c.String(),
                        Sex = c.String(),
                        WebSiteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 100),
                        ModifiedBy = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        isLawyer = c.Boolean(nullable: false),
                        FullName = c.String(),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.LawyerCourts",
                c => new
                    {
                        Lawyer_Id = c.String(nullable: false, maxLength: 128),
                        Court_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Lawyer_Id, t.Court_Id })
                .ForeignKey("dbo.Lawyers", t => t.Lawyer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courts", t => t.Court_Id, cascadeDelete: true)
                .Index(t => t.Lawyer_Id)
                .Index(t => t.Court_Id);
            
            CreateTable(
                "dbo.ServiceTypeLawyers",
                c => new
                    {
                        ServiceType_Id = c.String(nullable: false, maxLength: 128),
                        Lawyer_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ServiceType_Id, t.Lawyer_Id })
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Lawyers", t => t.Lawyer_Id, cascadeDelete: true)
                .Index(t => t.ServiceType_Id)
                .Index(t => t.Lawyer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lawyers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceTypeLawyers", "Lawyer_Id", "dbo.Lawyers");
            DropForeignKey("dbo.ServiceTypeLawyers", "ServiceType_Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.LawyerCourts", "Court_Id", "dbo.Courts");
            DropForeignKey("dbo.LawyerCourts", "Lawyer_Id", "dbo.Lawyers");
            DropIndex("dbo.ServiceTypeLawyers", new[] { "Lawyer_Id" });
            DropIndex("dbo.ServiceTypeLawyers", new[] { "ServiceType_Id" });
            DropIndex("dbo.LawyerCourts", new[] { "Court_Id" });
            DropIndex("dbo.LawyerCourts", new[] { "Lawyer_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Lawyers", new[] { "Id" });
            DropTable("dbo.ServiceTypeLawyers");
            DropTable("dbo.LawyerCourts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.Lawyers");
            DropTable("dbo.Courts");
        }
    }
}
