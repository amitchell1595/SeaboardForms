namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class llllllllllllll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "DriverChangeTypeID", "dbo.ChangeTypes");
        }
        
        public override void Down()
        {
        }
    }
}
