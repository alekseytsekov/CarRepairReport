namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLanguageValueEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LanguageValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LangTwoLetterCode = c.String(),
                        Key = c.String(),
                        Value = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LanguageValues");
        }
    }
}
