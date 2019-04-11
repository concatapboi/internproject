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
    public class CartController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Cart
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            var tbl_Cart = db.tbl_Cart.Include(t => t.tbl_Customer).Include(t => t.tbl_Product);
            return View(tbl_Cart.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }
        // GET: Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Cart tbl_Cart = db.tbl_Cart.Find(id);
            if (tbl_Cart == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Cart);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username");
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cus_id,pro_id,amount,total_price")] tbl_Cart tbl_Cart)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_Cart.Add(tbl_Cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username", tbl_Cart.cus_id);
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name", tbl_Cart.pro_id);
            return View(tbl_Cart);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Cart tbl_Cart = db.tbl_Cart.Find(id);
            if (tbl_Cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username", tbl_Cart.cus_id);
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name", tbl_Cart.pro_id);
            return View(tbl_Cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cus_id,pro_id,amount,total_price")] tbl_Cart tbl_Cart)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cus_id = new SelectList(db.tbl_Customer, "id", "username", tbl_Cart.cus_id);
            ViewBag.pro_id = new SelectList(db.tbl_Product, "id", "name", tbl_Cart.pro_id);
            return View(tbl_Cart);
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Cart tbl_Cart = db.tbl_Cart.Find(id);
            if (tbl_Cart == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Cart tbl_Cart = db.tbl_Cart.Find(id);
            db.tbl_Cart.Remove(tbl_Cart);
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
