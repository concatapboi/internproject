using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        private DemoEntities db = new DemoEntities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.slider = db.tbl_Slider;
            ViewBag.product = db.tbl_Product;
            return View();
        }
        // GET: Product
        public ActionResult Product()
        {
            ViewBag.product = db.tbl_Product;
            ViewBag.category = db.tbl_Category;
            ViewBag.Hotproduct = db.tbl_Product.Where(p => p.rate > 0).Take(3);
            return View();
        }
        //GET: Product by Cate id
        [HttpGet]
        public ActionResult ProductByCate(int? id)
        {
            ViewBag.product = db.tbl_Product.Where(p => p.cate_id == id);
            ViewBag.category = db.tbl_Category;
            ViewBag.Hotproduct = db.tbl_Product.Where(p => p.rate > 0).Take(3);
            return View("Product");
        }
        //GET: Product by Rate
        public ActionResult ProductByRate()
        {
            ViewBag.product = db.tbl_Product.Where(p => p.rate > 0);
            ViewBag.category = db.tbl_Category;
            ViewBag.Hotproduct = db.tbl_Product.Where(p => p.rate > 0).Take(3);
            return View("Product");
        }
        //GET: Product by Rate
        public ActionResult DiscountProduct()
        {
            ViewBag.product = db.tbl_Product.Where(p => p.discount > 0);
            ViewBag.category = db.tbl_Category;
            ViewBag.Hotproduct = db.tbl_Product.Where(p => p.rate > 0).Take(3);
            return View("Product");
        }

        //GET: Search
        [HttpGet]
        public ActionResult Search()
        {
            String name = Request.QueryString.Get("search");
            ViewBag.search = name;
            ViewBag.product = db.tbl_Product.Where(p => p.name.Contains(name));
            ViewBag.category = db.tbl_Category;
            ViewBag.Hotproduct = db.tbl_Product.Where(p => p.rate > 0).Take(3);
            return View("Product");
        }
        //GET: Account
        public ActionResult Account()
        {
            tbl_Customer cus = (tbl_Customer)Session["user"];
            ViewBag.bill = db.tbl_Bill.Where(b => b.cus_id ==cus.id);
            return View();
        }
        //GET: BillDetail
        //{
        public PartialViewResult _BillDetail(int? id)
        {
            List<tbl_BillDetail> billDetail = db.tbl_BillDetail.Where(bD => bD.bill_id == id).ToList();
            List<tbl_Product> pList = db.tbl_Product.ToList();
            ViewBag.pList = pList;
            ViewBag.billDetail = billDetail;
            return PartialView();
        }
        //GET: Checkout
        public ActionResult Checkout()
        {
            if (Session["user"] == null) return RedirectToAction("Account");
            return View();
        }
        //GET: Order
        public ActionResult Ordering()
        {
            CartData cart = (CartData)Session["cart"];
            if(cart.amounts ==0) return Redirect(Request.UrlReferrer.AbsoluteUri);
            String cus_address = Request.Form.Get("address");
            String cus_name = Request.Form.Get("name");
            tbl_Customer cus = (tbl_Customer)Session["user"];
            CartData cartList = (CartData)Session["cart"];
            tbl_Bill bill = db.tbl_Bill.Create();
            bill.cus_id = cus.id;
            bill.level_status = 0;
            bill.total_price = cartList.cost;
            bill.cus_address = cus_address;
            bill.cus_name = cus_name;
            bill.created_at = DateTime.Now;
            db.tbl_Bill.Add(bill);
            for(int i = 0;i< cartList.arrCart.Count; i++)
            {
                Cart c = (Cart)cartList.arrCart[i];
                tbl_BillDetail billDetail = db.tbl_BillDetail.Create();
                billDetail.bill_id = bill.id;
                billDetail.pro_id = c.item.id;
                billDetail.price = c.cost*c.amounts;
                billDetail.amount = c.amounts;
                db.tbl_BillDetail.Add(billDetail);
                tbl_Product p = db.tbl_Product.Find(c.item.id);
                p.amount -= c.amounts;
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            Session.Remove("cart");
            return RedirectToAction("Account");
        }
        //GET: Cancel Ordering
        public ActionResult CancelOrdering(int? id)
        {
            if (id == null) return View("../Err");
            tbl_Bill bill = db.tbl_Bill.Find(id);
            bill.level_status = 4;
            List<tbl_BillDetail> billList = db.tbl_BillDetail.Where(bD => bD.bill_id == id).ToList();
            for(int i = 0; i < billList.Count; i++)
            {
                tbl_Product p = db.tbl_Product.Find(billList[i].pro_id);
                p.amount += billList[i].amount;
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            }
            db.Entry(bill).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        //GET: Cart
        public ActionResult Cart()
        {
            CartData cart = (CartData)Session["cart"];
            CartData cartAvai = new CartData();
            CartData cartSold = new CartData();
            if (cart != null)
            {
                for (int i = 0; i < cart.arrCart.Count; i++)
                {
                    Cart c = (Cart)cart.arrCart[i];
                    tbl_Product p = db.tbl_Product.Find(c.item.id);
                    if (p.amount >= c.amounts)
                    {
                        cartAvai.addWithNum(c.item, c.amounts);
                    }
                    else if (p.amount > 0)
                    {
                        cartAvai.addWithNum(c.item, c.amounts);
                    }
                    else if (p.amount <= 0)
                    {
                        cartSold.addWithNum(c.item, c.amounts);
                    }
                }
            }
            ViewBag.cart = cartAvai;
            return View();
        }
        //GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
        //GET: Product Detail
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return View("Err");
            }
            ViewBag.productItem = db.tbl_Product.Find(id);
            if (db.tbl_Product.Find(id) == null)
            {
                return View("../Err");
            }
            ViewBag.product = db.tbl_Product.Where(p => p.amount > 0).Take(4);
            ViewBag.cat = db.tbl_Category.Find(db.tbl_Product.Find(id).cate_id);
            ViewBag.pub = db.tbl_Publisher.Find(ViewBag.cat.pub_id);
            return View();
        }
        //GET: miniCart
        //{
        public PartialViewResult _Cart()
        {
            CartData oldCart = (CartData)Session["cart"];
            CartData cart = new CartData(oldCart);
            ViewBag.cart = cart;
            return PartialView();
        }
        public ActionResult addCart(int id)
        {
            tbl_Product p = db.tbl_Product.Find(id);
            CartData oldCart = (CartData)Session["cart"];
            Session.Remove("cart");
            CartData cart = new CartData(oldCart);
            if(cart.arrCart.Count > 0) 
            {
                for (int i = 0; i < cart.arrCart.Count; i++)
                {
                    Cart c = (Cart)cart.arrCart[i];
                    if (c.item.id == id)
                    {
                        if (p.amount > c.amounts) cart.add(p);
                        Session["cart"] = cart;
                        return Redirect(Request.UrlReferrer.AbsoluteUri);
                     }
                }
            }
            cart.add(p);
            Session["cart"] = cart;
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public ActionResult removeItem(int id)
        {
            tbl_Product p = db.tbl_Product.Find(id);
            CartData oldCart = (CartData)Session["cart"];
            Session.Remove("cart");
            CartData cart = new CartData(oldCart);
            int num = cart.getID(p);
            Cart c = (Cart)cart.arrCart[num];
            cart.remove(p,c.amounts);
            Session["cart"] = cart;
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        //}
        //Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id,username,pass,name,email,phone,cus_address")] tbl_Customer tbl_Customer)
        {
            if (ModelState.IsValid)
            {
                List<tbl_Customer> cusList = db.tbl_Customer.ToList();
                foreach(tbl_Customer c in cusList)
                {
                    if(c.username == tbl_Customer.username)
                    {
                        Session["errValue"] = tbl_Customer;
                        Session["errMess"] = "Username đã tồn tại!";
                        return Redirect(Request.UrlReferrer.AbsoluteUri);
                    }
                    if ( tbl_Customer.pass.Length < 3 || tbl_Customer.pass.Contains(" ")==true)
                    {
                        Session["errValue"] = tbl_Customer;
                        Session["errMess"] = "Mật khẩu có ít nhất 3 ký tự, không có khoảng trắng";
                        return Redirect(Request.UrlReferrer.AbsoluteUri);
                    }
                    if (c.email == tbl_Customer.email)
                    {
                        Session["errValue"] = tbl_Customer;
                        Session["errMess"] = "Email đã tồn tại!";
                        return Redirect(Request.UrlReferrer.AbsoluteUri);
                    }

                }
                db.tbl_Customer.Add(tbl_Customer);
                db.SaveChanges();
                Session["user"] = tbl_Customer;
                return RedirectToAction("Index");
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        //Update Information
        [HttpPost]
        public ActionResult UpdateInfo()
        {
            tbl_Customer cus = (tbl_Customer)Session["user"];
            if (cus == null) return RedirectToAction("Account");
            Session.Remove("user");
            tbl_Customer user = db.tbl_Customer.Find(cus.id);
            String name = Request.Form.Get("name");
            String pass = Request.Form.Get("pass");
            String email = Request.Form.Get("email");
            String phone = Request.Form.Get("phone");
            String address = Request.Form.Get("address");
            user.name = name;
            if (pass.Length < 3)
            {
                ViewBag.errUpdate = "Fail. Pass length under 3";
                return RedirectToAction("Account");
            }
            user.phone = phone;
            user.email = email;
            user.cus_address = address;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Session["user"] = user;
            ViewBag.success = "Done updating!!";
            return RedirectToAction("Account");
        }
        //Login
        [HttpPost]
        public ActionResult Login()
        {
            String username = Request.Form.Get("username");
            String pass = Request.Form.Get("password");
            List<tbl_Customer> cusList = db.tbl_Customer.ToList();
            tbl_Customer cus = null;
            foreach (tbl_Customer c in cusList)
            {
                if (c.username == username && c.pass == pass)
                {
                    cus = c;
                    Session["user"] = cus;
                    List<tbl_Cart> arrtbl_Cart = db.tbl_Cart.ToList();
                    CartData oldCart = (CartData)Session["cart"];
                    CartData cart = new CartData(oldCart);
                    for (int  i = 0; i < arrtbl_Cart.Count; i++)
                    {
                        if (arrtbl_Cart[i].cus_id == cus.id)
                        {
                            tbl_Product p = db.tbl_Product.Find(arrtbl_Cart[i].pro_id);
                            cart.addWithNum(p, arrtbl_Cart[i].amount);
                            db.tbl_Cart.Remove(arrtbl_Cart[i]);
                        }
                    }
                    db.SaveChanges();
                    Session.Remove("cart");
                    Session["cart"] = cart;
                    return RedirectToAction("Account");
                }
                else
                {
                    if (c.username != username) Session["errLogin"] = "Username incorrect!";
                    else if(c.pass != pass) Session["errLogin"] = "Password incorrect!";
                }
            }
            Session["usernameLogin"] = username;
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        //Logout
        public ActionResult Logout()
        {
            CartData oldCart = (CartData)Session["cart"];
            tbl_Customer cus = (tbl_Customer)Session["user"];
            if (oldCart != null) {
                for (int i = 0; i < oldCart.arrCart.Count; i++)
                {
                    Cart item = (Cart)oldCart.arrCart[i];
                    tbl_Cart cart = new tbl_Cart { cus_id = cus.id, pro_id = item.item.id, amount = item.amounts, total_price = item.cost };
                    db.tbl_Cart.Add(cart);
                    db.SaveChanges();
                }
            }
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}