namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "SiteMapLocation", c => c.String());
            DropColumn("dbo.Sites", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "Phone", c => c.String());
            DropColumn("dbo.Sites", "SiteMapLocation");
        }
    }
}
