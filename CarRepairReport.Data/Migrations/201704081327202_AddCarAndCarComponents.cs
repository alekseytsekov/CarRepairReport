namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarAndCarComponents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Make = c.String(),
                        GearboxId = c.Int(nullable: false),
                        EngineId = c.Int(nullable: false),
                        FirstRegistration = c.DateTime(nullable: false),
                        RunningDistance = c.Int(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Engines", t => t.EngineId, cascadeDelete: false)
                .ForeignKey("dbo.Gearboxes", t => t.GearboxId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.GearboxId)
                .Index(t => t.EngineId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.CarParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManufacturerId = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: false)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.ManufacturerId)
                .Index(t => t.Car_Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Engines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FuelType = c.Int(nullable: false),
                        EngineSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnginePower = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gearboxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfGears = c.Int(nullable: false),
                        GearBoxType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Cars", "GearboxId", "dbo.Gearboxes");
            DropForeignKey("dbo.Cars", "EngineId", "dbo.Engines");
            DropForeignKey("dbo.CarParts", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.CarParts", "ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.CarParts", new[] { "Car_Id" });
            DropIndex("dbo.CarParts", new[] { "ManufacturerId" });
            DropIndex("dbo.Cars", new[] { "OwnerId" });
            DropIndex("dbo.Cars", new[] { "EngineId" });
            DropIndex("dbo.Cars", new[] { "GearboxId" });
            DropTable("dbo.Gearboxes");
            DropTable("dbo.Engines");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.CarParts");
            DropTable("dbo.Cars");
        }
    }
}
