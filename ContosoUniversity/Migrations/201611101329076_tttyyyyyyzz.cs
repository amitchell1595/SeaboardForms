namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tttyyyyyyzz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DPayLevels",
                c => new
                    {
                        PayLevelID = c.Int(nullable: false, identity: true),
                        PayLevelName = c.String(maxLength: 50),
                        TMWCode = c.String(),
                    })
                .PrimaryKey(t => t.PayLevelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DPayLevels");
        }
    }
}
