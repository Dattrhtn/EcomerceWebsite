using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EcomerceWebsite.Models;

namespace EcomerceWebsite.Controllers
{
    public class Client_CartController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Client_Cart
        public ActionResult Index()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var account_id = int.Parse(Session["account_id"] as string);
                Session["numberOfCart"] = db.carts.Where(c => c.account_account_id == account_id).Count();
                var carts = db.carts.Include(c => c.account).Include(c => c.Product).Where(c => c.account_account_id == account_id);

                var Prices = from cart in db.carts
                             where cart.account_account_id == account_id
                             join product in db.Products
                             on cart.product_product_id equals product.product_id
                             select cart.quantity * product.price;

                var totalPrices = Prices.Sum();
                ViewBag.totalPrices = totalPrices;
                return View(carts.ToList());
            }
            return RedirectToAction("Index", "Login");
        }
        // GET: Client_Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Client_Cart/Create
        public ActionResult Create()
        {
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name");
            ViewBag.product_product_id = new SelectList(db.Products, "product_id", "productCode");
            return View();
        }

        // POST: Client_Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cart_id,quantity,account_account_id,product_product_id,ngayTao")] cart cart)
        {
            if (ModelState.IsValid)
            {
                db.carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", cart.account_account_id);
            ViewBag.product_product_id = new SelectList(db.Products, "product_id", "productCode", cart.product_product_id);
            return View(cart);
        }

        // GET: Client_Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", cart.account_account_id);
            ViewBag.product_product_id = new SelectList(db.Products, "product_id", "productCode", cart.product_product_id);
            return View(cart);
        }

        // POST: Client_Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cart_id,quantity,account_account_id,product_product_id,ngayTao")] cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", cart.account_account_id);
            ViewBag.product_product_id = new SelectList(db.Products, "product_id", "productCode", cart.product_product_id);
            return View(cart);
        }

        // GET: Client_Cart/Delete/5
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            cart cart = db.carts.Find(id);
            db.carts.Remove(cart);
            db.SaveChanges();

            // Lấy thông tin về controller hiện tại
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();

            // Chuyển hướng đến action Index của controller hiện tại
            return RedirectToAction("Index", currentController);
        }
        [HttpPost]
        public ActionResult Update_Cart()
        {
            string list_cartid = Request["cart_id"].ToString();
            string list_quantity = Request["quantity_value"].ToString();
            if (list_cartid != null && list_quantity != null)
            {
                var mang_cartid = list_cartid.Split(',');
                var mang_quantity = list_quantity.Split(',');

                for (int i = 0; i < mang_cartid.Length; i++)
                {
                    int cart_id = int.Parse(mang_cartid[i]);
                    int current_quantity = int.Parse(mang_quantity[i]);
                    var cart = db.carts.Find(cart_id);

                    cart.quantity = current_quantity;
                    db.Entry(cart).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
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
        //[Authorize]
        public ActionResult Checkout()
        {
            var carts = db.carts.Include(c => c.account).Include(c => c.Product);

            var Prices = from cart in db.carts
                         join product in db.Products
                         on cart.product_product_id equals product.product_id
                         select cart.quantity * product.price;

            var totalPrices = Prices.Sum();
            ViewBag.totalPrices = totalPrices;
            return View(carts.ToList());

            //if (db.carts.Count() == 0)
            //{
            //    return Redirect("/");
            //}
            //return View();
        }
    }
}
