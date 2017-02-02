namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orbits", "TMWCode2", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orbits", "TMWCode2");
        }
    }
}
