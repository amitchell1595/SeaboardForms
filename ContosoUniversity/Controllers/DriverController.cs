using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using RazorEngine;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Serialization;
using StackExchange.Profiling;

namespace ContosoUniversity.Controllers
{

    public class DriverController : Controller
    {
        private MDMSContext db = new MDMSContext();
        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = db.Drivers.Include(u => u.DriverChangeType).Where(x => x.AddedToTMW == false || x.Pending == false || x.Approved == false);


            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.DriverFirstName.Contains(searchString)).Where(x => x.AddedToTMW == false || x.Pending == false || x.Approved == false);

            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.DriverFirstName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.HireDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.HireDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.DriverFirstName);
                    break;
            }





            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult History(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = db.Drivers.Include(u => u.DriverChangeType);


            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.DriverFirstName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.DriverFirstName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.HireDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.HireDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.DriverFirstName);
                    break;
            }





            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        //// GET: Sites
        //public ActionResult Index()
        //{
        //    return View(db.Sites.ToList());
        //}

        // GET: Sites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver site = db.Drivers.Include(i => i.DriverChangeType)
              .SingleOrDefault(x => x.ID == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        public ActionResult Thanks()
        {
   

            return View();
        }

        // GET: Sites/Create
        public ActionResult Create()
        {

                var s = new Driver()
                {
                    Terminals = new SelectList(db.DriverTerminals, "TerminalID", "DropdownName"),
                    CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = User.Identity.Name,
                    DriverEffectiveDate = DateTime.Now,
                    BirthDate = DateTime.Now,
                    HireDate = DateTime.Now,
                    OrientationStart = DateTime.Now,
                    OrientationEnd = DateTime.Now,
                    LastDayWorked = DateTime.Now,
                    ExitDate = DateTime.Now,
                    ExitReasons = new SelectList(db.ExitReasons, "ExitReasonID", "ExitReasonName"),
                    Countries = new SelectList(db.Countries, "CountryID", "CountryName"),
                    Cities = new SelectList(db.Cities, "CityID", "CityName"),
                    Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName"),
                    Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName"),
                    Entities = new SelectList(db.Entities, "EntityID", "EntityName"),
                    SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName"),
                    ChangeTypes = new SelectList(db.DriverChangeTypes, "DriverChangeTypeID", "DriverChangeTypeName"),
                    DriverTypes = new SelectList(db.DriverTypes, "DriverTypeID", "DriverTypeName"),
                    DriverPayrolls = new SelectList(db.DriverPayrolls, "PayrollID2", "PayrollName"),
                    PayLevels = new SelectList(db.DriverPayLevels, "PayLevelID", "PayLevelName"),
                    Orbits = new SelectList(db.Domiciles, "DomicileID", "DomicileName"),
                    Name = "Driver"

                };





                return View(s);
            
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExclusiveActionDriver]
        public ActionResult Create([Bind(Include = "DriverChangeTypeID, DriverEffectiveDate, DriverID, DriverTypeID, OOBusinessNumber, BrokerUnitNumber, DriverPayrollID, DriverFirstName, DriverLastName,  PayLevelID, Address, PostalCode, CountryID,ProvinceID,LicenseProvinceID,CityID,HomePhone,CellPhone,EMail,EmergencyName, EmergencyNumber, LicenseClass, LicenseNumber, EntityID, DivisionID,DomicileID,TerminalID,BirthDate,HireDate,OrientationStart,OrientationEnd,LastDayWorked,ExitDate,ExitReasonID,Comments")] Driver site)
        {


            site.CreatedDate = DateTime.Now;
            site.CreatedBy = User.Identity.Name;
            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;
            site.Name = "Driver";
            if (site.DriverChangeTypeID == 0 || site.DriverChangeTypeID == null)
            {
                ModelState.AddModelError("DriverChangeTypeID", "Please select a change type.");

            }
            else
            {
                if (site.DriverChangeTypeID == 1)
                {


                    if (site.DriverPayrollID == null || site.DriverPayrollID == 0)
                    {
                        ModelState.AddModelError("DriverPayrollID", "Please select a payroll.");

                    }
                    if (site.DriverTypeID == null)
                    {
                        ModelState.AddModelError("DriverTypeID", "Please select a driver type.");

                    }
                    if (site.DriverFirstName == null || site.DriverLastName == null)
                    {
                        ModelState.AddModelError("DriverFirstName", "Please enter the driver name.");

                    }
                    if (site.Address == null)
                    {
                        ModelState.AddModelError("Address", "Please enter the address.");

                    }
                    if (site.CountryID == null)
                    {
                        ModelState.AddModelError("CountryID", "Please select a Country.");

                    }
                    if (site.ProvinceID == null)
                    {
                        ModelState.AddModelError("ProvinceID", "Please select a Province.");

                    }
                    if (site.CityID == null)
                    {
                        ModelState.AddModelError("CityID", "Please select a City.");

                    }
                    if (site.EntityID == null)
                    {
                        ModelState.AddModelError("EntityID", "Please select an Entity.");

                    }
                    if (site.DivisionID == null)
                    {
                        ModelState.AddModelError("DivisionID", "Please select a Division.");

                    }
                    if (site.DomicileID == null)
                    {
                        ModelState.AddModelError("DomicileID", "Please select an Orbit.");

                    }
                    if (site.TerminalID == null)
                    {
                        ModelState.AddModelError("TerminalID", "Please select a terminal.");

                    }
                    if (site.LicenseProvinceID == null)
                    {
                        ModelState.AddModelError("LicenseProvinceID", "Please select a license province.");

                    }

                    site.ExitDate = DateTime.MaxValue;
                    site.LastDayWorked = DateTime.MaxValue;
                    site.ExitReason = null;
                 
                }
                else if (site.DriverChangeTypeID == 4)
                {
                    if (site.DriverID == null)
                    {
                        ModelState.AddModelError("DriverID", "Please enter the driver ID to exit.");

                    }
                    if (site.DriverFirstName == null || site.DriverLastName == null)
                    {
                        ModelState.AddModelError("DriverFirstName", "Please enter the driver name.");

                    }
                    if (site.ExitReasonID == null || site.ExitReasonID == 0)
                    {
                        ModelState.AddModelError("ExitReasonID", "Please select an exit reason for this driver.");

                    }
                    if (site.DriverTypeID == null)
                    {
                        ModelState.AddModelError("DriverTypeID", "Please select a driver type.");

                    }
                    if (site.EntityID == null)
                    {
                        ModelState.AddModelError("EntityID", "Please select an Entity.");

                    }
                    if (site.DivisionID == null)
                    {
                        ModelState.AddModelError("DivisionID", "Please select a Division.");

                    }
                    if (site.DomicileID == null)
                    {
                        ModelState.AddModelError("DomicileID", "Please select an Orbit.");

                    }

                    site.BirthDate = DateTime.MaxValue;
                    site.HireDate = DateTime.MaxValue;
                    site.OrientationStart = DateTime.MaxValue;
                    site.OrientationEnd = DateTime.MaxValue;

                }
                else if (site.DriverChangeTypeID == 2) {
                    if (site.DriverID == null)
                    {
                        ModelState.AddModelError("DriverID", "Please enter the driver ID.");

                    }
                    if (site.DriverFirstName == null || site.DriverLastName == null)
                    {
                        ModelState.AddModelError("DriverFirstName", "Please enter the driver name.");

                    }
                    if (site.DriverTypeID == null)
                    {
                        ModelState.AddModelError("DriverTypeID", "Please select a driver type.");

                    }
                    if (site.EntityID == null)
                    {
                        ModelState.AddModelError("EntityID", "Please select an Entity.");

                    }
                    if (site.DivisionID == null)
                    {
                        ModelState.AddModelError("DivisionID", "Please select a Division.");

                    }
                    if (site.DomicileID == null)
                    {
                        ModelState.AddModelError("DomicileID", "Please select an Orbit.");

                    }

                    if (site.DriverPayrollID == null || site.DriverPayrollID == 0)
                    {
                        ModelState.AddModelError("DriverPayrollID", "Please select a payroll.");

                    }


                    site.ExitDate = DateTime.MaxValue;
                    site.LastDayWorked = DateTime.MaxValue;
                    site.ExitReason = null;

                    site.BirthDate = DateTime.MaxValue;
                    site.HireDate = DateTime.MaxValue;
                    site.OrientationStart = DateTime.MaxValue;
                    site.OrientationEnd = DateTime.MaxValue;
                
                
                }
                else if (site.DriverChangeTypeID == 3)
                {
                    if (site.DriverID == null)
                    {
                        ModelState.AddModelError("DriverID", "Please enter the driver ID.");

                    }
                    if (site.DriverFirstName == null || site.DriverLastName == null)
                    {
                        ModelState.AddModelError("DriverFirstName", "Please enter the driver name.");

                    }
                    if (site.CountryID == null)
                    {
                        ModelState.AddModelError("CountryID", "Please select a Country.");

                    }
                    if (site.ProvinceID == null)
                    {
                        ModelState.AddModelError("ProvinceID", "Please select a Province.");

                    }
                    if (site.CityID == null)
                    {
                        ModelState.AddModelError("CityID", "Please select a City.");

                    }


                    site.ExitDate = DateTime.MaxValue;
                    site.LastDayWorked = DateTime.MaxValue;
                    site.ExitReason = null;

                    site.BirthDate = DateTime.MaxValue;
                    site.HireDate = DateTime.MaxValue;
                    site.OrientationStart = DateTime.MaxValue;
                    site.OrientationEnd = DateTime.MaxValue;


                }
                else
                {

                    if (site.DriverID == null)
                    {
                        ModelState.AddModelError("DriverID", "Please enter the driver ID.");

                    }
                    if (site.DriverFirstName == null || site.DriverLastName == null)
                    {
                        ModelState.AddModelError("DriverFirstName", "Please enter the driver name.");

                    }

                    site.ExitDate = DateTime.MaxValue;
                    site.LastDayWorked = DateTime.MaxValue;
                    site.ExitReason = null;

                    site.BirthDate = DateTime.MaxValue;
                    site.HireDate = DateTime.MaxValue;
                    site.OrientationStart = DateTime.MaxValue;
                    site.OrientationEnd = DateTime.MaxValue;

                }

            }

            if (ModelState.IsValid)
            {



                db.Drivers.Add(site);
                db.SaveChanges();

                
                var x = db.Drivers.Include(s => s.DriverPayroll).Include(s => s.PayLevel).Include(s => s.Country).Include(s => s.Province).Include(s => s.City)
                    .Include(s => s.DriverChangeType).Include(s => s.DriverType).Include(s => s.LicenseProvince).Include(s => s.Entity).Include(s => s.Division)
                    .Include(s => s.Domicile).Include(s => s.Terminal).Include(s => s.ExitReason)
                        .Where(c => c.ID == site.ID).FirstOrDefault();

                   
                SubmittedMail(x);
                return RedirectToAction("Thanks");
               
            }

            site.Terminals = new SelectList(db.DriverTerminals, "TerminalID", "DropdownName");
            site.Countries = new SelectList(db.Countries, "CountryID", "CountryName");
                site.Cities = new SelectList(db.Cities, "CityID", "CityName");
                site.Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
                site.Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName");
                site.Entities = new SelectList(db.Entities, "EntityID", "EntityName");
                site.BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName");
                site.SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
                site.Terminals = new SelectList(db.DriverTerminals, "TerminalID", "DropdownName");
                site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
                site.Orbits = new SelectList(db.Domiciles, "DomicileID", "DomicileName");
                site.PayLevels = new SelectList(db.DriverPayLevels, "PayLevelID", "PayLevelName");
                site.ExitReasons = new SelectList(db.ExitReasons, "ExitReasonID", "ExitReasonName");
                site.ChangeTypes = new SelectList(db.DriverChangeTypes, "DriverChangeTypeID", "DriverChangeTypeName");
                site.DriverTypes = new SelectList(db.DriverTypes, "DriverTypeID", "DriverTypeName");
                site.DriverPayrolls = new SelectList(db.DriverPayrolls, "PayrollID2", "PayrollName");

            return View(site);
        }

        // GET: Sites/Edit/5
        public ActionResult Edit(int? id)
        {
   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver site = db.Drivers.Find(id);

            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;
            site.Countries = new SelectList(db.Countries, "CountryID", "CountryName");
            site.Cities = new SelectList(db.Cities, "CityID", "CityName");
            site.Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
            site.Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName");
            site.Entities = new SelectList(db.Entities, "EntityID", "EntityName");
            site.BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName");
            site.SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
            site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
            site.Orbits = new SelectList(db.Domiciles, "DomicileID", "DomicileName");
            site.Terminals = new SelectList(db.DriverTerminals, "TerminalID", "DropdownName");

            site.ExitReasons = new SelectList(db.ExitReasons, "ExitReasonID", "ExitReasonName");
            site.Countries = new SelectList(db.Countries, "CountryID", "CountryName");
            site.Cities = new SelectList(db.Cities, "CityID", "CityName");
            site.Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
            site.Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName");
            site.Entities = new SelectList(db.Entities, "EntityID", "EntityName");
            site.BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName");
            site.SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
            site.ChangeTypes = new SelectList(db.DriverChangeTypes, "DriverChangeTypeID", "DriverChangeTypeName");
            site.DriverTypes = new SelectList(db.DriverTypes, "DriverTypeID", "DriverTypeName");
            site.DriverPayrolls = new SelectList(db.DriverPayrolls, "PayrollID2", "PayrollName");
            site.PayLevels = new SelectList(db.DriverPayLevels, "PayLevelID", "PayLevelName");
            site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
           

            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var site = db.Drivers.Include(i => i.DriverChangeType)
              .SingleOrDefault(x => x.ID == id); 
            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;

            

            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;

            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;
            site.Countries = new SelectList(db.Countries, "CountryID", "CountryName");
            site.Cities = new SelectList(db.Cities, "CityID", "CityName");
            site.Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
            site.Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName");
            site.Entities = new SelectList(db.Entities, "EntityID", "EntityName");
            site.BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName");
            site.SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
            site.Terminals = new SelectList(db.DriverTerminals, "TerminalID", "DropdownName");
            site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
            site.Orbits = new SelectList(db.Domiciles, "DomicileID", "DomicileName");

             
            if (TryUpdateModel(site, "", new string[] { "DriverChangeTypeID", "DriverID", "RejectReason", "Approved", "DriverTypeID", "OOBusinessNumber", "BrokerUnitNumber", "DriverPayrollID", "DriverFirstName", "DriverLastName", "PayLevelID", "Address", "PostalCode", "CountryID", "ProvinceID", "LicenseProvinceID", "CityID", "HomePhone", "CellPhone", "EMail", "EmergencyName", "EmergencyNumber", "LicenseClass", "LicenseNumber", "EntityID", "DivisionID", "DomicileID", "TerminalID", "BirthDate", "HireDate", "OrientationStart", "OrientationEnd", "LastDayWorked", "ExitDate", "ExitReasonID", "Comments" }))
            {
                try
                {
                    db.SaveChanges();
                    if (site.Approved) {
                        ApproveDriver(site);

                        var x = db.Drivers.Include(s => s.DriverPayroll).Include(s => s.PayLevel).Include(s => s.Country).Include(s => s.Province).Include(s => s.City)
    .Include(s => s.DriverChangeType).Include(s => s.DriverType).Include(s => s.LicenseProvince).Include(s => s.Entity).Include(s => s.Division)
    .Include(s => s.Domicile).Include(s => s.Terminal).Include(s => s.ExitReason)
        .Where(c => c.ID == site.ID).FirstOrDefault();

                        DistributeDriver(x);
                    }
                    else if (site.RejectReason != "" && site.RejectReason != null)
                    {

                        RejectDriver(site);


                    }
                    else { 
                    
                    }


                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(site);
        }
        //public void AddToTMW() {

        //    string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

        //    SqlConnection cnn = new SqlConnection(cnnString);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = cnn;
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = "zsp_InsertCustomerIntoTMW";
        //    //add any parameters the stored procedure might require
        //    cnn.Open();
        //    object o = cmd.ExecuteScalar();
        //    cnn.Close();
        //}


        // GET: Sites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver site = db.Drivers.Include(i => i.DriverChangeType)
              .SingleOrDefault(x => x.ID == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }
        private void SubmittedMail(Driver u)
        {
            string subject = ""; 

            MailDefinition md = new MailDefinition();
            string[] pieces = u.CreatedBy.Split('\\');
            md.From = pieces[1] + "@seaboard.acl.ca";
            md.IsBodyHtml = true;
            

            ListDictionary replacements = new ListDictionary();

            string body = "";

            if (u.DriverChangeTypeID == 1 && u.PayLevelID != null)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.HireDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Birth Date: @Model.BirthDate.ToShortDateString()  <br/> 
Hire Date: @Model.HireDate.ToShortDateString()  <br/> 
Orientation Start: @Model.OrientationStart.ToShortDateString()  <br/> 
Orientation End: @Model.OrientationStart.ToShortDateString()  <br/> 
<br/>

<b><u>Payroll Information</u></b> <br/>
Payroll: @Model.DriverPayroll.PayrollName <br/>
Pay Level: @Model.PayLevel.PayLevelName <br/>  
OO Business #: @Model.OOBusinessNumber <br/> <br/>

<b><u>Address Information</u></b> <br/>
Address: @Model.Address  <br/>
Postal Code: @Model.PostalCode  <br/>
Country: @Model.Country.CountryName  <br/>
Province: @Model.Province.ProvinceName  <br/>
City: @Model.City.CityName  <br/>  <br/>

<b><u>Contact Information</u></b> <br/>
Home Phone: @Model.HomePhone <br/>
Cell Phone: @Model.CellPhone  <br/>
Email: @Model.Email  <br/>
Emergency Number: @Model.EmergencyNumber <br/>
Emergency Name: @Model.EmergencyName  <br/>  <br/>

<b><u>License Information</u></b> <br/>
License Province: @Model.LicenseProvince.ProvinceName <br/>
License Class: @Model.LicenseClass <br/>
License #: @Model.LicenseNumber <br/>  <br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/>
Terminal: @Model.Terminal.TerminalName <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                
Please click the following link to access this request:
                <a href='http://seatranweb01:8080/Driver/Edit/@Model.ID'>http://seatranweb01:8080/Driver/Edit/@Model.ID</a>

                </body>
                </html>";

            }
            else if (u.DriverChangeTypeID == 1) {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.HireDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>
Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Birth Date: @Model.BirthDate.ToShortDateString()  <br/> 
Hire Date: @Model.HireDate.ToShortDateString()  <br/> 
Orientation Start: @Model.OrientationStart.ToShortDateString()  <br/> 
Orientation End: @Model.OrientationStart.ToShortDateString()  <br/> 
<br/>

<b><u>Payroll Information</u></b> <br/>
Payroll: @Model.DriverPayroll.PayrollName <br/>
OO Business #: @Model.OOBusinessNumber <br/> <br/>

<b><u>Address Information</u></b> <br/>
Address: @Model.Address  <br/>
Postal Code: @Model.PostalCode  <br/>
Country: @Model.Country.CountryName  <br/>
Province: @Model.Province.ProvinceName  <br/>
City: @Model.City.CityName  <br/>  <br/>

<b><u>Contact Information</u></b> <br/>
Home Phone: @Model.HomePhone <br/>
Cell Phone: @Model.CellPhone  <br/>
Email: @Model.Email  <br/>
Emergency Number: @Model.EmergencyNumber <br/>
Emergency Name: @Model.EmergencyName  <br/>  <br/>

<b><u>License Information</u></b> <br/>
License Province: @Model.LicenseProvince.ProvinceName <br/>
License Class: @Model.LicenseClass <br/>
License #: @Model.LicenseNumber <br/>  <br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/>
Terminal: @Model.Terminal.TerminalName <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                
Please click the following link to access this request:
                <a href='http://seatranweb01:8080/Driver/Edit/@Model.ID'>http://seatranweb01:8080/Driver/Edit/@Model.ID</a>

                </body>
                </html>";
            
            }
            else if (u.DriverChangeTypeID == 4)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.ExitDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Last Day Worked:  @Model.LastDayWorked.ToShortDateString()  <br/> 
Exit Date:   @Model.ExitDate.ToShortDateString() <br/> 
Exit Reason:  @Model.ExitReason.ExitReasonName <br/> 


<br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/> <br/> 

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                
Please click the following link to access this request:
                <a href='http://seatranweb01:8080/Driver/Edit/@Model.ID'>http://seatranweb01:8080/Driver/Edit/@Model.ID</a>

                </body>
                </html>";

            }
            else if (u.DriverChangeTypeID == 2)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.DriverEffectiveDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Effective Date: @Model.DriverEffectiveDate.ToShortDateString() <br/>  <br/>

<b><u>Payroll Information</u></b> <br/>
Payroll: @Model.DriverPayroll.PayrollName <br/> 
OO Business #: @Model.OOBusinessNumber <br/> <br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/> <br/> 

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                
Please click the following link to access this request:
                <a href='http://seatranweb01:8080/Driver/Edit/@Model.ID'>http://seatranweb01:8080/Driver/Edit/@Model.ID</a>

                </body>
                </html>";

            }

            else if (u.DriverChangeTypeID == 3)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.DriverEffectiveDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Effective Date: @Model.DriverEffectiveDate.ToShortDateString() <br/>  <br/>


<b><u>Address Information</u></b> <br/>
Address: @Model.Address  <br/>
Postal Code: @Model.PostalCode  <br/>
Country: @Model.Country.CountryName  <br/>
Province: @Model.Province.ProvinceName  <br/>
City: @Model.City.CityName  <br/>  <br/>

<b><u>Contact Information</u></b> <br/>
Home Phone: @Model.HomePhone <br/>
Cell Phone: @Model.CellPhone  <br/>
Email: @Model.Email  <br/>
Emergency Number: @Model.EmergencyNumber <br/>
Emergency Name: @Model.EmergencyName  <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                
Please click the following link to access this request:
                <a href='http://seatranweb01:8080/Driver/Edit/@Model.ID'>http://seatranweb01:8080/Driver/Edit/@Model.ID</a>

                </body>
                </html>";

            }
            else
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.DriverEffectiveDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/>  
Effective Date: @Model.DriverEffectiveDate <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate <br/><br/><br/>
                
Please click the following link to access this request:
                <a href='http://seatranweb01:8080/Driver/Edit/@Model.ID'>http://seatranweb01:8080/Driver/Edit/@Model.ID</a>

                </body>
                </html>";

            }
            md.Subject = Razor.Parse(subject, u);
            string mailBody = Razor.Parse(body, u);
            MailMessage msg = md.CreateMailMessage("support@seaboard.acl.ca", replacements, mailBody, new System.Web.UI.Control());

            //Send the message.
            SmtpClient client = new SmtpClient("aclexchange.acl.ca");
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            client.Send(msg);

        }

        public ActionResult AddToNewTMW(int id)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_InsertDriverIntoTMWNew";
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            //Response.Write("<script>alert('Added to TMW');</script>");

            return RedirectToAction("Index");
        
        }

        public ActionResult SendSupportEmail(int id)
        {
            var x = db.Drivers.Include(s => s.DriverPayroll).Include(s => s.PayLevel).Include(s => s.Country).Include(s => s.Province).Include(s => s.City)
.Include(s => s.DriverChangeType).Include(s => s.DriverType).Include(s => s.LicenseProvince).Include(s => s.Entity).Include(s => s.Division)
.Include(s => s.Domicile).Include(s => s.Terminal).Include(s => s.ExitReason)
.Where(c => c.ID == id).FirstOrDefault();

            SubmittedMail(x);

            return RedirectToAction("Index");

        }

        public void ApproveDriver(Driver u)
        {
            string subject = "";

            MailDefinition md = new MailDefinition();
            string[] pieces = u.CreatedBy.Split('\\');
            var to = pieces[1] + "@seaboard.acl.ca";
            md.From = "support@seaboard.acl.ca";
            md.IsBodyHtml = true;


            ListDictionary replacements = new ListDictionary();

            string body = "";


            subject = @"Your Driver Change Form for @Model.DriverFirstName @Model.DriverLastName has been Approved!";
            body =
               @"<html>
                <body>
                The below driver change form has been approved and sent to the DriverChange DL:
                <br/><br/>

<b><u>Driver Information</u></b><br/>
Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> <br/>

               

                </body>
                </html>";


            db.SaveChanges();
            md.Subject = Razor.Parse(subject, u);
            string mailBody = Razor.Parse(body, u);
            MailMessage msg = md.CreateMailMessage(to, replacements, mailBody, new System.Web.UI.Control());

            //Send the message.
            SmtpClient client = new SmtpClient("aclexchange.acl.ca");
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            client.Send(msg);
          





        }

        public ActionResult RejectDriver(Driver u)
        {
            string subject = "";

            MailDefinition md = new MailDefinition();
            string[] pieces = u.CreatedBy.Split('\\');
            var to = pieces[1] + "@seaboard.acl.ca";
            md.From = "support@seaboard.acl.ca";
            md.IsBodyHtml = true;


            ListDictionary replacements = new ListDictionary();

            string body = "";


                subject = @"Your Driver Change Form for @Model.DriverFirstName @Model.DriverLastName has been Rejected";
                body =
                   @"<html>
                <body>
                The below driver change form has been rejected for the following reason:
                <br/>
                @Model.RejectReason 
                <br/><br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> <br/>

                
Please click the following link to resubmit your request:
                <a href='http://seatranweb01:8080/Driver/Create'>http://seatranweb01:8080/Driver/Create</a>

                </body>
                </html>";

       
                        db.SaveChanges();
                        md.Subject = Razor.Parse(subject, u);
                        string mailBody = Razor.Parse(body, u);
                        MailMessage msg = md.CreateMailMessage(to, replacements, mailBody, new System.Web.UI.Control());

                        //Send the message.
                        SmtpClient client = new SmtpClient("aclexchange.acl.ca");
                        // Add credentials if the SMTP server requires them.
                        client.Credentials = CredentialCache.DefaultNetworkCredentials;

                        client.Send(msg);
                        return RedirectToAction("Index");
       
              

          
        
        }
        public ActionResult DistributeDriver(Driver u)
        {
            string subject = "";

            MailDefinition md = new MailDefinition();
            string[] pieces = u.CreatedBy.Split('\\');
            md.From =  "support@seaboard.acl.ca";
            md.IsBodyHtml = true;


            ListDictionary replacements = new ListDictionary();

            string body = "";

            if (u.DriverChangeTypeID == 1 && u.PayLevelID != null)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.HireDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted and approved, please see the details below.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Birth Date: @Model.BirthDate.ToShortDateString()  <br/> 
Hire Date: @Model.HireDate.ToShortDateString()  <br/> 
Orientation Start: @Model.OrientationStart.ToShortDateString()  <br/> 
Orientation End: @Model.OrientationStart.ToShortDateString()  <br/> 
<br/>

<b><u>Payroll Information</u></b> <br/>
Payroll: @Model.DriverPayroll.PayrollName <br/>
Pay Level: @Model.PayLevel.PayLevelName <br/>  
OO Business #: @Model.OOBusinessNumber <br/> <br/>

<b><u>Address Information</u></b> <br/>
Address: @Model.Address  <br/>
Postal Code: @Model.PostalCode  <br/>
Country: @Model.Country.CountryName  <br/>
Province: @Model.Province.ProvinceName  <br/>
City: @Model.City.CityName  <br/>  <br/>

<b><u>Contact Information</u></b> <br/>
Home Phone: @Model.HomePhone <br/>
Cell Phone: @Model.CellPhone  <br/>
Email: @Model.Email  <br/>
Emergency Number: @Model.EmergencyNumber <br/>
Emergency Name: @Model.EmergencyName  <br/>  <br/>

<b><u>License Information</u></b> <br/>
License Province: @Model.LicenseProvince.ProvinceName <br/>
License Class: @Model.LicenseClass <br/>
License #: @Model.LicenseNumber <br/>  <br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/>
Terminal: @Model.Terminal.TerminalName <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                

                </body>
                </html>";

            }
            else if (u.DriverChangeTypeID == 1)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.HireDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted and approved, please see the details below.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>
Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Birth Date: @Model.BirthDate.ToShortDateString()  <br/> 
Hire Date: @Model.HireDate.ToShortDateString()  <br/> 
Orientation Start: @Model.OrientationStart.ToShortDateString()  <br/> 
Orientation End: @Model.OrientationStart.ToShortDateString()  <br/> 
<br/>

<b><u>Payroll Information</u></b> <br/>
Payroll: @Model.DriverPayroll.PayrollName <br/>
OO Business #: @Model.OOBusinessNumber <br/> <br/>

<b><u>Address Information</u></b> <br/>
Address: @Model.Address  <br/>
Postal Code: @Model.PostalCode  <br/>
Country: @Model.Country.CountryName  <br/>
Province: @Model.Province.ProvinceName  <br/>
City: @Model.City.CityName  <br/>  <br/>

<b><u>Contact Information</u></b> <br/>
Home Phone: @Model.HomePhone <br/>
Cell Phone: @Model.CellPhone  <br/>
Email: @Model.Email  <br/>
Emergency Number: @Model.EmergencyNumber <br/>
Emergency Name: @Model.EmergencyName  <br/>  <br/>

<b><u>License Information</u></b> <br/>
License Province: @Model.LicenseProvince.ProvinceName <br/>
License Class: @Model.LicenseClass <br/>
License #: @Model.LicenseNumber <br/>  <br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/>
Terminal: @Model.Terminal.TerminalName <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                

                </body>
                </html>";

            }
            else if (u.DriverChangeTypeID == 4)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.ExitDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted and approved, please see the details below.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Last Day Worked:  @Model.LastDayWorked.ToShortDateString()  <br/> 
Exit Date:   @Model.ExitDate.ToShortDateString() <br/> 
Exit Reason:  @Model.ExitReason.ExitReasonName <br/> 


<br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/> <br/> 

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                

                </body>
                </html>";

            }
            else if (u.DriverChangeTypeID == 2)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.DriverEffectiveDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted and approved, please see the details below.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver Type:  @Model.DriverType.DriverTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Effective Date: @Model.DriverEffectiveDate.ToShortDateString() <br/>  <br/>

<b><u>Payroll Information</u></b> <br/>
Payroll: @Model.DriverPayroll.PayrollName <br/> 
OO Business #: @Model.OOBusinessNumber <br/> <br/>


<b><u>TMW Information</u></b> <br/>
Entity: @Model.Entity.EntityName <br/>
Division: @Model.Division.DivisionName <br/>
Orbit: @Model.Domicile.DomicileName <br/> <br/> 

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                

                </body>
                </html>";

            }

            else if (u.DriverChangeTypeID == 3)
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.DriverEffectiveDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted and approved, please see the details below.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/> 
Effective Date: @Model.DriverEffectiveDate.ToShortDateString() <br/>  <br/>


<b><u>Address Information</u></b> <br/>
Address: @Model.Address  <br/>
Postal Code: @Model.PostalCode  <br/>
Country: @Model.Country.CountryName  <br/>
Province: @Model.Province.ProvinceName  <br/>
City: @Model.City.CityName  <br/>  <br/>

<b><u>Contact Information</u></b> <br/>
Home Phone: @Model.HomePhone <br/>
Cell Phone: @Model.CellPhone  <br/>
Email: @Model.Email  <br/>
Emergency Number: @Model.EmergencyNumber <br/>
Emergency Name: @Model.EmergencyName  <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate.ToShortDateString() <br/><br/><br/>
                

                </body>
                </html>";

            }
            else
            {
                subject = @"Driver Change Form  Submitted for @Model.DriverFirstName @Model.DriverLastName - @Model.DriverChangeType.DriverChangeTypeName @Model.DriverEffectiveDate.ToShortDateString()";
                body =
                   @"<html>
                <body>
                A driver change form has been submitted and approved, please see the details below.
                <br/>
                <br/>

<b><u>Driver Information</u></b><br/>

Change Type:  @Model.DriverChangeType.DriverChangeTypeName<br/>
Driver ID:    @Model.DriverID<br/>
Name: @Model.DriverFirstName @Model.DriverLastName <br/>  
Effective Date: @Model.DriverEffectiveDate <br/>  <br/>

<b><u>Additional Information</u></b> <br/>
Comments: @Model.Comments <br/>
Submitted By: @Model.CreatedBy <br/>
Submitted Time: @Model.CreatedDate <br/><br/><br/>
                

                </body>
                </html>";

            }
            md.Subject = Razor.Parse(subject, u);
            string mailBody = Razor.Parse(body, u);
            MailMessage msg = md.CreateMailMessage("driverchange@seaboard.acl.ca", replacements, mailBody, new System.Web.UI.Control());

            //Send the message.
            SmtpClient client = new SmtpClient("aclexchange.acl.ca");
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            client.Send(msg);
                        return RedirectToAction("Index");
       
              

          
        
        }
        
        public string GetTMWInfoDriver(string id)
        {
            string value1 = null;
            string value2 = null;
            string value3 = null;
            string value4 = null;
            string value5 = null;
            string value6 = null;
            string value7 = null;
            string value8 = null;
            string value9 = null;
            string value10 = null;
            string value11 = null;
            string value12 = null;
            int value13 = 0;
            int value14 = 0;
            int value15 = 0;
            string value16 = null;
            string value17 = null;
            string value18 = null;
            int value19 = 0;
            int value20 = 0;
            int value21 = 0;
            string value22 = null;
            int value23 = 0;
            string value24 = null;
            string value25 = null;
            int value26 = 0;
            int value27 = 0;

            if (id != null)
            {
                string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;


                var result = new StringBuilder();


                using (var cnnn = new SqlConnection(cnnString))
                {
                    string CommandText2 = "select * from TMW_STD.dbo.manpowerprofile where mpp_id = '" + id + "'";
                    SqlCommand command2 = new SqlCommand(CommandText2, cnnn);
                    cnnn.Open();

                    using (var reader = command2.ExecuteReader())
                    {
   
                        while (reader.Read())
                        {
                            value1 = reader.GetValue(reader.GetOrdinal("mpp_id")).ToString();
                            value2 = reader.GetValue(reader.GetOrdinal("mpp_firstname")).ToString();
                            value3 = reader.GetValue(reader.GetOrdinal("mpp_lastname")).ToString();
                            value4 = reader.GetValue(reader.GetOrdinal("mpp_address1")).ToString();
                            value5 = reader.GetValue(reader.GetOrdinal("mpp_zip")).ToString();
                            value6 = reader.GetValue(reader.GetOrdinal("mpp_state")).ToString();
                            value7 = reader.GetValue(reader.GetOrdinal("mpp_city")).ToString();
                            value8 = reader.GetValue(reader.GetOrdinal("mpp_homephone")).ToString();
                            value9 = reader.GetValue(reader.GetOrdinal("mpp_currentphone")).ToString();
                            value10 = reader.GetValue(reader.GetOrdinal("mpp_email")).ToString();
                            value11 = reader.GetValue(reader.GetOrdinal("mpp_emerphone")).ToString();
                            value12 = reader.GetValue(reader.GetOrdinal("mpp_emername")).ToString();

                            value16 = reader.GetValue(reader.GetOrdinal("mpp_division")).ToString();
                            value17 = reader.GetValue(reader.GetOrdinal("mpp_company")).ToString();
                            value18 = reader.GetValue(reader.GetOrdinal("mpp_domicile")).ToString();

                            value22 = reader.GetValue(reader.GetOrdinal("mpp_type1")).ToString();
                            value24 = reader.GetValue(reader.GetOrdinal("mpp_type2")).ToString();
                            value25 = reader.GetValue(reader.GetOrdinal("mpp_type3")).ToString();
                        }
                    }
                }
            }

            if (value1 != null)
            {
                //Country ID
                value13 = db.Provinces.SingleOrDefault(x => x.ProvinceAbbr == value6).CountryID;

                //Province ID
                value14 = db.Provinces.SingleOrDefault(x => x.ProvinceAbbr == value6).ProvinceID;

                //City ID
                value15 = db.Cities.SingleOrDefault(x => x.TMWCode == value7).CityID;

                if (value16 != "UNK" && value16 != null)
                {
                    value19 = db.Divisions.SingleOrDefault(x => x.TMWCode == value16).DivisionID;

                }

                if (value17 != "UNK" && value17 != null)
                {
                    value20 = db.Entities.SingleOrDefault(x => x.TMWCode == value17).EntityID;

                }
                if (value18 != "UNK" && value18 != null)
                {
                    value21 = db.Domiciles.SingleOrDefault(x => x.TMWCode == value18).DomicileID;

                }

                if (value22 != "UNK" && value22 != null)
                {
                    value23 = db.DriverTypes.SingleOrDefault(x => x.TMWCode == value22).DriverTypeID;

                }
                if (value24 != "UNK" && value24 != null)
                {
                    value26 = db.DriverPayLevels.SingleOrDefault(x => x.TMWCode == value24).PayLevelID;

                }

                if (value25 != "UNK" && value25 != null)
                {
                    value27 = 0;

                }

            }
 


            List<SelectListItem> dInfo = new List<SelectListItem>()
                {
                     new SelectListItem() {Text=value2, Value="mpp_firstname"},
                     new SelectListItem() {Text=value3, Value="mpp_lastname"},
                     new SelectListItem() {Text=value4, Value="mpp_address1"},
                     new SelectListItem() {Text=value5, Value="mpp_zip"},
                     new SelectListItem() {Text=value6, Value="mpp_state"},
                     new SelectListItem() {Text=value7, Value="mpp_city"},
                     new SelectListItem() {Text=value8, Value="mpp_homephone"},
                     new SelectListItem() {Text=value9, Value="mpp_currentphone"},
                     new SelectListItem() {Text=value10, Value="mpp_email"},
                     new SelectListItem() {Text=value11, Value="mpp_emerphone"},
                     new SelectListItem() {Text=value12, Value="mpp_emername"},
                     new SelectListItem() {Text=value13.ToString(), Value="mpp_country"},
                     new SelectListItem() {Text=value14.ToString(), Value="mpp_province"},
                     new SelectListItem() {Text=value15.ToString(), Value="mpp_city"},
                     new SelectListItem() {Text=value19.ToString(), Value="mpp_division"},
                     new SelectListItem() {Text=value20.ToString(), Value="mpp_company"},
                     new SelectListItem() {Text=value21.ToString(), Value="mpp_domicile"},
                     new SelectListItem() {Text=value23.ToString(), Value="mpp_type1"},
                     new SelectListItem() {Text=value26.ToString(), Value="mpp_type2"},
                     new SelectListItem() {Text=value27.ToString(), Value="mpp_type3"},
                };


            return new JavaScriptSerializer().Serialize(dInfo);
        }


        public ActionResult AddTo3335(int id)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_InsertDriverInto3335";
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            //Response.Write("<script>alert('Added to TMW');</script>");

            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromNewTMW(int id, string modify)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_DeleteDriverFromNewTMW";
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar);
            cmd.Parameters["@ModifiedBy"].Value = modify;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            //Response.Write("<script>alert('Added to TMW');</script>");

            return RedirectToAction("Index");

        }

        public ActionResult RemoveFrom3335(int id)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_DeleteDriverFrom3335";
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            //Response.Write("<script>alert('Added to TMW');</script>");

            return RedirectToAction("Index");

        }
        public ActionResult AddTo1589(int id)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_InsertDriverInto1589";
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            //Response.Write("<script>alert('Added to TMW');</script>");

            return RedirectToAction("Index");

        }
        public ActionResult RemoveFrom1589(int id)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_DeleteDriverFrom1589";
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = id;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            //Response.Write("<script>alert('Added to TMW');</script>");

            return RedirectToAction("Index");

        }

        
        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver site = db.Drivers.Find(id);
            db.Drivers.Remove(site);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
