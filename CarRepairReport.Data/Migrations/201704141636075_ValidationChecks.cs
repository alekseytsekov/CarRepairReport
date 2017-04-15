namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationChecks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleServices", "WorkingTime", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.VehicleServices", "WorkingDays", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.VehicleServices", "NonWorkingDays", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleServices", "NonWorkingDays", c => c.String(maxLength: 200));
            AlterColumn("dbo.VehicleServices", "WorkingDays", c => c.String(maxLength: 200));
            AlterColumn("dbo.VehicleServices", "WorkingTime", c => c.String(maxLength: 200));
        }
    }
}
