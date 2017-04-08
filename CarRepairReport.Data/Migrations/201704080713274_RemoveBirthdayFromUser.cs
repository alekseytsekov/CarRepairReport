namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBirthdayFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Birthday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
