namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lllllllllll : DbMigration
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
            AddForeignKey("dbo.People", "CityID", "dbo.Cities", "CityID");
            AddForeignKey("dbo.People", "CountryID", "dbo.Countries", "CountryID");
            AddForeignKey("dbo.People", "DivisionID", "dbo.Divisions", "DivisionID");
            AddForeignKey("dbo.People", "DriverChangeTypeID", "dbo.DriverChangeTypes", "DriverChangeTypeID");
            AddForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls", "PayrollID2");
            AddForeignKey("dbo.People", "DriverTypeID", "dbo.DriverTypes", "DriverTypeID");
            AddForeignKey("dbo.People", "EntityID", "dbo.Entities", "EntityID");
            AddForeignKey("dbo.People", "ExitReasonID", "dbo.ExitReasons", "ExitReasonID");
            AddForeignKey("dbo.People", "LicenseProvinceID", "dbo.Provinces", "ProvinceID");
            AddForeignKey("dbo.People", "OrbitID", "dbo.Orbits", "OrbitID");
            AddForeignKey("dbo.People", "PayLevelID", "dbo.DPayLevels", "PayLevelID");
            AddForeignKey("dbo.People", "ProvinceID", "dbo.Provinces", "ProvinceID");
            AddForeignKey("dbo.People", "ChangeTypeID", "dbo.ChangeTypes", "ChangeTypeID");
            AddForeignKey("dbo.People", "CompanyID", "dbo.Companies", "CompanyID");
            AddForeignKey("dbo.People", "PayrollID", "dbo.Payrolls", "PayrollID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "PayrollID", "dbo.Payrolls");
            DropForeignKey("dbo.People", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.People", "ChangeTypeID", "dbo.ChangeTypes");
            DropForeignKey("dbo.People", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.People", "PayLevelID", "dbo.DPayLevels");
            DropForeignKey("dbo.People", "OrbitID", "dbo.Orbits");
            DropForeignKey("dbo.People", "LicenseProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.People", "ExitReasonID", "dbo.ExitReasons");
            DropForeignKey("dbo.People", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.People", "DriverTypeID", "dbo.DriverTypes");
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropForeignKey("dbo.People", "DriverChangeTypeID", "dbo.DriverChangeTypes");
            DropForeignKey("dbo.People", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.People", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.People", "CityID", "dbo.Cities");
            AddForeignKey("dbo.People", "PayrollID", "dbo.Payrolls", "PayrollID", cascadeDelete: true);
            AddForeignKey("dbo.People", "CompanyID", "dbo.Companies", "CompanyID", cascadeDelete: true);
            AddForeignKey("dbo.People", "ChangeTypeID", "dbo.ChangeTypes", "ChangeTypeID", cascadeDelete: true);
            AddForeignKey("dbo.People", "ProvinceID", "dbo.Provinces", "ProvinceID", cascadeDelete: true);
            AddForeignKey("dbo.People", "PayLevelID", "dbo.DPayLevels", "PayLevelID", cascadeDelete: true);
            AddForeignKey("dbo.People", "OrbitID", "dbo.Orbits", "OrbitID", cascadeDelete: true);
            AddForeignKey("dbo.People", "LicenseProvinceID", "dbo.Provinces", "ProvinceID", cascadeDelete: true);
            AddForeignKey("dbo.People", "ExitReasonID", "dbo.ExitReasons", "ExitReasonID", cascadeDelete: true);
            AddForeignKey("dbo.People", "EntityID", "dbo.Entities", "EntityID", cascadeDelete: true);
            AddForeignKey("dbo.People", "DriverTypeID", "dbo.DriverTypes", "DriverTypeID", cascadeDelete: true);
            AddForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls", "PayrollID2", cascadeDelete: true);
            AddForeignKey("dbo.People", "DriverChangeTypeID", "dbo.DriverChangeTypes", "DriverChangeTypeID", cascadeDelete: true);
            AddForeignKey("dbo.People", "DivisionID", "dbo.Divisions", "DivisionID", cascadeDelete: true);
            AddForeignKey("dbo.People", "CountryID", "dbo.Countries", "CountryID", cascadeDelete: true);
            AddForeignKey("dbo.People", "CityID", "dbo.Cities", "CityID", cascadeDelete: true);
        }
    }
}
