using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;
using System.Data.Entity.Validation;
using System.Text;

namespace ContosoUniversity.DAL
{
    class MDMSInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MDMSContext>
    {
        protected override void Seed(MDMSContext context)
        {
            try {

                context.Database.ExecuteSqlCommand("TRUNCATE TABLE ChangeTypes");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE SiteTypes");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Companies");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Payrolls");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Roles");
            var changetypes = new List<ChangeType>
            {
            new ChangeType{ChangeTypeName="New User"},
            new ChangeType{ChangeTypeName="Change User"},
            new ChangeType{ChangeTypeName="Remove User"}

            };

            changetypes.ForEach(s => context.ChangeTypes.Add(s));
            context.SaveChanges();

            var dchangetypes = new List<DriverChangeType>
            {
            new DriverChangeType{DriverChangeTypeName="New Driver"},
            new DriverChangeType{DriverChangeTypeName="Driver Transfer"},
            new DriverChangeType{DriverChangeTypeName="Driver Information Change"},
            new DriverChangeType{DriverChangeTypeName="Exit"}

            };

            dchangetypes.ForEach(s => context.DriverChangeTypes.Add(s));
            context.SaveChanges();



            var sitetypes = new List<SiteType>
            {
            new SiteType{SiteTypeName="Bill To"},
            new SiteType{SiteTypeName="Shipper"},
            new SiteType{SiteTypeName="Consignee"}

            };

            sitetypes.ForEach(s => context.SiteTypes.Add(s));
            context.SaveChanges();

            var companies = new List<Company>
            {
            new Company{CompanyName="Seaboard"},
            new Company{CompanyName="Seaboard Bulk Terminals"},
            new Company{CompanyName="Harmac"},
            new Company{CompanyName="Foss"},
            new Company{CompanyName="GWR"},
            new Company{CompanyName="JBM Rentals"},
            new Company{CompanyName="R&G"},
            new Company{CompanyName="Tudhope"},
            new Company{CompanyName="Wiebe"},
            new Company{CompanyName="Unique"},
            new Company{CompanyName="G3"}


            };

            companies.ForEach(s => context.Companies.Add(s));
            context.SaveChanges();


            var payrolls = new List<Payroll>
            {
            new Payroll{PayrollName="Corporate Management"},
            new Payroll{PayrollName="Corporate Non Management"},
            new Payroll{PayrollName="G3"},
            new Payroll{PayrollName="Unique"},
            new Payroll{PayrollName="Mantei’s"},
            new Payroll{PayrollName="JBM Rentals Fleet"},
            new Payroll{PayrollName="Harmac Fleet"},
            new Payroll{PayrollName="Seaboard Bulk Terminals"}
          

            };

     

            payrolls.ForEach(s => context.Payrolls.Add(s));
            context.SaveChanges();

            var roles = new List<Role>
            {
            new Role{RoleName="Dispatcher"},
            new Role{RoleName="Dispatch Administrator"},
            new Role{RoleName="Billing/Settlements Analyst"},
            new Role{RoleName="Accountant"},
            new Role{RoleName="Pay Roll"},
            new Role{RoleName="AR"},
            new Role{RoleName="Safety"},
            new Role{RoleName="HR"},
            new Role{RoleName="Administrator Rates"},
            new Role{RoleName="Administrator Customer"},
            new Role{RoleName="Administrator Fleet"},
            new Role{RoleName="Administrator Driver"},
            new Role{RoleName="Manager"},
            new Role{RoleName="IT"}
          

            };

            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}