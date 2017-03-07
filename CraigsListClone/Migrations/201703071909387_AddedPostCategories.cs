namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Categories", new[] { "Post_Id" });
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        CatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CatId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.CatId);
            
            DropColumn("dbo.Categories", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Post_Id", c => c.Int());
            DropForeignKey("dbo.PostCategories", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostCategories", "CatId", "dbo.Categories");
            DropIndex("dbo.PostCategories", new[] { "CatId" });
            DropIndex("dbo.PostCategories", new[] { "PostId" });
            DropTable("dbo.PostCategories");
            CreateIndex("dbo.Categories", "Post_Id");
            AddForeignKey("dbo.Categories", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
