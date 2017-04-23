namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToCarPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarParts", "IsSeenByVehicleService", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarParts", "IsSeenByVehicleService");
        }
    }
}
