namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLittleChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImageUrl");
        }
    }
}
