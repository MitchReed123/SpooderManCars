namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Garage", "CollectionValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Garage", "CollectionValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
