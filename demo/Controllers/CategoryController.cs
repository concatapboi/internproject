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
    public class CategoryController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Category
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            var tbl_Category = db.tbl_Category.Include(t => t.tbl_Publisher);
            return View(tbl_Category.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            ViewBag.pub_id = new SelectList(db.tbl_Publisher, "id", "name");
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,pub_id,name,meta_name,amount")] tbl_Category tbl_Category)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_Category.Add(tbl_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.tbl_Publisher, "id", "name", tbl_Category.pub_id);
            return View(tbl_Category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.tbl_Publisher, "id", "name", tbl_Category.pub_id);
            return View(tbl_Category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,pub_id,name,meta_name,amount")] tbl_Category tbl_Category)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.tbl_Publisher, "id", "name", tbl_Category.pub_id);
            return View(tbl_Category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            db.tbl_Category.Remove(tbl_Category);
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
