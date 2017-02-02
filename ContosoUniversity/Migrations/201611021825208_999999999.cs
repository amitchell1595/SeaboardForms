namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _999999999 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "TankInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "TankInfo");
        }
    }
}
