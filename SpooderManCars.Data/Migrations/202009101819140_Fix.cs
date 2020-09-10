namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Int(nullable: false),
                        GarageId = c.Int(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        Make = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        CarType = c.Int(nullable: false),
                        Transmission = c.String(nullable: false),
                        CarValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Garage", t => t.GarageId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.GarageId);
            
            CreateTable(
                "dbo.Garage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CollectorId = c.Guid(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Locations = c.String(),
                        Founded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Racing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerID = c.Int(nullable: false),
                        TeamName = c.String(),
                        BasedOutOF = c.String(),
                        Drivers = c.String(),
                        RaceEvent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerID, cascadeDelete: true)
                .Index(t => t.ManufacturerID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Racing", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.Car", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Car", "GarageId", "dbo.Garage");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Racing", new[] { "ManufacturerID" });
            DropIndex("dbo.Car", new[] { "GarageId" });
            DropIndex("dbo.Car", new[] { "ManufacturerId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Racing");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Garage");
            DropTable("dbo.Car");
        }
    }
}
