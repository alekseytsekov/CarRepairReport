namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVehicleServiceToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "VehicleServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.VehicleServices", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.VehicleServices", "Owner_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.VehicleServices", "Owner_Id");
            AddForeignKey("dbo.VehicleServices", "Owner_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleServices", "Owner_Id", "dbo.Users");
            DropIndex("dbo.VehicleServices", new[] { "Owner_Id" });
            DropColumn("dbo.VehicleServices", "Owner_Id");
            DropColumn("dbo.VehicleServices", "UserId");
            DropColumn("dbo.Users", "VehicleServiceId");
        }
    }
}
