namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUploadsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        File = c.String(),
                        TypeRef = c.String(),
                        RefId = c.Int(),
                        OwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uploads", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Uploads", new[] { "OwnerId" });
            DropTable("dbo.Uploads");
        }
    }
}
