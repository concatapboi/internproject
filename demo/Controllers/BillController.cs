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
    public class BillController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Bill
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            var tbl_Bill = db.tbl_Bill.Include(t => t.tbl_Customer);
            return View(tbl_Bill.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }

        // GET: Bill/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Bill tbl_Bill = db.tbl_Bill.Find(id);
            if (tbl_Bill == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Bill);
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,level_status,cus_id,created_at,total_price,cus_name")] tbl_Bill tbl_Bill)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_Bill.Add(tbl_Bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username", tbl_Bill.cus_id);
            return View(tbl_Bill);
        }

        // GET: Bill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Bill tbl_Bill = db.tbl_Bill.Find(id);
            if (tbl_Bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username", tbl_Bill.cus_id);
            return View(tbl_Bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,level_status,cus_id,created_at,total_price,cus_name")] tbl_Bill tbl_Bill)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Bill).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username", tbl_Bill.cus_id);
            return View(tbl_Bill);
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Bill tbl_Bill = db.tbl_Bill.Find(id);
            if (tbl_Bill == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Bill tbl_Bill = db.tbl_Bill.Find(id);
            db.tbl_Bill.Remove(tbl_Bill);
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
