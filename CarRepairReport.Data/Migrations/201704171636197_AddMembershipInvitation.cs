namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipInvitation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipInvitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleServiceId = c.Int(nullable: false),
                        MemberEmail = c.String(nullable: false, maxLength: 100),
                        IsAccepted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MembershipInvitations");
        }
    }
}
