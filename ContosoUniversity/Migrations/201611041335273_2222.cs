namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2222 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DriverChangeTypes",
                c => new
                    {
                        DriverChangeTypeID = c.Int(nullable: false, identity: true),
                        DriverChangeTypeName = c.String(),
                    })
                .PrimaryKey(t => t.DriverChangeTypeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DriverChangeTypes");
        }
    }
}
