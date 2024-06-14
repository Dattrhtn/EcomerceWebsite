using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EcomerceWebsite.Models;

namespace EcomerceWebsite.Controllers
{
    public class Admin_accountsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_accounts
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var totalItems = db.accounts.ToList().Count;
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var paginatedProducts = db.accounts.OrderBy(x => x.ngayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.TotalItems = totalItems;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;

                return View(paginatedProducts);
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Admin_accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin_accounts/Create
        public ActionResult Create()
        {
            var model = new account(); // Thay YourModel bằng tên thực của model của bạn
            model.role = 0;
            return View(model);
        }

        // POST: Admin_accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "account_id,first_name,last_name,email,password,address,role,phone_number")] account account)
        {
            if (ModelState.IsValid)
            {
                account.ngayTao = DateTime.Now;
                db.accounts.Add(account);
                db.SaveChanges();
                TempData["erAddAc"] = "Thêm tài khoản thành công!";
                ViewBag.erAddAc = TempData["erAddAc"] as string;
                return View();
            }

            return View(account);
        }

        // GET: Admin_accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin_accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "account_id,first_name,last_name,email,password,address,role,phone_number")] account account)
        {
            var existingPayment = db.accounts.AsNoTracking().FirstOrDefault(p => p.account_id == account.account_id);
            if (ModelState.IsValid)
            {
                account.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                TempData["erEditAC"] = "Sửa thông tin tài khoản thành công!";
                ViewBag.erEditAC = TempData["erEditAC"] as string;
                return View(account);
            }
            return View(account);
        }

        // GET: Admin_accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.erDeleteAc = TempData["erDeleteAc"] as string;
            return View(account);
        }

        // POST: Admin_accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            try { 
                
                db.accounts.Remove(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["erDeleteAc"] = "Tài khoản đang được sử dụng!";
                return RedirectToAction("Delete", new { id = account.account_id});
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
