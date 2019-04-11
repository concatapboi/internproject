using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class PublisherController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Publisher
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            return View(db.tbl_Publisher.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }
        // GET: Publisher/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Publisher tbl_Publisher = db.tbl_Publisher.Find(id);
            if (tbl_Publisher == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Publisher);
        }

        // GET: Publisher/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            return View();
        }

        // POST: Publisher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,meta_name,amount")] tbl_Publisher tbl_Publisher)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_Publisher.Add(tbl_Publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Publisher);
        }

        // GET: Publisher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Publisher tbl_Publisher = db.tbl_Publisher.Find(id);
            if (tbl_Publisher == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Publisher);
        }

        // POST: Publisher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,meta_name,amount")] tbl_Publisher tbl_Publisher)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Publisher).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Publisher);
        }

        // GET: Publisher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Publisher tbl_Publisher = db.tbl_Publisher.Find(id);
            if (tbl_Publisher == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Publisher);
        }

        // POST: Publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Publisher tbl_Publisher = db.tbl_Publisher.Find(id);
            db.tbl_Publisher.Remove(tbl_Publisher);
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
