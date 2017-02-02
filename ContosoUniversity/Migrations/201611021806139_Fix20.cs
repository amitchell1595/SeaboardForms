namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix20 : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.People", "CityID", c => c.Int());
            CreateIndex("dbo.People", "CityID");
            AddForeignKey("dbo.People", "CityID", "dbo.Cities", "CityID", cascadeDelete: true);
            DropColumn("dbo.People", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "City", c => c.String());
            DropForeignKey("dbo.People", "CityID", "dbo.Cities");
            DropIndex("dbo.People", new[] { "CityID" });
            DropColumn("dbo.People", "CityID");
            
        }
    }
}
