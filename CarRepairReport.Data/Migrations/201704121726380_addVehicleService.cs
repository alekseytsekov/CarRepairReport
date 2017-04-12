namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVehicleService : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Costs", "CarPartId", "dbo.CarParts");
            DropIndex("dbo.Costs", new[] { "CarPartId" });
            CreateTable(
                "dbo.VehicleServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        AddressId = c.Int(nullable: false),
                        LogoUrl = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .Index(t => t.AddressId);
            
            AddColumn("dbo.CarParts", "Cost_Id", c => c.Int());
            AddColumn("dbo.Costs", "RequestedToVehicleService", c => c.Boolean(nullable: false));
            AddColumn("dbo.Costs", "IsApprovedByVehicleService", c => c.Boolean(nullable: false));
            AddColumn("dbo.Costs", "VehicleServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.CarParts", "Cost_Id");
            CreateIndex("dbo.Costs", "VehicleServiceId");
            AddForeignKey("dbo.CarParts", "Cost_Id", "dbo.Costs", "Id");
            AddForeignKey("dbo.Costs", "VehicleServiceId", "dbo.VehicleServices", "Id", cascadeDelete: false);
            DropColumn("dbo.Costs", "CarPartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Costs", "CarPartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Costs", "VehicleServiceId", "dbo.VehicleServices");
            DropForeignKey("dbo.VehicleServices", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.CarParts", "Cost_Id", "dbo.Costs");
            DropIndex("dbo.VehicleServices", new[] { "AddressId" });
            DropIndex("dbo.Costs", new[] { "VehicleServiceId" });
            DropIndex("dbo.CarParts", new[] { "Cost_Id" });
            DropColumn("dbo.Costs", "VehicleServiceId");
            DropColumn("dbo.Costs", "IsApprovedByVehicleService");
            DropColumn("dbo.Costs", "RequestedToVehicleService");
            DropColumn("dbo.CarParts", "Cost_Id");
            DropTable("dbo.VehicleServices");
            CreateIndex("dbo.Costs", "CarPartId");
            AddForeignKey("dbo.Costs", "CarPartId", "dbo.CarParts", "Id", cascadeDelete: true);
        }
    }
}
