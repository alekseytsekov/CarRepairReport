namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostModelChangeChildToChildren : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "ChildId", newName: "Post_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_ChildId", newName: "IX_Post_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_Post_Id", newName: "IX_ChildId");
            RenameColumn(table: "dbo.Posts", name: "Post_Id", newName: "ChildId");
        }
    }
}
