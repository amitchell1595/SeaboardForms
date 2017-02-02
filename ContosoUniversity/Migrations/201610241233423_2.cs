namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tanks",
                c => new
                    {
                        TankID = c.Int(nullable: false, identity: true),
                        ProductClass = c.Int(nullable: false),
                        ProductCode = c.Int(nullable: false),
                        TankModel = c.Int(nullable: false),
                        TankUOM = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                        SafeFill = c.Int(nullable: false),
                        ShutDown = c.Int(nullable: false),
                        DailyConsumption = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        CityFullName = c.String(maxLength: 50),
                        Site_SiteID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TankID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.ProductClasses", t => t.ProductClass, cascadeDelete: true)
                .ForeignKey("dbo.ProductCodes", t => t.ProductCode, cascadeDelete: true)
                .ForeignKey("dbo.TankModels", t => t.TankModel, cascadeDelete: true)
                .ForeignKey("dbo.TankUOMs", t => t.TankUOM, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.Site_SiteID)
                .Index(t => t.ProductClass)
                .Index(t => t.ProductCode)
                .Index(t => t.TankModel)
                .Index(t => t.TankUOM)
                .Index(t => t.CompanyID)
                .Index(t => t.Site_SiteID);
            
            CreateTable(
                "dbo.ProductClasses",
                c => new
                    {
                        ProductClassID = c.Int(nullable: false, identity: true),
                        ProductClassName = c.String(),
                        TMWCode = c.String(),
                    })
                .PrimaryKey(t => t.ProductClassID);
            
            CreateTable(
                "dbo.ProductCodes",
                c => new
                    {
                        ProductCodeID = c.Int(nullable: false, identity: true),
                        ProductCodeName = c.String(),
                        TMWCode = c.String(),
                    })
                .PrimaryKey(t => t.ProductCodeID);
            
            CreateTable(
                "dbo.TankModels",
                c => new
                    {
                        TankModelID = c.Int(nullable: false, identity: true),
                        TankModelName = c.String(),
                        TankModelMake = c.String(),
                        TankModelCapacity = c.Int(nullable: false),
                        TMWCode = c.String(),
                    })
                .PrimaryKey(t => t.TankModelID);
            
            CreateTable(
                "dbo.TankUOMs",
                c => new
                    {
                        TankUOMID = c.Int(nullable: false, identity: true),
                        TankUOMName = c.String(),
                    })
                .PrimaryKey(t => t.TankUOMID);
            
            AddColumn("dbo.Sites", "AdditionalInfo", c => c.String());
            AddColumn("dbo.Sites", "NumberOfTanks", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tanks", "Site_SiteID", "dbo.Sites");
            DropForeignKey("dbo.Tanks", "TankUOM", "dbo.TankUOMs");
            DropForeignKey("dbo.Tanks", "TankModel", "dbo.TankModels");
            DropForeignKey("dbo.Tanks", "ProductCode", "dbo.ProductCodes");
            DropForeignKey("dbo.Tanks", "ProductClass", "dbo.ProductClasses");
            DropForeignKey("dbo.Tanks", "CompanyID", "dbo.Companies");
            DropIndex("dbo.Tanks", new[] { "Site_SiteID" });
            DropIndex("dbo.Tanks", new[] { "CompanyID" });
            DropIndex("dbo.Tanks", new[] { "TankUOM" });
            DropIndex("dbo.Tanks", new[] { "TankModel" });
            DropIndex("dbo.Tanks", new[] { "ProductCode" });
            DropIndex("dbo.Tanks", new[] { "ProductClass" });
            DropColumn("dbo.Sites", "NumberOfTanks");
            DropColumn("dbo.Sites", "AdditionalInfo");
            DropTable("dbo.TankUOMs");
            DropTable("dbo.TankModels");
            DropTable("dbo.ProductCodes");
            DropTable("dbo.ProductClasses");
            DropTable("dbo.Tanks");
        }
    }
}
