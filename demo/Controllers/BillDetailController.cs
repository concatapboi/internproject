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
    public class BillDetailController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: BillDetail
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            var tbl_BillDetail = db.tbl_BillDetail.Include(t => t.tbl_Bill).Include(t => t.tbl_Product);
            return View(tbl_BillDetail.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }
        // GET: BillDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_BillDetail tbl_BillDetail = db.tbl_BillDetail.Find(id);
            if (tbl_BillDetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_BillDetail);
        }

        // GET: BillDetail/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            ViewBag.bill_id = new SelectList(db.tbl_Bill, "id", "cus_name");
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name");
            return View();
        }

        // POST: BillDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,bill_id,pro_id,amount,price")] tbl_BillDetail tbl_BillDetail)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_BillDetail.Add(tbl_BillDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bill_id = new SelectList(db.tbl_Bill, "id", "cus_name", tbl_BillDetail.bill_id);
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name", tbl_BillDetail.pro_id);
            return View(tbl_BillDetail);
        }

        // GET: BillDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_BillDetail tbl_BillDetail = db.tbl_BillDetail.Find(id);
            if (tbl_BillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.bill_id = new SelectList(db.tbl_Bill, "id", "cus_name", tbl_BillDetail.bill_id);
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name", tbl_BillDetail.pro_id);
            return View(tbl_BillDetail);
        }

        // POST: BillDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,bill_id,pro_id,amount,price")] tbl_BillDetail tbl_BillDetail)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_BillDetail).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bill_id = new SelectList(db.tbl_Bill, "id", "cus_name", tbl_BillDetail.bill_id);
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name", tbl_BillDetail.pro_id);
            return View(tbl_BillDetail);
        }

        // GET: BillDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_BillDetail tbl_BillDetail = db.tbl_BillDetail.Find(id);
            if (tbl_BillDetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_BillDetail);
        }

        // POST: BillDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_BillDetail tbl_BillDetail = db.tbl_BillDetail.Find(id);
            db.tbl_BillDetail.Remove(tbl_BillDetail);
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
