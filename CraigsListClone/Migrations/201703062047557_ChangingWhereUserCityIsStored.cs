namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingWhereUserCityIsStored : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CityId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CityId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities");
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            DropColumn("dbo.AspNetUsers", "CityId");
        }
    }
}
