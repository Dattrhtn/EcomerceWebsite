using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var carts = db.carts.Include(c => c.account).Include(c => c.Product);
            return View(carts.ToList());
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
        public ActionResult Create([Bind(Include = "cart_id,account_account_id,quantity,product_product_id,ngayTao")] cart cart)
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
        public ActionResult Edit([Bind(Include = "cart_id,account_account_id,quantity,product_product_id,ngayTao")] cart cart)
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
        public ActionResult Delete(int? id)
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

        // POST: Client_Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cart cart = db.carts.Find(id);
            db.carts.Remove(cart);
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
