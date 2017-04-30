namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForumModels : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginalId = c.Int(),
                        ParentId = c.Int(),
                        ChildId = c.Int(),
                        Title = c.String(),
                        Content = c.String(),
                        IsQuestion = c.Boolean(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Posts", t => t.ChildId)
                .ForeignKey("dbo.Posts", t => t.OriginalId)
                .ForeignKey("dbo.Posts", t => t.ParentId)
                .Index(t => t.OriginalId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryPosts",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Post_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: false)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: false)
                .Index(t => t.Category_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Post_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: false)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: false)
                .Index(t => t.Tag_Id)
                .Index(t => t.Post_Id);
            
            CreateIndex("dbo.Users", "ApplicationUserId", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Posts", "ParentId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "OriginalId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ChildId", "dbo.Posts");
            DropForeignKey("dbo.CategoryPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.CategoryPosts", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Users");
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropIndex("dbo.CategoryPosts", new[] { "Post_Id" });
            DropIndex("dbo.CategoryPosts", new[] { "Category_Id" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "ChildId" });
            DropIndex("dbo.Posts", new[] { "ParentId" });
            DropIndex("dbo.Posts", new[] { "OriginalId" });
            DropIndex("dbo.Users", new[] { "ApplicationUserId" });
            DropTable("dbo.TagPosts");
            DropTable("dbo.CategoryPosts");
            DropTable("dbo.Tags");
            DropTable("dbo.Categories");
            DropTable("dbo.Posts");
            CreateIndex("dbo.Users", "ApplicationUserId");
        }
    }
}
