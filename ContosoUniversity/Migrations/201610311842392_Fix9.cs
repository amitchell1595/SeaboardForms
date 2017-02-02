namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "RoleID", "dbo.Roles");
            DropIndex("dbo.People", new[] { "RoleID" });
            AddColumn("dbo.People", "Role", c => c.String());
            DropColumn("dbo.People", "RoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "RoleID", c => c.Int());
            DropColumn("dbo.People", "Role");
            CreateIndex("dbo.People", "RoleID");
            AddForeignKey("dbo.People", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
        }
    }
}
