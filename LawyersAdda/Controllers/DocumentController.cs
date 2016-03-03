using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LawyersAdda.Entities;
using LawyersAdda.Models;
using Microsoft.AspNet.Identity;

namespace LawyersAdda.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Document/
        public ActionResult Index()
        {
            return View(db.Documentations.ToList());
        }

        // GET: /Document/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentation documentation = db.Documentations.Find(id);
            if (documentation == null)
            {
                return HttpNotFound();
            }
            return View(documentation);
        }

        // GET: /Document/Create
        public ActionResult Create()
        {
            string DocumentID=Guid.NewGuid().ToString();
            ViewBag.DocumentID = DocumentID;
            Documentation d = new Documentation()
            {
                ID=DocumentID,
                DocumentType="-",
                DocumentDescription="-",
                UserID=User.Identity.GetUserId()
            };
            ApplicationDbContext db = new ApplicationDbContext();
            db.Documentations.Add(d);
            db.SaveChanges();
            return View();
        }

        // POST: /Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DocumentType,DocumentDescription")] Documentation documentation)
        {
            if (ModelState.IsValid)
            {
                Documentation d=db.Documentations.Where(t => t.ID == documentation.ID).Single();
                d.DocumentType = documentation.DocumentType;
                d.DocumentDescription = documentation.DocumentDescription;
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentation);
        }

        // GET: /Document/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentation documentation = db.Documentations.Find(id);
            if (documentation == null)
            {
                return HttpNotFound();
            }
            return View(documentation);
        }

        // POST: /Document/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,DocumentType,DocumentDescription")] Documentation documentation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentation);
        }

        // GET: /Document/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentation documentation = db.Documentations.Find(id);
            if (documentation == null)
            {
                return HttpNotFound();
            }
            return View(documentation);
        }

        // POST: /Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Documentation documentation = db.Documentations.Find(id);
            db.Documentations.Remove(documentation);
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
