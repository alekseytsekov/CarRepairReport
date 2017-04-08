namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anotherMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturers", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Manufacturers", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Manufacturers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Engines", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Engines", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Engines", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Engines", "IsDeleted");
            DropColumn("dbo.Engines", "ModifiedOn");
            DropColumn("dbo.Engines", "CreatedOn");
            DropColumn("dbo.Manufacturers", "IsDeleted");
            DropColumn("dbo.Manufacturers", "ModifiedOn");
            DropColumn("dbo.Manufacturers", "CreatedOn");
        }
    }
}
