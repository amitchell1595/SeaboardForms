namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "TMWCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cities", "TMWCode");
        }
    }
}
