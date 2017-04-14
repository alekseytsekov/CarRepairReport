namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVehicleServiceAdminsEmployees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "VehicleServiceId", c => c.Int());
            AddColumn("dbo.Users", "IsVehicleServiceOwner", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Users", "VehicleServiceId");
            AddForeignKey("dbo.Users", "VehicleServiceId", "dbo.VehicleServices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "VehicleServiceId", "dbo.VehicleServices");
            DropIndex("dbo.Users", new[] { "VehicleServiceId" });
            DropColumn("dbo.Users", "IsVehicleServiceOwner");
            DropColumn("dbo.Users", "VehicleServiceId");
        }
    }
}
