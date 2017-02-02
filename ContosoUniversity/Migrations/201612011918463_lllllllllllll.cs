namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lllllllllllll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "CityID", "dbo.Cities");
            DropForeignKey("dbo.People", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.People", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.People", "DriverChangeTypeID", "dbo.DriverChangeTypes");
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropForeignKey("dbo.People", "DriverTypeID", "dbo.DriverTypes");
            DropForeignKey("dbo.People", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.People", "ExitReasonID", "dbo.ExitReasons");
            DropForeignKey("dbo.People", "LicenseProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.People", "OrbitID", "dbo.Orbits");
            DropForeignKey("dbo.People", "PayLevelID", "dbo.DPayLevels");
            DropForeignKey("dbo.People", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.People", "ChangeTypeID", "dbo.ChangeTypes");
            DropForeignKey("dbo.People", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.People", "PayrollID", "dbo.Payrolls");
        }
        
        public override void Down()
        {
        }
    }
}
