namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeToLanguageResource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LanguageValues", "Type", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LanguageValues", "Type");
        }
    }
}
