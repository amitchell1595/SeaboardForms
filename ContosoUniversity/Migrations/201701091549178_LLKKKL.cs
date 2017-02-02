namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LLKKKL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "AddedToTMW", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "AddedToTMW");
        }
    }
}
