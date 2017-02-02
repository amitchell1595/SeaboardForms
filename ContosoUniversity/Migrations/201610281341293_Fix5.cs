namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "AddedToTMW", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "AddedToTMW");
        }
    }
}
