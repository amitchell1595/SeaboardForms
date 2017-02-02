using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System.Web.Script.Serialization;


namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private MDMSContext db = new MDMSContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // Commenting out LINQ to show how to do the same thing in SQL.
            //IQueryable<EnrollmentDateGroup> = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            // SQL version of the above LINQ code.
            string query = "SELECT EffectiveDate, COUNT(*) AS UserCount"
                + " FROM People"
                + " WHERE Discriminator = 'User' "
                + "GROUP BY EffectiveDate";
            IEnumerable<EffectiveDateGroup> data = db.Database.SqlQuery<EffectiveDateGroup>(query);

            return View(data.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ViewResult Error()
        {
            //Response.StatusCode = 401; // Do not set this or else you get a redirect loop
            return View();
        }

        public string GetProvincesForCountry(int id)
        {
            // get the products from the repository 

            var products = new SelectList(db.Provinces.Where(p => p.CountryID == id).OrderBy(i => i.ProvinceName), "ProvinceID", "ProvinceName");

            return new JavaScriptSerializer().Serialize(products);
        }

        
        public string GetDivisionsForEntity(int id)
        {
            // get the products from the repository 
            var divCode = (from h in db.Entities
                           where h.EntityID.Equals(id)
                           select h.TMWCode).SingleOrDefault();
            var products = new SelectList(db.Divisions.Where(p => p.EntityCode == divCode).OrderBy(i => i.DivisionName), "DivisionID", "DivisionName");

            return new JavaScriptSerializer().Serialize(products);
        }

        public string GetOrbitsForDivison(int id)
        {
            // get the products from the repository 
            var divCode = (from h in db.Divisions
                           where h.DivisionID.Equals(id)
                           select h.TMWCode).SingleOrDefault();
            var products = new SelectList(db.Orbits.Where(p => p.TMWCode == divCode).OrderBy(i => i.OrbitName), "OrbitID", "OrbitName");

            return new JavaScriptSerializer().Serialize(products);
        }


        public string GetPayrollsForDriver(int id)
        {
            var divCode = (from h in db.DriverTypes
                           where h.DriverTypeID.Equals(id)
                           select h.TMWCode).SingleOrDefault();

            SelectList products;

            if (divCode == "OO" || divCode == "OD")
            {

                products = new SelectList(db.DriverPayrolls.Where(p => p.PayrollName.Contains("Brokers")).OrderBy(i => i.PayrollName), "PayrollID2", "PayrollName");
            }
            else if (divCode == "CD")
            {

                products = new SelectList(db.DriverPayrolls.Where(p => p.PayrollName.Contains("Drivers")).OrderBy(i => i.PayrollName), "PayrollID2", "PayrollName");
            }
            else
            {
                products = new SelectList(db.DriverPayrolls.OrderBy(i => i.PayrollName), "PayrollID2", "PayrollName");

            }


            return new JavaScriptSerializer().Serialize(products);
        }


        public string GetDomicilesForDivison(int id)
        {
            // get the products from the repository 
            var divCode = (from h in db.Divisions
                           where h.DivisionID.Equals(id)
                           select h.TMWCode).SingleOrDefault();
            var products = new SelectList(db.Domiciles.Where(p => p.TMWCode2 == divCode).OrderBy(i => i.DomicileName), "DomicileID", "DomicileName");

            return new JavaScriptSerializer().Serialize(products);
        }
      

        public string GetBillTosForDivison(int id)
        {
            // get the products from the repository 
            var divCode = (from h in db.Divisions
                           where h.DivisionID.Equals(id)
                           select h.TMWCode).SingleOrDefault();
            var products = new SelectList(db.BillTos.Where(p => p.DivisionCode == divCode).OrderBy(i => i.BillToName), "BillToID", "DropDownName");

            return new JavaScriptSerializer().Serialize(products);
        }


        public string GetTerminalsForDivison(int id)
        {
            // get the products from the repository 
            var divCode = (from h in db.Divisions
                           where h.DivisionID.Equals(id)
                           select h.TMWCode).SingleOrDefault();
            var products = new SelectList(db.Terminals.Where(p => p.DivisionCode == divCode).OrderBy(i => i.TerminalName), "TerminalID", "TerminalName");

            return new JavaScriptSerializer().Serialize(products);
        }


       public string GetCitiesForProvince(int id)
        {
            // get the products from the repository 

            var products = new SelectList(db.Cities.Where(p => p.ProvinceID == id).OrderBy(i => i.CityName), "CityID", "CityName");

            return new JavaScriptSerializer().Serialize(products);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}