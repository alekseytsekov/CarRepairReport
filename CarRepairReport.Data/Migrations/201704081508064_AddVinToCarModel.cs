namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVinToCarModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "VIN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "VIN");
        }
    }
}
