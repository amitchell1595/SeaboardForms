namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tttt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "OrbitID", "dbo.Orbits");
            DropIndex("dbo.People", new[] { "OrbitID" });
            CreateTable(
                "dbo.Domiciles",
                c => new
                    {
                        DomicileID = c.Int(nullable: false, identity: true),
                        DomicileName = c.String(maxLength: 50),
                        TMWCode = c.String(maxLength: 10),
                        TMWCode2 = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.DomicileID);
            
            AddColumn("dbo.People", "DomicileID", c => c.Int());
            CreateIndex("dbo.People", "DomicileID");
            AddForeignKey("dbo.People", "DomicileID", "dbo.Domiciles", "DomicileID");
            DropColumn("dbo.People", "OrbitID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "OrbitID", c => c.Int());
            DropForeignKey("dbo.People", "DomicileID", "dbo.Domiciles");
            DropIndex("dbo.People", new[] { "DomicileID" });
            DropColumn("dbo.People", "DomicileID");
            DropTable("dbo.Domiciles");
            CreateIndex("dbo.People", "OrbitID");
            AddForeignKey("dbo.People", "OrbitID", "dbo.Orbits", "OrbitID");
        }
    }
}
