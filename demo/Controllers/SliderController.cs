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
    public class SliderController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Slider
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            return View(db.tbl_Slider.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }
        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Slider tbl_Slider = db.tbl_Slider.Find(id);
            if (tbl_Slider == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,meta_name")] tbl_Slider tbl_Slider)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.tbl_Slider.Add(tbl_Slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Slider tbl_Slider = db.tbl_Slider.Find(id);
            if (tbl_Slider == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,meta_name")] tbl_Slider tbl_Slider)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Slider).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Slider tbl_Slider = db.tbl_Slider.Find(id);
            if (tbl_Slider == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Slider tbl_Slider = db.tbl_Slider.Find(id);
            db.tbl_Slider.Remove(tbl_Slider);
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
