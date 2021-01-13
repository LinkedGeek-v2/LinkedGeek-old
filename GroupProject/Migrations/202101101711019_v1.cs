namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.String(nullable: false, maxLength: 128),
                        StreetName = c.String(),
                        StreetNumber = c.String(),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.City", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AddressID)
                .Index(t => t.AddressID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 255),
                        Latitude = c.String(maxLength: 20),
                        Longtitude = c.String(maxLength: 20),
                        CountryIsoID = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.Country", t => t.CountryIsoID)
                .Index(t => t.CountryIsoID);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryIsoID = c.String(nullable: false, maxLength: 2),
                        CountryName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.CountryIsoID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImageName = c.String(),
                        IsDeveloper = c.Boolean(nullable: false),
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
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(nullable: false, maxLength: 100),
                        FoundationDate = c.DateTime(),
                        FounderName = c.String(maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CompanyID)
                .ForeignKey("dbo.AspNetUsers", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        DatePosted = c.DateTime(nullable: false),
                        JobDescription = c.String(),
                        JobType = c.Int(nullable: false),
                        CompanyID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.JobID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.JobsApplied",
                c => new
                    {
                        DeveloperID = c.String(nullable: false, maxLength: 128),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeveloperID, t.JobID })
                .ForeignKey("dbo.Developer", t => t.DeveloperID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.JobID)
                .Index(t => t.DeveloperID)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.Developer",
                c => new
                    {
                        DeveloperID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Int(),
                        CompanyWorkingId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DeveloperID)
                .ForeignKey("dbo.Company", t => t.CompanyWorkingId)
                .ForeignKey("dbo.AspNetUsers", t => t.DeveloperID)
                .Index(t => t.DeveloperID)
                .Index(t => t.CompanyWorkingId);
            
            CreateTable(
                "dbo.DeveloperSkills",
                c => new
                    {
                        DeveloperID = c.String(nullable: false, maxLength: 128),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeveloperID, t.SkillID })
                .ForeignKey("dbo.Developer", t => t.DeveloperID, cascadeDelete: true)
                .ForeignKey("dbo.Skill", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.DeveloperID)
                .Index(t => t.SkillID);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.Education",
                c => new
                    {
                        EducationID = c.Int(nullable: false, identity: true),
                        School = c.String(nullable: false, maxLength: 100),
                        Degree = c.String(nullable: false, maxLength: 100),
                        Field = c.String(nullable: false, maxLength: 100),
                        Grade = c.Double(),
                        StartYear = c.DateTime(nullable: false),
                        EndYear = c.DateTime(),
                        DeveloperID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EducationID)
                .ForeignKey("dbo.Developer", t => t.DeveloperID)
                .Index(t => t.DeveloperID);
            
            CreateTable(
                "dbo.Experience",
                c => new
                    {
                        ExperienceID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 100),
                        CompanyLocation = c.String(),
                        StartYear = c.DateTime(nullable: false),
                        EndYear = c.DateTime(),
                        ExperienceType = c.Int(nullable: false),
                        CompanyWorkingId = c.String(maxLength: 128),
                        DeveloperID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ExperienceID)
                .ForeignKey("dbo.Company", t => t.CompanyWorkingId)
                .ForeignKey("dbo.Developer", t => t.DeveloperID)
                .Index(t => t.CompanyWorkingId)
                .Index(t => t.DeveloperID);
            
            CreateTable(
                "dbo.Following",
                c => new
                    {
                        FollowerID = c.String(nullable: false, maxLength: 128),
                        FolloweeID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerID, t.FolloweeID })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerID)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeID)
                .Index(t => t.FollowerID)
                .Index(t => t.FolloweeID);
            
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
                "dbo.Notification",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateTimeSent = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        OtherUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.OtherUserId)
                .Index(t => t.OtherUserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        DatePosted = c.DateTime(nullable: false),
                        Text = c.String(),
                        ImageName = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
                "dbo.SkillsRequired",
                c => new
                    {
                        SkillID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillID, t.JobID })
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.Skill", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.SkillID)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillsRequired", "SkillID", "dbo.Skill");
            DropForeignKey("dbo.SkillsRequired", "JobID", "dbo.Job");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Post", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notification", "OtherUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Address", "AddressID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Following", "FolloweeID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Following", "FollowerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Company", "CompanyID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobsApplied", "JobID", "dbo.Job");
            DropForeignKey("dbo.JobsApplied", "DeveloperID", "dbo.Developer");
            DropForeignKey("dbo.Developer", "DeveloperID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Experience", "DeveloperID", "dbo.Developer");
            DropForeignKey("dbo.Experience", "CompanyWorkingId", "dbo.Company");
            DropForeignKey("dbo.Education", "DeveloperID", "dbo.Developer");
            DropForeignKey("dbo.DeveloperSkills", "SkillID", "dbo.Skill");
            DropForeignKey("dbo.DeveloperSkills", "DeveloperID", "dbo.Developer");
            DropForeignKey("dbo.Developer", "CompanyWorkingId", "dbo.Company");
            DropForeignKey("dbo.Job", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Address", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "CountryIsoID", "dbo.Country");
            DropIndex("dbo.SkillsRequired", new[] { "JobID" });
            DropIndex("dbo.SkillsRequired", new[] { "SkillID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Post", new[] { "UserId" });
            DropIndex("dbo.Notification", new[] { "OtherUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Following", new[] { "FolloweeID" });
            DropIndex("dbo.Following", new[] { "FollowerID" });
            DropIndex("dbo.Experience", new[] { "DeveloperID" });
            DropIndex("dbo.Experience", new[] { "CompanyWorkingId" });
            DropIndex("dbo.Education", new[] { "DeveloperID" });
            DropIndex("dbo.DeveloperSkills", new[] { "SkillID" });
            DropIndex("dbo.DeveloperSkills", new[] { "DeveloperID" });
            DropIndex("dbo.Developer", new[] { "CompanyWorkingId" });
            DropIndex("dbo.Developer", new[] { "DeveloperID" });
            DropIndex("dbo.JobsApplied", new[] { "JobID" });
            DropIndex("dbo.JobsApplied", new[] { "DeveloperID" });
            DropIndex("dbo.Job", new[] { "CompanyID" });
            DropIndex("dbo.Company", new[] { "CompanyID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.City", new[] { "CountryIsoID" });
            DropIndex("dbo.Address", new[] { "CityID" });
            DropIndex("dbo.Address", new[] { "AddressID" });
            DropTable("dbo.SkillsRequired");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Post");
            DropTable("dbo.Notification");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Following");
            DropTable("dbo.Experience");
            DropTable("dbo.Education");
            DropTable("dbo.Skill");
            DropTable("dbo.DeveloperSkills");
            DropTable("dbo.Developer");
            DropTable("dbo.JobsApplied");
            DropTable("dbo.Job");
            DropTable("dbo.Company");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Country");
            DropTable("dbo.City");
            DropTable("dbo.Address");
        }
    }
}
