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
    public class AdminController : Controller
    {
        private DemoEntities db = new DemoEntities();
        //GET: Home
        public ActionResult Home()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            ViewBag.cus = db.tbl_Customer.ToList();
            ViewBag.bill = db.tbl_Bill.Where(b=>b.level_status == 3).ToList();
            ViewBag.p = db.tbl_Product.Where(p=>p.amount==0).ToList();
            return View("../adminHome");
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["admin"]==null) return RedirectToAction("Login");
            return View(db.tbl_Admin.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }
        //POST: Login
        [HttpPost]
        public ActionResult postLogin()
        {
            String username = Request.Form.Get("username");
            String pass = Request.Form.Get("pass");
            List<tbl_Admin> adList = db.tbl_Admin.ToList();
            tbl_Admin admin = null;
            foreach (tbl_Admin a in adList)
            {
                if (a.username != username || a.pass != pass)
                    return RedirectToAction("Login");
                admin = a;
                Session["admin"] = admin;
                return RedirectToAction("Home");
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        //GET: Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,pass,name")] tbl_Admin tbl_Admin)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_Admin.Add(tbl_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Admin);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,pass,name")] tbl_Admin tbl_Admin)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Admin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            db.tbl_Admin.Remove(tbl_Admin);
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
