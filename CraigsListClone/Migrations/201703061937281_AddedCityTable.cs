namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "CityId", c => c.Int(nullable: true));
            CreateIndex("dbo.Posts", "CityId");
            AddForeignKey("dbo.Posts", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "CityId", "dbo.Cities");
            DropIndex("dbo.Posts", new[] { "CityId" });
            DropColumn("dbo.Posts", "CityId");
            DropTable("dbo.Cities");
        }
    }
}
