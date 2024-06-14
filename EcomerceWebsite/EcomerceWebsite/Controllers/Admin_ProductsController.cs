using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EcomerceWebsite.Models;

namespace EcomerceWebsite.Controllers
{
    public class Admin_ProductsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_Products
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var products = db.Products.Include(p => p.Category);
                var totalItems = products.ToList().Count;

                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var paginatedProducts = products.OrderBy(x => x.ngayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.TotalItems = totalItems;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;

                return View(paginatedProducts);
            }
            return RedirectToAction("Index", "Login");
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
            var states = new List<SelectListItem>
            {
                new SelectListItem { Value = "S", Text = "S" },
                new SelectListItem { Value = "M", Text = "M" },
                new SelectListItem { Value = "L", Text = "L" },
                new SelectListItem { Value = "XL", Text = "XL" },
                new SelectListItem { Value = "XXL", Text = "XXL" },
                new SelectListItem { Value = "XXXL", Text = "XXXL" },
            };
            // Truyền danh sách các trạng thái vào ViewBag
            ViewBag.States = states;
            return View();
        }

        // POST: Admin_Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,productCode,SKU,description,price,name,size,color,quantity,stock,Category_category_id,Image")] Product product, HttpPostedFileBase FileAnh)
        {
            if (ModelState.IsValid)
            {
                var query = db.Products.Where(a => a.productCode ==  product.productCode && a.size == product.size && a.color == product.color).FirstOrDefault();
                if(query != null)
                {
                    TempData["ttPrd"] = "Sản phẩm đã tồn tại!";
                    ViewBag.ttPrd = TempData["ttPrd"] as string;

                    return RedirectToAction("Create");
                }
                product.ngayTao = DateTime.Now;
                if(FileAnh is null)
                {
                    product.Image = "";
                }
                else
                {
                    string rootFolder = Server.MapPath("/img/");
                    string pathEmail = rootFolder + FileAnh.FileName;
                    FileAnh.SaveAs(pathEmail);

                    product.Image = "/img/" + FileAnh.FileName;
                }
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
            var states = new List<SelectListItem>
            {
                new SelectListItem { Value = "S", Text = "S" },
                new SelectListItem { Value = "M", Text = "M" },
                new SelectListItem { Value = "L", Text = "L" },
                new SelectListItem { Value = "XL", Text = "XL" },
                new SelectListItem { Value = "XXL", Text = "XXL" },
                new SelectListItem { Value = "XXXL", Text = "XXXL" },
            };

            // Truyền danh sách các trạng thái vào ViewBag
            ViewBag.States = states;
            ViewBag.erEditPr = TempData["erEditPr"] as string;
            ViewBag.Category_category_id = new SelectList(db.Categories, "category_id", "name", product.Category_category_id);
            return View(product);
        }

        // POST: Admin_Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,productCode,SKU,description,price,name,size,color,quantity,stock,Category_category_id,Image")] Product product, HttpPostedFileBase FileAnh)
        {
            var existingPayment = db.Products.AsNoTracking().FirstOrDefault(p => p.product_id == product.product_id);
            if (ModelState.IsValid)
            {

                var query = db.Products.Where(a => a.productCode == product.productCode && a.size == product.size && a.color == product.color && a.product_id != product.product_id).FirstOrDefault();
                if (query != null)
                {
                    TempData["ttPrd2"] = "Sản phẩm đã tồn tại!";
                    ViewBag.ttPrd = TempData["ttPrd2"] as string;

                    return RedirectToAction("Edit");
                }
                product.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                if (FileAnh is null)
                {
                    var prdimage = db.Products.Where(x => x.product_id == product.product_id).AsNoTracking().FirstOrDefault();
                    string tmp = prdimage.Image ?? "";
                    product.Image = tmp;
                }
                else
                {
                    string rootFolder = Server.MapPath("/img/");
                    string pathEmail = rootFolder + FileAnh.FileName;
                    FileAnh.SaveAs(pathEmail);

                    product.Image = "/img/" + FileAnh.FileName;
                }
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
            ViewBag.erDeletePr = TempData["erDeletePr"] as string;
            return View(product);
        }

        // POST: Admin_Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erDeletePr"] = "Sản phẩm đang được sử dụng trong đơn hàng!";
                return RedirectToAction("Delete", new { id = product.product_id });
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        public ActionResult Search(string search_value)
        {
            ViewBag.value = search_value;
            var products = db.Products.Include(p => p.Category).Where(p => p.name.Contains(search_value)).ToList();
            if (products.Count > 0)
            {
                TempData["products_Search"] = products;
                return View(products);
            }
            else
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
            }

            return View(products);

        }
    }
}
