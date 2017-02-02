namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tttyyy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverPayrolls", "TMWCode", c => c.String());
            AddColumn("dbo.DriverTypes", "TMWCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverTypes", "TMWCode");
            DropColumn("dbo.DriverPayrolls", "TMWCode");
        }
    }
}
