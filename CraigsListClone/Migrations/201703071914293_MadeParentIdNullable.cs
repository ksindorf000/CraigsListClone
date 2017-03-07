namespace CraigsListClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeParentIdNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "ParentId", c => c.Int(nullable: false));
        }
    }
}
