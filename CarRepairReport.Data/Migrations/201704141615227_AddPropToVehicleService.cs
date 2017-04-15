namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToVehicleService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleServices", "WorkingTime", c => c.String(maxLength: 200));
            AddColumn("dbo.VehicleServices", "WorkingDays", c => c.String(maxLength: 200));
            AddColumn("dbo.VehicleServices", "NonWorkingDays", c => c.String(maxLength: 200));
            AlterColumn("dbo.VehicleServices", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.VehicleServices", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleServices", "Description", c => c.String());
            AlterColumn("dbo.VehicleServices", "Name", c => c.String());
            DropColumn("dbo.VehicleServices", "NonWorkingDays");
            DropColumn("dbo.VehicleServices", "WorkingDays");
            DropColumn("dbo.VehicleServices", "WorkingTime");
        }
    }
}
