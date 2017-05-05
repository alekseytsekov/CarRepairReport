namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPropToErrorLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ErrorLogs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ErrorLogs", "UserId");
        }
    }
}
