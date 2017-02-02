using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using PagedList;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using RazorEngine.Templating;
using RazorEngine;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Net.Mail;

namespace ContosoUniversity.Controllers
{
    [ExclusiveAction]
    public class UserController : Controller
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

            var students = db.Users.Include(u => u.ChangeType).Include(u => u.Company);

            
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString))
                    .Include(s => s.ChangeType).Include(x => x.Payroll);

            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EffectiveDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EffectiveDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.Name);
                    break;
            }



  

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }


        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User student = db.Users.Where(x => x.ID == id).Include(x => x.ChangeType)
                .Include(x => x.Payroll).Include(x => x.Company).FirstOrDefault();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {

            var u = new User()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = User.Identity.Name,
                UpdatedDate = DateTime.Now,
                UpdatedBy = User.Identity.Name,
                Companies = new SelectList(db.Companies, "CompanyID", "CompanyName"),
                ChangeTypes = new SelectList(db.ChangeTypes, "ChangeTypeID", "ChangeTypeName"),
                Payrolls = new SelectList(db.Payrolls, "PayrollID", "PayrollName"),
                EffectiveDate = DateTime.Now
            };

            return View(u);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, EffectiveDate, ChangeTypeID, CompanyID, PayrollID, SupervisorName, Role, ApplicationsandFolders, AdditionalInfo, CreatedBy, CreatedDate")]User student)
        {
            student.CreatedDate = DateTime.Now;
            student.CreatedBy = User.Identity.Name;
            student.UpdatedDate = DateTime.Now;
            student.UpdatedBy = User.Identity.Name;
            student.Companies = new SelectList(db.Companies, "CompanyID", "CompanyName");
            student.ChangeTypes = new SelectList(db.ChangeTypes, "ChangeTypeID", "ChangeTypeName");
            student.Payrolls = new SelectList(db.Payrolls, "PayrollID", "PayrollName");
            student.EffectiveDate = DateTime.Now;

            if (student.PayrollID == null)
            {
                ModelState.AddModelError("PayrollID", "Please select a payroll.");
               
            }
            if (student.CompanyID == null)
            {
                ModelState.AddModelError("CompanyID", "Please select a company.");

            }
            if (student.ChangeTypeID == 0 || student.ChangeTypeID == null)
            {
                ModelState.AddModelError("ChangeTypeID", "Please select a change type.");

            }


            try
            {
                if (ModelState.IsValid)
                {
                    student.CreatedDate = DateTime.Now;
                    student.CreatedBy = User.Identity.Name;
                    student.UpdatedDate = DateTime.Now;
                    student.UpdatedBy = User.Identity.Name;
                    db.Users.Add(student);
                    db.SaveChanges();

                    var u = db.Users.Include(s => s.ChangeType).Include(s => s.Payroll).Include(s => s.Company)
                        .Where(c => c.ID == student.ID).FirstOrDefault();

                    SubmittedMail(u);
                    return RedirectToAction("Thanks");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {

                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }
        public ActionResult Thanks()
        {


            return View();
        }
        private void SubmittedMail(User u)
        {
            string subject =
         @"User Change Form Submitted for @Model.Name - @Model.ChangeType.ChangeTypeName @Model.EffectiveDate.ToShortDateString()";
            string[] pieces = u.CreatedBy.Split('\\');
            MailDefinition md = new MailDefinition();
            md.From = pieces[1] + "@seaboard.acl.ca";
                md.IsBodyHtml = true;
                md.Subject = Razor.Parse(subject, u);
             
                ListDictionary replacements = new ListDictionary();


                   string body =
                        @"<html>
                <body>
                A user change form has been submitted.
                <br/>
                <br/>
                Employee Name: @Model.Name <br/>
                Company Name: @Model.Company.CompanyName <br/>
                Payroll: @Model.Payroll.PayrollName <br/>
                Supervisor: @Model.SupervisorName <br/>
                Role: @Model.Role <br/>
                Applications and Folders: @Model.ApplicationsAndFolders <br/>
                Additional Info: @Model.AdditionalInfo <br/>
                Submitted By: @Model.CreatedBy <br/>
                Submitted Time: @Model.CreatedDate <br/>
                Effective Date: @Model.EffectiveDate
                <br/><br/>
                Please click the following link to access this request:
                
                <a href='http://seatranweb01:8080/User/Edit/@Model.ID'>http://seatranweb01:8080/User/Edit/@Model.ID</a>

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

        private void CompletedMail(User u)
        {
            string subject =
            @"User Change Form for @Model.Name has been completed!";
            

            MailDefinition md = new MailDefinition();
            md.From = "MDMS@seaboard.acl.ca";
            md.IsBodyHtml = true;
            md.Subject = Razor.Parse(subject, u);

            ListDictionary replacements = new ListDictionary();
         


            string body =
                @"<html>
                <body>
                Your user change form request has been completed! If you require additional assistance please contact <a href=""mailto:support@seaboard.acl.ca?Subject=User%20Form%20Issue"" target=""_top"">support@seaboard.acl.ca</a>.
                <br/>
                <br/>
                Employee Name: @Model.Name <br/>
                Submitted By: @Model.CreatedBy <br/>
                Submitted Time: @Model.CreatedDate <br/>
                Effective Date: @Model.EffectiveDate
                <br/><br/>

                Please click the following link to view your request:

                <a href='http://seatranweb01:8080/User/Details/@Model.ID'>http://seatranweb01:8080/User/Details/@Model.ID</a>

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

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User student = db.Users.Find(id);



            student.Companies = new SelectList(db.Companies, "CompanyID", "CompanyName");
            student.ChangeTypes = new SelectList(db.ChangeTypes, "ChangeTypeID", "ChangeTypeName");
            student.Payrolls = new SelectList(db.Payrolls, "PayrollID", "PayrollName");


            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
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
            var studentToUpdate = db.Users.Find(id);

            studentToUpdate.UpdatedDate = DateTime.Now;
            studentToUpdate.UpdatedBy = User.Identity.Name;

            studentToUpdate.Companies = new SelectList(db.Companies, "CompanyID", "CompanyName");
            studentToUpdate.ChangeTypes = new SelectList(db.ChangeTypes, "ChangeTypeID", "ChangeTypeName");
            studentToUpdate.Payrolls = new SelectList(db.Payrolls, "PayrollID", "PayrollName");

            if (TryUpdateModel(studentToUpdate, "", new string[] { "Name", "EffectiveDate", "ChangeTypeID", "CompanyID", "PayrollID", "SupervisorName", "Role", "ApplicationsandFolders", "AdditionalInfo", "UpdatedBy", "UpdatedDate", "Completed"}))
            {
                try
                {
                    db.SaveChanges();
                    if (studentToUpdate.Completed == true) {
                        CompletedMail(studentToUpdate);
                    }
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            User student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                User student = db.Users.Find(id);
                db.Users.Remove(student);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
