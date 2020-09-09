namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jellin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Garage", "CollectionValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Garage", "CollectionValue", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
