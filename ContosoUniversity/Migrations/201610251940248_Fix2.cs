namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sites", "SiteChangeTypeID", "dbo.SiteChangeTypes");
            DropIndex("dbo.Sites", new[] { "SiteChangeTypeID" });
            DropColumn("dbo.Sites", "SiteChangeTypeID");
            DropTable("dbo.SiteChangeTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SiteChangeTypes",
                c => new
                    {
                        ChangeTypeID = c.Int(nullable: false),
                        ChangeTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ChangeTypeID);
            
            AddColumn("dbo.Sites", "SiteChangeTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sites", "SiteChangeTypeID");
            AddForeignKey("dbo.Sites", "SiteChangeTypeID", "dbo.SiteChangeTypes", "ChangeTypeID", cascadeDelete: true);
        }
    }
}
