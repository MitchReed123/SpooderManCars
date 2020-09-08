namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Garage", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Garage", "Name");
        }
    }
}
