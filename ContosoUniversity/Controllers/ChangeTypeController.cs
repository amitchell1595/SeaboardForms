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

namespace ContosoUniversity.Controllers
{
    public class ChangeTypeController : Controller
    {
        private MDMSContext db = new MDMSContext();

        // GET: ChangeType
        public ActionResult Index()
        {
            
            return View(db.ChangeTypes.ToList());
        }

        // GET: ChangeType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeType changeType = db.ChangeTypes.Find(id);
            if (changeType == null)
            {
                return HttpNotFound();
            }
            return View(changeType);
        }

        // GET: ChangeType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChangeType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChangeTypeID,ChangeTypeName")] ChangeType changeType)
        {
            if (ModelState.IsValid)
            {
                db.ChangeTypes.Add(changeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(changeType);
        }

        // GET: ChangeType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeType changeType = db.ChangeTypes.Find(id);
            if (changeType == null)
            {
                return HttpNotFound();
            }
            return View(changeType);
        }

        // POST: ChangeType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChangeTypeID,ChangeTypeName")] ChangeType changeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(changeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(changeType);
        }

        // GET: ChangeType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeType changeType = db.ChangeTypes.Find(id);
            if (changeType == null)
            {
                return HttpNotFound();
            }
            return View(changeType);
        }

        // POST: ChangeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChangeType changeType = db.ChangeTypes.Find(id);
            db.ChangeTypes.Remove(changeType);
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
