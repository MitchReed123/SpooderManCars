namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemadeRacing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Racing", "Victories", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Racing", "Victories");
        }
    }
}
