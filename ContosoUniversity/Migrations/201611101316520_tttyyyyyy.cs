namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tttyyyyyy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropPrimaryKey("dbo.DriverPayrolls");
            AddColumn("dbo.DriverPayrolls", "PayrollID2", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DriverPayrolls", "PayrollID2");
            AddForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls", "PayrollID2", cascadeDelete: true);
            DropColumn("dbo.DriverPayrolls", "PayrollID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DriverPayrolls", "PayrollID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropPrimaryKey("dbo.DriverPayrolls");
            DropColumn("dbo.DriverPayrolls", "PayrollID2");
            AddPrimaryKey("dbo.DriverPayrolls", "PayrollID");
            AddForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls", "PayrollID2", cascadeDelete: true);
        }
    }
}
