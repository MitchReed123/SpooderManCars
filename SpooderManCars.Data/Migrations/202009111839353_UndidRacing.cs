namespace SpooderManCars.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UndidRacing : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Racing", "Victories");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Racing", "Victories", c => c.String());
        }
    }
}
