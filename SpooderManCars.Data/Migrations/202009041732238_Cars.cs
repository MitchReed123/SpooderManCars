namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cars : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cars", newName: "Car");
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
            
            AddColumn("dbo.Car", "OwnerID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Car", "Make", c => c.String(nullable: false));
            AlterColumn("dbo.Car", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.Car", "Transmission", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Racing", "ManufacturerID", "dbo.Manufacturer");
            DropIndex("dbo.Racing", new[] { "ManufacturerID" });
            AlterColumn("dbo.Car", "Transmission", c => c.String());
            AlterColumn("dbo.Car", "Model", c => c.String());
            AlterColumn("dbo.Car", "Make", c => c.String());
            DropColumn("dbo.Car", "OwnerID");
            DropTable("dbo.Racing");
            RenameTable(name: "dbo.Car", newName: "Cars");
        }
    }
}
