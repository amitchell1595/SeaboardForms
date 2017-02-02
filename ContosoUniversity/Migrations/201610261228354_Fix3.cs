namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sites", "ServiceFleet");
            DropColumn("dbo.Sites", "SiteUpload");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "SiteUpload", c => c.Binary());
            AddColumn("dbo.Sites", "ServiceFleet", c => c.String());
        }
    }
}
