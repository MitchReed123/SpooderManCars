namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ellclkaje : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Garage", "CollectionValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Garage", "CollectionValue", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
