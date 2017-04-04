namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserToAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Addresses", "UserId");
            AddForeignKey("dbo.Addresses", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropColumn("dbo.Addresses", "UserId");
        }
    }
}
