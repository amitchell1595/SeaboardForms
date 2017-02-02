namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tanks", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Tanks", "Site_SiteID", "dbo.Sites");
            DropIndex("dbo.Tanks", new[] { "CompanyID" });
            DropIndex("dbo.Tanks", new[] { "Site_SiteID" });
            DropPrimaryKey("dbo.Sites");
            AddColumn("dbo.Sites", "RequestID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Tanks", "Site_RequestID", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.Sites", "RequestID");
            CreateIndex("dbo.Tanks", "Site_RequestID");
            AddForeignKey("dbo.Tanks", "Site_RequestID", "dbo.Sites", "RequestID");
            DropColumn("dbo.Sites", "SiteID");
            DropColumn("dbo.Tanks", "CompanyID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tanks", "CompanyID", c => c.Int(nullable: false));
            AddColumn("dbo.Sites", "SiteID", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Tanks", "Site_RequestID", "dbo.Sites");
            DropIndex("dbo.Tanks", new[] { "Site_RequestID" });
            DropPrimaryKey("dbo.Sites");
            AlterColumn("dbo.Tanks", "Site_RequestID", c => c.String(maxLength: 128));
            DropColumn("dbo.Sites", "RequestID");
            AddPrimaryKey("dbo.Sites", "SiteID");
            RenameColumn(table: "dbo.Tanks", name: "Site_RequestID", newName: "Site_SiteID");
            CreateIndex("dbo.Tanks", "Site_SiteID");
            CreateIndex("dbo.Tanks", "CompanyID");
            AddForeignKey("dbo.Tanks", "Site_RequestID", "dbo.Sites", "RequestID");
            AddForeignKey("dbo.Tanks", "CompanyID", "dbo.Companies", "CompanyID", cascadeDelete: true);
        }
    }
}
