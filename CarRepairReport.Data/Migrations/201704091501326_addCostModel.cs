namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCostModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Costs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MountedOn = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CarId = c.Int(nullable: false),
                        CarPartId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: false)
                .ForeignKey("dbo.CarParts", t => t.CarPartId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CarId)
                .Index(t => t.CarPartId);
            
            AddColumn("dbo.CarParts", "MountedOn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Costs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Costs", "CarPartId", "dbo.CarParts");
            DropForeignKey("dbo.Costs", "CarId", "dbo.Cars");
            DropIndex("dbo.Costs", new[] { "CarPartId" });
            DropIndex("dbo.Costs", new[] { "CarId" });
            DropIndex("dbo.Costs", new[] { "UserId" });
            DropColumn("dbo.CarParts", "MountedOn");
            DropTable("dbo.Costs");
        }
    }
}
