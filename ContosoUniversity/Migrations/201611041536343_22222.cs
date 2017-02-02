namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22222 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropPrimaryKey("dbo.DriverPayrolls");
            AlterColumn("dbo.DriverPayrolls", "PayrollID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DriverPayrolls", "PayrollID");
            AddForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls", "PayrollID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropPrimaryKey("dbo.DriverPayrolls");
            AlterColumn("dbo.DriverPayrolls", "PayrollID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.DriverPayrolls", "PayrollID");
            AddForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls", "PayrollID", cascadeDelete: true);
        }
    }
}
