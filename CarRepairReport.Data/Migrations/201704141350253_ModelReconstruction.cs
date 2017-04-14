namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelReconstruction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarParts", "Cost_Id", "dbo.Costs");
            DropForeignKey("dbo.Costs", "VehicleServiceId", "dbo.VehicleServices");
            DropIndex("dbo.CarParts", new[] { "Cost_Id" });
            DropIndex("dbo.Costs", new[] { "VehicleServiceId" });
            AddColumn("dbo.CarParts", "RequestedToVehicleService", c => c.Boolean(nullable: false));
            AddColumn("dbo.CarParts", "IsApprovedByVehicleService", c => c.Boolean(nullable: false));
            AddColumn("dbo.CarParts", "VehicleServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.CarParts", "VehicleServiceId");
            AddForeignKey("dbo.CarParts", "VehicleServiceId", "dbo.VehicleServices", "Id", cascadeDelete: true);
            DropColumn("dbo.CarParts", "Cost_Id");
            DropColumn("dbo.Costs", "MountedOn");
            DropColumn("dbo.Costs", "RequestedToVehicleService");
            DropColumn("dbo.Costs", "IsApprovedByVehicleService");
            DropColumn("dbo.Costs", "VehicleServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Costs", "VehicleServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.Costs", "IsApprovedByVehicleService", c => c.Boolean(nullable: false));
            AddColumn("dbo.Costs", "RequestedToVehicleService", c => c.Boolean(nullable: false));
            AddColumn("dbo.Costs", "MountedOn", c => c.Int(nullable: false));
            AddColumn("dbo.CarParts", "Cost_Id", c => c.Int());
            DropForeignKey("dbo.CarParts", "VehicleServiceId", "dbo.VehicleServices");
            DropIndex("dbo.CarParts", new[] { "VehicleServiceId" });
            DropColumn("dbo.CarParts", "VehicleServiceId");
            DropColumn("dbo.CarParts", "IsApprovedByVehicleService");
            DropColumn("dbo.CarParts", "RequestedToVehicleService");
            CreateIndex("dbo.Costs", "VehicleServiceId");
            CreateIndex("dbo.CarParts", "Cost_Id");
            AddForeignKey("dbo.Costs", "VehicleServiceId", "dbo.VehicleServices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CarParts", "Cost_Id", "dbo.Costs", "Id");
        }
    }
}
