namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DroppedUserPrefTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserPrefDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPrefDatas",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
    }
}
