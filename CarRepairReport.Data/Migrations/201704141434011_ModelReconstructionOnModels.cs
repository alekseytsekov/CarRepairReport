namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelReconstructionOnModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarParts", "SerialNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.CarParts", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarParts", "Name", c => c.String());
            AlterColumn("dbo.CarParts", "SerialNumber", c => c.String());
        }
    }
}
