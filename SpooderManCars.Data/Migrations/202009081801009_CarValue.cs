namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Car", "CarValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Garage", "CollectionValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Garage", "CollectionValue", c => c.Double(nullable: false));
            DropColumn("dbo.Car", "CarValue");
        }
    }
}
