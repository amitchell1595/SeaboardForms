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

namespace ContosoUniversity.Controllers
{
    [ExclusiveAction]
    public class SitesController : Controller
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

            var students = db.Sites.Include(u => u.Country);


            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString))
                    .Include(s => s.SiteType);

            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.UpdatedDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.UpdatedDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.Name);
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
            Site site = db.Sites.Include(i => i.SiteType)
              .SingleOrDefault(x => x.RequestID == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // GET: Sites/Create
        public ActionResult Create()
        {
            var s = new Site()
            {
               
                CreatedDate = DateTime.Now,
                CreatedBy = User.Identity.Name,
                UpdatedDate = DateTime.Now,
                UpdatedBy = User.Identity.Name,
                Countries = new SelectList(db.Countries, "CountryID", "CountryName"),
                Cities = new SelectList(db.Cities, "CityID", "CityName"),
                Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName"),
                Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName"),
                Entities = new SelectList(db.Entities, "EntityID", "EntityName"),
                BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName"),
                SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName"),
                Terminals = new SelectList(db.Terminals, "TerminalID", "TerminalName"),
                Axles = new SelectList(db.Axles, "AxleID", "AxleName"),
                Orbits = new SelectList(db.Orbits, "OrbitID", "OrbitName")
            };

            return View(s);
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteTypeID,CompanyID,Email,AltID,Comment1,Comment2,SitePriority,MainPhone,AutomaticDips,SecondaryPhone,Fax,AdditionalInfo,Name,Address,Address2,PostalCode,DivisionID, OrbitID, EntityID, CountryID,ProvinceID,CityID,Phone,AxleID, BillToID, TerminalID, TankInfo")] Site site, HttpPostedFileBase upload)
        {
            site.CreatedDate = DateTime.Now;
            site.CreatedBy = User.Identity.Name;
            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;

             var validImageTypes = new string[]
             {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
      
             if (upload == null || upload.ContentLength == 0)
             {
                // ModelState.AddModelError("SiteUpload", "This field is required");
             }
             else if (!validImageTypes.Contains(upload.ContentType))
             {
                 ModelState.AddModelError("SiteUpload", "Please choose either a GIF, JPG or PNG image.");
             }

            if (ModelState.IsValid)
            {

                

                if (upload != null && upload.ContentLength > 0)
                {
                    var uploadDir = "~/SiteMaps";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), upload.FileName);
                    var imageUrl = Path.Combine(uploadDir, upload.FileName);
                    upload.SaveAs(imagePath);
                    site.SiteMapLocation = imagePath;
                }

                db.Sites.Add(site);
                db.SaveChanges();
                SubmittedMail(site);
                return RedirectToAction("Index");
               
            }
            site.Countries = new SelectList(db.Countries, "CountryID", "CountryName");
                site.Cities = new SelectList(db.Cities, "CityID", "CityName");
                site.Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
                site.Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName");
                site.Entities = new SelectList(db.Entities, "EntityID", "EntityName");
                site.BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName");
                site.SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
                site.Terminals = new SelectList(db.Terminals, "TerminalID", "TerminalName");
                site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
                site.Orbits = new SelectList(db.Orbits, "OrbitID", "OrbitName");

            return View(site);
        }

        // GET: Sites/Edit/5
        public ActionResult Edit(int? id)
        {
   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);

            site.UpdatedDate = DateTime.Now;
            site.UpdatedBy = User.Identity.Name;
            site.Countries = new SelectList(db.Countries, "CountryID", "CountryName");
            site.Cities = new SelectList(db.Cities, "CityID", "CityName");
            site.Provinces = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
            site.Divisions = new SelectList(db.Divisions, "DivisionID", "DivisionName");
            site.Entities = new SelectList(db.Entities, "EntityID", "EntityName");
            site.BillTos = new SelectList(db.BillTos, "BillToID", "DropDownName");
            site.SiteTypes = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
            site.Terminals = new SelectList(db.Terminals, "TerminalID", "TerminalName");
            site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
            site.Orbits = new SelectList(db.Orbits, "OrbitID", "OrbitName");
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
            var site = db.Sites.Find(id);
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
            site.Terminals = new SelectList(db.Terminals, "TerminalID", "TerminalName");
            site.Axles = new SelectList(db.Axles, "AxleID", "AxleName");
            site.Orbits = new SelectList(db.Orbits, "OrbitID", "OrbitName");


            if (TryUpdateModel(site, "", new string[] { "SiteTypeID", "CompanyID", "Email", "AltID", "Comment1", "Completed", "Comment2", "SitePriority", "MainPhone", " AutomaticDips", "SecondaryPhone", "Fax", "AdditionalInfo", "Name", "Address", "Address2", "PostalCode", "DivisionID", "OrbitID", "EntityID", "CountryID", "ProvinceID", "CityID", "Phone", "AxleID", "BillToID", "TerminalID", "UpdatedDate", "UpdatedBy", "TankInfo" }))
            {
                try
                {
                    db.SaveChanges();
                    if (site.Completed == true)
                    {
                       CompletedMail(site);
                       AddToTMW(); 

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
        public void AddToTMW() {

            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDatabase"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "zsp_InsertCustomerIntoTMW";
            //add any parameters the stored procedure might require
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
        }


        // GET: Sites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Include(i => i.SiteType)
              .SingleOrDefault(x => x.RequestID == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }
        private void SubmittedMail(Site u)
        {
            string subject =
         @"New Site Change Form  Submitted for @Model.Name";

            MailDefinition md = new MailDefinition();
            md.From = "MDMS@seaboard.acl.ca";
            md.IsBodyHtml = true;
            md.Subject = Razor.Parse(subject, u);

            ListDictionary replacements = new ListDictionary();


            string body =
                @"<html>
                <body>
                A site change form has been submitted.
                <br/>
                <br/>
                Name: @Model.Name <br/>
                Submitted By: @Model.CreatedBy <br/>
                Submitted Time: @Model.CreatedDate <br/>
                <br/><br/>
                Please click the following link to access this request:
                
                <a href='http://seatranweb01:8080/Sites/Edit/@Model.RequestID'>http://seatranweb01:8080/Sites/Edit/@Model.RequestID</a>

                </body>
                </html>";
            string mailBody = Razor.Parse(body, u);
            MailMessage msg = md.CreateMailMessage("support@seaboard.acl.ca", replacements, mailBody, new System.Web.UI.Control());

            //Send the message.
            SmtpClient client = new SmtpClient("aclexchange.acl.ca");
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            client.Send(msg);

        }

        private void CompletedMail(Site u)
        {
            string subject =
            @"Site Change Form for @Model.Name has been completed!";


            MailDefinition md = new MailDefinition();
            md.From = "MDMS@seaboard.acl.ca";
            md.IsBodyHtml = true;
            md.Subject = Razor.Parse(subject, u);

            ListDictionary replacements = new ListDictionary();



            string body =
                @"<html>
                <body>
                Your site change form request has been completed! If you require additional assistance please contact <a href=""mailto:support@seaboard.acl.ca?Subject=User%20Form%20Issue"" target=""_top"">support@seaboard.acl.ca</a>.
                <br/>
                <br/>
                Employee Name: @Model.Name <br/>
                Submitted By: @Model.CreatedBy <br/>
                Submitted Time: @Model.CreatedDate <br/>
                <br/><br/>

                Please click the following link to view your request:

                <a href='http://seatranweb01:8080/Sites/Details/@Model.RequestID'>http://seatranweb01:8080/Sites/Details/@Model.RequestID</a>

                </body>
                </html>";
            string mailBody = Razor.Parse(body, u);

            string[] pieces = u.CreatedBy.Split('\\');
            MailMessage msg = md.CreateMailMessage(pieces[1] + "@seaboard.acl.ca", replacements, mailBody, new System.Web.UI.Control());

            //Send the message.
            SmtpClient client = new SmtpClient("aclexchange.acl.ca");
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            client.Send(msg);

        }
        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = db.Sites.Find(id);
            db.Sites.Remove(site);
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
