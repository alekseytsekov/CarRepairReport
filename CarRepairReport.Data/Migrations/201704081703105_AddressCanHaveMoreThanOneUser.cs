namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressCanHaveMoreThanOneUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Address_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Address_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Address_Id);
            
            DropColumn("dbo.Addresses", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserAddresses", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserAddresses", "User_Id", "dbo.Users");
            DropIndex("dbo.UserAddresses", new[] { "Address_Id" });
            DropIndex("dbo.UserAddresses", new[] { "User_Id" });
            DropTable("dbo.UserAddresses");
            CreateIndex("dbo.Addresses", "UserId");
            AddForeignKey("dbo.Addresses", "UserId", "dbo.Users", "Id");
        }
    }
}
