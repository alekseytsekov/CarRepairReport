namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceRatingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPositive = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        VehicleServiceId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.VehicleServices", t => t.VehicleServiceId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.VehicleServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRatings", "VehicleServiceId", "dbo.VehicleServices");
            DropForeignKey("dbo.ServiceRatings", "UserId", "dbo.Users");
            DropIndex("dbo.ServiceRatings", new[] { "VehicleServiceId" });
            DropIndex("dbo.ServiceRatings", new[] { "UserId" });
            DropTable("dbo.ServiceRatings");
        }
    }
}
