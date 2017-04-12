namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNicknameToCar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "CarNickname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "CarNickname");
        }
    }
}
