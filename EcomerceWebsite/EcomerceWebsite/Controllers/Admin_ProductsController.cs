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
    public class Admin_ProductsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin_Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin_Products/Create
        public ActionResult Create()
        {
            ViewBag.Category_category_id = new SelectList(db.Categories, "category_id", "name");
            ViewBag.erAddPr = TempData["erAddPr"] as string;
            return View();
        }

        // POST: Admin_Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,productCode,SKU,description,price,name,size,color,quantity,stock,Category_category_id,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ngayTao = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                TempData["erAddPr"] = "Thêm sản phẩm thành công!";
                ViewBag.erAddPr = TempData["erAddPr"] as string;
                return RedirectToAction("Create");
            }

            ViewBag.Category_category_id = new SelectList(db.Categories, "category_id", "name", product.Category_category_id);
            return View(product);
        }

        // GET: Admin_Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.erEditPr = TempData["erEditPr"] as string;
            ViewBag.Category_category_id = new SelectList(db.Categories, "category_id", "name", product.Category_category_id);
            return View(product);
        }

        // POST: Admin_Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,productCode,SKU,description,price,name,size,color,quantity,stock,Category_category_id,Image")] Product product)
        {
            var existingPayment = db.Products.AsNoTracking().FirstOrDefault(p => p.product_id == product.product_id);
            if (ModelState.IsValid)
            {
                product.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["erEditPr"] = "Sửa thông tin sản phẩm thành công!";
                ViewBag.erEditPr = TempData["erEditPr"] as string;
                return RedirectToAction("Edit");
            }
            ViewBag.Category_category_id = new SelectList(db.Categories, "category_id", "name", product.Category_category_id);
            return View(product);
        }

        // GET: Admin_Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin_Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
                db.Products.Remove(product);
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
