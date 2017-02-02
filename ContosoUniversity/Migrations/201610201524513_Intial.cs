namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Axles",
                c => new
                    {
                        AxleID = c.Int(nullable: false, identity: true),
                        AxleName = c.String(),
                    })
                .PrimaryKey(t => t.AxleID);
            
            CreateTable(
                "dbo.BillToes",
                c => new
                    {
                        BillToID = c.Int(nullable: false, identity: true),
                        BillToName = c.String(maxLength: 50),
                        TMWCode = c.String(maxLength: 10),
                        DivisionCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.BillToID);
            
            CreateTable(
                "dbo.ChangeTypes",
                c => new
                    {
                        ChangeTypeID = c.Int(nullable: false, identity: true),
                        ChangeTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ChangeTypeID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 50),
                        ProvinceID = c.Int(nullable: false),
                        CityFullName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(maxLength: 50),
                        CountryCode = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        InstructorID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.People", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Completed = c.Boolean(nullable: false),
                        DriverID = c.String(),
                        TractorNumber = c.String(),
                        BrokerUnitNumber = c.String(),
                        OOBusinessNumber = c.String(),
                        DriverFirstName = c.String(),
                        DriverLastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                        SIN = c.String(),
                        HomePhone = c.String(),
                        CellPhone = c.String(),
                        Email = c.String(),
                        EmergencyName = c.String(),
                        EmergencyNumber = c.String(),
                        LicenseClass = c.String(),
                        LicenseNumber = c.String(),
                        EntityID = c.Int(),
                        DivisionID = c.Int(),
                        DriverTypeID = c.Int(),
                        OrbitID = c.Int(),
                        Terminal = c.String(),
                        PayLevel = c.String(),
                        CountryID = c.Int(),
                        ProvinceID = c.Int(),
                        LicenseProvinceID = c.Int(),
                        DriverChangeTypeID = c.Int(),
                        DriverPayrollID = c.Int(),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        HireDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        OrientationStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        OrientationEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastDayWorked = c.DateTime(precision: 7, storeType: "datetime2"),
                        ExitReasonID = c.Int(),
                        ExitDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Comments = c.String(),
                        Pending = c.Boolean(),
                        EffectiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ChangeTypeID = c.Int(),
                        RoleID = c.Int(),
                        CompanyID = c.Int(),
                        PayrollID = c.Int(),
                        SupervisorName = c.String(),
                        ApplicationsAndFolders = c.String(),
                        AdditionalInfo = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.DivisionID, cascadeDelete: true)
                .ForeignKey("dbo.ChangeTypes", t => t.DriverChangeTypeID, cascadeDelete: true)
                .ForeignKey("dbo.DriverPayrolls", t => t.DriverPayrollID, cascadeDelete: true)
                .ForeignKey("dbo.DriverTypes", t => t.DriverTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.EntityID, cascadeDelete: true)
                .ForeignKey("dbo.ExitReasons", t => t.ExitReasonID, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.LicenseProvinceID, cascadeDelete: true)
                .ForeignKey("dbo.Orbits", t => t.OrbitID, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceID, cascadeDelete: false)
                .ForeignKey("dbo.ChangeTypes", t => t.ChangeTypeID, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: false)
                .ForeignKey("dbo.Payrolls", t => t.PayrollID, cascadeDelete: false)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: false)
                .Index(t => t.EntityID)
                .Index(t => t.DivisionID)
                .Index(t => t.DriverTypeID)
                .Index(t => t.OrbitID)
                .Index(t => t.CountryID)
                .Index(t => t.ProvinceID)
                .Index(t => t.LicenseProvinceID)
                .Index(t => t.DriverChangeTypeID)
                .Index(t => t.DriverPayrollID)
                .Index(t => t.ExitReasonID)
                .Index(t => t.ChangeTypeID)
                .Index(t => t.RoleID)
                .Index(t => t.CompanyID)
                .Index(t => t.PayrollID);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionID = c.Int(nullable: false, identity: true),
                        DivisionName = c.String(maxLength: 50),
                        TMWCode = c.String(maxLength: 10),
                        EntityCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.DivisionID);
            
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        EntityID = c.Int(nullable: false, identity: true),
                        EntityName = c.String(maxLength: 50),
                        TMWCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.EntityID);
            
            CreateTable(
                "dbo.Orbits",
                c => new
                    {
                        OrbitID = c.Int(nullable: false, identity: true),
                        OrbitName = c.String(maxLength: 50),
                        TMWCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.OrbitID);
            
            CreateTable(
                "dbo.Payrolls",
                c => new
                    {
                        PayrollID = c.Int(nullable: false, identity: true),
                        PayrollName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PayrollID);
            
            CreateTable(
                "dbo.DriverPayrolls",
                c => new
                    {
                        PayrollID = c.Int(nullable: false),
                        PayrollName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PayrollID);
            
            CreateTable(
                "dbo.DriverTypes",
                c => new
                    {
                        DriverTypeID = c.Int(nullable: false, identity: true),
                        DriverTypeName = c.String(),
                    })
                .PrimaryKey(t => t.DriverTypeID);
            
            CreateTable(
                "dbo.ExitReasons",
                c => new
                    {
                        ExitReasonID = c.Int(nullable: false),
                        ExitReasonName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ExitReasonID);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceID = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(),
                        ProvinceAbbr = c.String(),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProvinceID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        SiteID = c.String(nullable: false, maxLength: 128),
                        SiteChangeTypeID = c.Int(nullable: false),
                        Address = c.String(),
                        Address2 = c.String(),
                        Email = c.String(),
                        AltID = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        AutomaticDips = c.Boolean(nullable: false),
                        BillToID = c.Int(nullable: false),
                        ServiceFleet = c.String(),
                        SitePriority = c.Int(nullable: false),
                        SiteUpload = c.Binary(),
                        Comment1 = c.String(),
                        Comment2 = c.String(),
                        PostalCode = c.String(),
                        MainPhone = c.String(),
                        SecondaryPhone = c.String(),
                        Fax = c.String(),
                        CityID = c.Int(nullable: false),
                        SiteTypeID = c.Int(nullable: false),
                        DivisionID = c.Int(nullable: false),
                        TerminalID = c.Int(nullable: false),
                        EntityID = c.Int(nullable: false),
                        OrbitID = c.Int(nullable: false),
                        AxleID = c.Int(nullable: false),
                        CountryID = c.Int(nullable: false),
                        ProvinceID = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SiteID)
                .ForeignKey("dbo.Axles", t => t.AxleID, cascadeDelete: true)
                .ForeignKey("dbo.BillToes", t => t.BillToID, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.DivisionID, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.EntityID, cascadeDelete: true)
                .ForeignKey("dbo.Orbits", t => t.OrbitID, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceID, cascadeDelete: true)
                .ForeignKey("dbo.SiteChangeTypes", t => t.SiteChangeTypeID, cascadeDelete: true)
                .ForeignKey("dbo.SiteTypes", t => t.SiteTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Terminals", t => t.TerminalID, cascadeDelete: true)
                .Index(t => t.SiteChangeTypeID)
                .Index(t => t.BillToID)
                .Index(t => t.CityID)
                .Index(t => t.SiteTypeID)
                .Index(t => t.DivisionID)
                .Index(t => t.TerminalID)
                .Index(t => t.EntityID)
                .Index(t => t.OrbitID)
                .Index(t => t.AxleID)
                .Index(t => t.CountryID)
                .Index(t => t.ProvinceID);
            
            CreateTable(
                "dbo.SiteChangeTypes",
                c => new
                    {
                        ChangeTypeID = c.Int(nullable: false),
                        ChangeTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ChangeTypeID);
            
            CreateTable(
                "dbo.SiteTypes",
                c => new
                    {
                        SiteTypeID = c.Int(nullable: false, identity: true),
                        SiteTypeName = c.String(),
                    })
                .PrimaryKey(t => t.SiteTypeID);
            
            CreateTable(
                "dbo.Terminals",
                c => new
                    {
                        TerminalID = c.Int(nullable: false, identity: true),
                        TerminalName = c.String(maxLength: 50),
                        TMWCode = c.String(maxLength: 10),
                        DivisionCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.TerminalID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "TerminalID", "dbo.Terminals");
            DropForeignKey("dbo.Sites", "SiteTypeID", "dbo.SiteTypes");
            DropForeignKey("dbo.Sites", "SiteChangeTypeID", "dbo.SiteChangeTypes");
            DropForeignKey("dbo.Sites", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.Sites", "OrbitID", "dbo.Orbits");
            DropForeignKey("dbo.Sites", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Sites", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.Sites", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Sites", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Sites", "BillToID", "dbo.BillToes");
            DropForeignKey("dbo.Sites", "AxleID", "dbo.Axles");
            DropForeignKey("dbo.People", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.People", "PayrollID", "dbo.Payrolls");
            DropForeignKey("dbo.People", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.People", "ChangeTypeID", "dbo.ChangeTypes");
            DropForeignKey("dbo.People", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.People", "OrbitID", "dbo.Orbits");
            DropForeignKey("dbo.People", "LicenseProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.People", "ExitReasonID", "dbo.ExitReasons");
            DropForeignKey("dbo.People", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.People", "DriverTypeID", "dbo.DriverTypes");
            DropForeignKey("dbo.People", "DriverPayrollID", "dbo.DriverPayrolls");
            DropForeignKey("dbo.People", "DriverChangeTypeID", "dbo.ChangeTypes");
            DropForeignKey("dbo.People", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.People", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Departments", "InstructorID", "dbo.People");
            DropIndex("dbo.Sites", new[] { "ProvinceID" });
            DropIndex("dbo.Sites", new[] { "CountryID" });
            DropIndex("dbo.Sites", new[] { "AxleID" });
            DropIndex("dbo.Sites", new[] { "OrbitID" });
            DropIndex("dbo.Sites", new[] { "EntityID" });
            DropIndex("dbo.Sites", new[] { "TerminalID" });
            DropIndex("dbo.Sites", new[] { "DivisionID" });
            DropIndex("dbo.Sites", new[] { "SiteTypeID" });
            DropIndex("dbo.Sites", new[] { "CityID" });
            DropIndex("dbo.Sites", new[] { "BillToID" });
            DropIndex("dbo.Sites", new[] { "SiteChangeTypeID" });
            DropIndex("dbo.People", new[] { "PayrollID" });
            DropIndex("dbo.People", new[] { "CompanyID" });
            DropIndex("dbo.People", new[] { "RoleID" });
            DropIndex("dbo.People", new[] { "ChangeTypeID" });
            DropIndex("dbo.People", new[] { "ExitReasonID" });
            DropIndex("dbo.People", new[] { "DriverPayrollID" });
            DropIndex("dbo.People", new[] { "DriverChangeTypeID" });
            DropIndex("dbo.People", new[] { "LicenseProvinceID" });
            DropIndex("dbo.People", new[] { "ProvinceID" });
            DropIndex("dbo.People", new[] { "CountryID" });
            DropIndex("dbo.People", new[] { "OrbitID" });
            DropIndex("dbo.People", new[] { "DriverTypeID" });
            DropIndex("dbo.People", new[] { "DivisionID" });
            DropIndex("dbo.People", new[] { "EntityID" });
            DropIndex("dbo.Departments", new[] { "InstructorID" });
            DropTable("dbo.Terminals");
            DropTable("dbo.SiteTypes");
            DropTable("dbo.SiteChangeTypes");
            DropTable("dbo.Sites");
            DropTable("dbo.Roles");
            DropTable("dbo.Provinces");
            DropTable("dbo.ExitReasons");
            DropTable("dbo.DriverTypes");
            DropTable("dbo.DriverPayrolls");
            DropTable("dbo.Payrolls");
            DropTable("dbo.Orbits");
            DropTable("dbo.Entities");
            DropTable("dbo.Divisions");
            DropTable("dbo.People");
            DropTable("dbo.Departments");
            DropTable("dbo.Countries");
            DropTable("dbo.Companies");
            DropTable("dbo.Cities");
            DropTable("dbo.ChangeTypes");
            DropTable("dbo.BillToes");
            DropTable("dbo.Axles");
        }
    }
}
