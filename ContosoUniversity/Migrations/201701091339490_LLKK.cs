namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LLKK : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DriverTerminals",
                c => new
                    {
                        TerminalID = c.Int(nullable: false, identity: true),
                        TerminalName = c.String(maxLength: 50),
                        Code = c.String(maxLength: 50),
                        Code2 = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TerminalID);
            
            AddColumn("dbo.People", "TerminalID", c => c.Int());
            CreateIndex("dbo.People", "TerminalID");
            AddForeignKey("dbo.People", "TerminalID", "dbo.DriverTerminals", "TerminalID");
            DropColumn("dbo.People", "Terminal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Terminal", c => c.String());
            DropForeignKey("dbo.People", "TerminalID", "dbo.DriverTerminals");
            DropIndex("dbo.People", new[] { "TerminalID" });
            DropColumn("dbo.People", "TerminalID");
            DropTable("dbo.DriverTerminals");
        }
    }
}
