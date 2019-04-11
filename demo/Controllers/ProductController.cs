using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class ProductController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Product
        public ActionResult Index()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            var tbl_Product = db.tbl_Product.Include(t => t.tbl_Category);
            return View(tbl_Product.ToList());
        }
        //GET: Login
        public ActionResult Login()
        {
            return View("../_Login");
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            List<tbl_Category> cate = db.tbl_Category.ToList();
            ViewBag.cate = cate;
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult postCreate()
        {
            HttpPostedFileBase f = Request.Files.Get("img_file");
            if(f!=null && f.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/dailyShop/img/product"),
                                               Path.GetFileName(f.FileName));
                    f.SaveAs(path);
                    tbl_Product p = new tbl_Product();
                    p.name = Request.Form.Get("name");
                    p.cate_id = Int32.Parse(Request.Form.Get("cate_id"));
                    p.meta_name = Request.Form.Get("meta_name");
                    p.img = f.FileName;
                    p.rate = Byte.Parse(Request.Form.Get("rate"));
                    p.amount = Int32.Parse(Request.Form.Get("amount"));
                    p.price = Int32.Parse(Request.Form.Get("price"));
                    p.discount = Int32.Parse(Request.Form.Get("discount"));
                    p.get_at = DateTime.Parse(Request.Form.Get("get_at"));
                    db.tbl_Product.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Create");
                }
                
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
                return RedirectToAction("Create");
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Product p = db.tbl_Product.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            List<tbl_Category> cate = db.tbl_Category.ToList();
            ViewBag.cate = cate;
            ViewBag.p = p;
            return View();
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult postEdit()
        {
            HttpPostedFileBase f = Request.Files.Get("img_file");
            int id = Int32.Parse(Request.Form.Get("id"));
            tbl_Product p = db.tbl_Product.Find(id);
            if (f != null && f.ContentLength > 0)
            {
                try
                {
                    string oldPath = Path.Combine(Server.MapPath("~/Content/dailyShop/img/product"),
                                              Path.GetFileName(p.img));
                    FileInfo oldf = new FileInfo(oldPath);
                    if (oldf.Exists)
                    {
                        oldf.Delete();
                    }
                    string path = Path.Combine(Server.MapPath("~/Content/dailyShop/img/product"),
                                               Path.GetFileName(f.FileName));
                    f.SaveAs(path);
                    p.name = Request.Form.Get("name");
                    p.cate_id = Int32.Parse(Request.Form.Get("cate_id"));
                    p.meta_name = Request.Form.Get("meta_name");
                    p.img = f.FileName;
                    p.rate = Byte.Parse(Request.Form.Get("rate"));
                    p.amount = Int32.Parse(Request.Form.Get("amount"));
                    p.price = Int32.Parse(Request.Form.Get("price"));
                    p.discount = Int32.Parse(Request.Form.Get("discount"));
                    p.get_at = DateTime.Parse(Request.Form.Get("get_at"));
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                }

            }
            else
            {
                p.name = Request.Form.Get("name");
                p.cate_id = Int32.Parse(Request.Form.Get("cate_id"));
                p.meta_name = Request.Form.Get("meta_name");
                p.rate = Byte.Parse(Request.Form.Get("rate"));
                p.amount = Int32.Parse(Request.Form.Get("amount"));
                p.price = Int32.Parse(Request.Form.Get("price"));
                p.discount = Int32.Parse(Request.Form.Get("discount"));
                p.get_at = DateTime.Parse(Request.Form.Get("get_at"));
            }
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null) return RedirectToAction("Login");
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            string path = Path.Combine(Server.MapPath("~/Content/dailyShop/img/product"),
                                              Path.GetFileName(tbl_Product.img));
            FileInfo f = new FileInfo(path);
            if (f.Exists)
            {
                f.Delete();
                db.tbl_Product.Remove(tbl_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
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
