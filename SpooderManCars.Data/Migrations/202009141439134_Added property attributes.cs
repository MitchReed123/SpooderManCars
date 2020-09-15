namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedpropertyattributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Garage", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Garage", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Manufacturer", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.Manufacturer", "Locations", c => c.String(nullable: false));
            AlterColumn("dbo.Racing", "TeamName", c => c.String(nullable: false));
            AlterColumn("dbo.Racing", "BasedOutOF", c => c.String(nullable: false));
            AlterColumn("dbo.Racing", "Drivers", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Racing", "Drivers", c => c.String());
            AlterColumn("dbo.Racing", "BasedOutOF", c => c.String());
            AlterColumn("dbo.Racing", "TeamName", c => c.String());
            AlterColumn("dbo.Manufacturer", "Locations", c => c.String());
            AlterColumn("dbo.Manufacturer", "CompanyName", c => c.String());
            AlterColumn("dbo.Garage", "Location", c => c.String());
            AlterColumn("dbo.Garage", "Name", c => c.String());
        }
    }
}
