namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tttyyyyyyzzz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "PayLevelID", c => c.Int());
            CreateIndex("dbo.People", "PayLevelID");
            AddForeignKey("dbo.People", "PayLevelID", "dbo.DPayLevels", "PayLevelID", cascadeDelete: true);
            DropColumn("dbo.People", "PayLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "PayLevel", c => c.String());
            DropForeignKey("dbo.People", "PayLevelID", "dbo.DPayLevels");
            DropIndex("dbo.People", new[] { "PayLevelID" });
            DropColumn("dbo.People", "PayLevelID");
        }
    }
}
