namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePostCreationNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Created", c => c.DateTime(nullable: false));
        }
    }
}
