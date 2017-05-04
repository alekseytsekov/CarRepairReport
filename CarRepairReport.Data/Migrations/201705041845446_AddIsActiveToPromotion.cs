namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToPromotion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promotions", "IsActive");
        }
    }
}
