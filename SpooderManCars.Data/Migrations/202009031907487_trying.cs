namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trying : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Int(nullable: false),
                        GarageId = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        CarType = c.Int(nullable: false),
                        Transmission = c.String(),
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
                        CollectorId = c.Guid(nullable: false),
                        Location = c.String(),
                        CollectionValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Cars", "GarageId", "dbo.Garage");
            DropIndex("dbo.Cars", new[] { "GarageId" });
            DropIndex("dbo.Cars", new[] { "ManufacturerId" });
            DropTable("dbo.Garage");
            DropTable("dbo.Cars");
            DropTable("dbo.Manufacturer");
        }
    }
}
