namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKPromotionVService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "VehicleServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Promotions", "VehicleServiceId");
            AddForeignKey("dbo.Promotions", "VehicleServiceId", "dbo.VehicleServices", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "VehicleServiceId", "dbo.VehicleServices");
            DropIndex("dbo.Promotions", new[] { "VehicleServiceId" });
            DropColumn("dbo.Promotions", "VehicleServiceId");
        }
    }
}
