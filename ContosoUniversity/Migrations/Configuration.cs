namespace ContosoUniversity.Migrations
{
    using ContosoUniversity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUniversity.DAL.MDMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContosoUniversity.DAL.MDMSContext context)
        {
            //try
            //{

            //    //context.Database.ExecuteSqlCommand("DELETE FROM ChangeTypes");
            //    //context.Database.ExecuteSqlCommand("DELETE FROM SiteTypes");
            //    //context.Database.ExecuteSqlCommand("DELETE FROM Companies");
            //    //context.Database.ExecuteSqlCommand("DELETE FROM Payrolls");
            //    //context.Database.ExecuteSqlCommand("DELETE FROM Roles");
            
            //    var changetypes = new List<ChangeType>
            //{
            //new ChangeType{ChangeTypeName="New User"},
            //new ChangeType{ChangeTypeName="Change User"},
            //new ChangeType{ChangeTypeName="Remove User"}

            //};

            //    changetypes.ForEach(s => context.ChangeTypes.Add(s));
            //    context.SaveChanges();

            //    var companies = new List<Company>
            //{
            //new Company{CompanyName="Seaboard"},
            //new Company{CompanyName="Seaboard Bulk Terminals"},
            //new Company{CompanyName="Harmac"},
            //new Company{CompanyName="Foss"},
            //new Company{CompanyName="GWR"},
            //new Company{CompanyName="JBM Rentals"},
            //new Company{CompanyName="R&G"},
            //new Company{CompanyName="Tudhope"},
            //new Company{CompanyName="Wiebe"},
            //new Company{CompanyName="Unique"},
            //new Company{CompanyName="G3"}


            //};

            //    companies.ForEach(s => context.Companies.Add(s));
            //    context.SaveChanges();

            //var sitetypes = new List<SiteType>
            //{
            //new SiteType{SiteTypeName="Bill To"},
            //new SiteType{SiteTypeName="Shipper"},
            //new SiteType{SiteTypeName="Consignee"}

            //};

            //    sitetypes.ForEach(s => context.SiteTypes.Add(s));
            //    context.SaveChanges();

            //var payrolls = new List<Payroll>
            //{
            //new Payroll{PayrollName="Corporate Management"},
            //new Payroll{PayrollName="Corporate Non Management"},
            //new Payroll{PayrollName="G3"},
            //new Payroll{PayrollName="Unique"},
            //new Payroll{PayrollName="Mantei’s"},
            //new Payroll{PayrollName="JBM Rentals Fleet"},
            //new Payroll{PayrollName="Harmac Fleet"},
            //new Payroll{PayrollName="Seaboard Bulk Terminals"}
          

            //};



            //    payrolls.ForEach(s => context.Payrolls.Add(s));
            //    context.SaveChanges();

            //    var roles = new List<Role>
            //{
            //new Role{RoleName="Dispatcher"},
            //new Role{RoleName="Dispatch Administrator"},
            //new Role{RoleName="Billing/Settlements Analyst"},
            //new Role{RoleName="Accountant"},
            //new Role{RoleName="Pay Roll"},
            //new Role{RoleName="AR"},
            //new Role{RoleName="Safety"},
            //new Role{RoleName="HR"},
            //new Role{RoleName="Administrator Rates"},
            //new Role{RoleName="Administrator Customer"},
            //new Role{RoleName="Administrator Fleet"},
            //new Role{RoleName="Administrator Driver"},
            //new Role{RoleName="Manager"},
            //new Role{RoleName="IT"}
          

            //};

            //    roles.ForEach(s => context.Roles.Add(s));
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (var failure in ex.EntityValidationErrors)
            //    {
            //        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
            //        foreach (var error in failure.ValidationErrors)
            //        {
            //            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            //            sb.AppendLine();
            //        }
            //    }

            //    throw new DbEntityValidationException(
            //        "Entity Validation Failed - errors follow:\n" +
            //        sb.ToString(), ex
            //    ); // Add the original exception as the innerException
            //}
        }
    }
}
