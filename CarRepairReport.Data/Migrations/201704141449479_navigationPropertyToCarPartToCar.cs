namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navigationPropertyToCarPartToCar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarParts", "Car_Id", "dbo.Cars");
            DropIndex("dbo.CarParts", new[] { "Car_Id" });
            RenameColumn(table: "dbo.CarParts", name: "Car_Id", newName: "CarId");
            AlterColumn("dbo.CarParts", "CarId", c => c.Int(nullable: false));
            CreateIndex("dbo.CarParts", "CarId");
            AddForeignKey("dbo.CarParts", "CarId", "dbo.Cars", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarParts", "CarId", "dbo.Cars");
            DropIndex("dbo.CarParts", new[] { "CarId" });
            AlterColumn("dbo.CarParts", "CarId", c => c.Int());
            RenameColumn(table: "dbo.CarParts", name: "CarId", newName: "Car_Id");
            CreateIndex("dbo.CarParts", "Car_Id");
            AddForeignKey("dbo.CarParts", "Car_Id", "dbo.Cars", "Id");
        }
    }
}
