namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToMembershipInvitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipInvitations", "VehicleServiceName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipInvitations", "VehicleServiceName");
        }
    }
}
