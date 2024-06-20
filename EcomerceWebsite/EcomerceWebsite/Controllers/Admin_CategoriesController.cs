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
    public class Admin_CategoriesController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_Categories
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var ac_id = Convert.ToInt32(Session["account_id"] as string );
                var currentac = db.accounts.FirstOrDefault(ac => ac.account_id == ac_id);
                if(currentac.role == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                var totalItems = db.Categories.ToList().Count;

                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var paginatedProducts = db.Categories.OrderBy(x => x.ngayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.TotalItems = totalItems;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;

                return View(paginatedProducts);
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Admin_Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin_Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,name")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.ngayTao = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["erAddCate"] = "Thêm danh mục sản phẩm thành công!";
                ViewBag.erAddCate = TempData["erAddCate"] as string;
                return View();
            }

            return View(category);
        }

        // GET: Admin_Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin_Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,name")] Category category)
        {
            var existingPayment = db.Categories.AsNoTracking().FirstOrDefault(p => p.category_id == category.category_id);
            if (ModelState.IsValid)
            {
                category.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["erEditCate"] = "Sửa danh mục sản phẩm thành công!";
                ViewBag.erEditCate = TempData["erEditCate"] as string;
                return View(category);
            }
            return View(category);
        }

        // GET: Admin_Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.erDeleteCate = TempData["erDeleteCate"] as string;
            return View(category);
        }

        // POST: Admin_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            try
            {
                
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["erDeleteCate"] = "Không thể xóa danh mục: danh mục đang chứa sản phẩm";
                return RedirectToAction("Delete",new { id = category.category_id});
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
    }
}
