namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Desc = c.String(),
                        Cost = c.Double(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "OwnerId" });
            DropTable("dbo.Posts");
        }
    }
}
