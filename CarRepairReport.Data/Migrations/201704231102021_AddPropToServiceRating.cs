namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToServiceRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRatings", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRatings", "Message");
        }
    }
}
