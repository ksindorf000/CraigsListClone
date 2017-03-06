namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserPreferenceTable : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.UserPrefDatas");
        }
    }
}
