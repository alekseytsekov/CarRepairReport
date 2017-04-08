namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "IsPrimary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "IsPrimary");
        }
    }
}
