namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zzzxxx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Approved", c => c.Boolean());
            AddColumn("dbo.People", "RejectReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "RejectReason");
            DropColumn("dbo.People", "Approved");
        }
    }
}
