namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gggggggg1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "DriverEffectiveDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "DriverEffectiveDate");
        }
    }
}
