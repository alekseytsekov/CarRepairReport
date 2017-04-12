namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePropertyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarParts", "MountedOnKm", c => c.Int(nullable: false));
            DropColumn("dbo.CarParts", "MountedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarParts", "MountedOn", c => c.Int(nullable: false));
            DropColumn("dbo.CarParts", "MountedOnKm");
        }
    }
}
