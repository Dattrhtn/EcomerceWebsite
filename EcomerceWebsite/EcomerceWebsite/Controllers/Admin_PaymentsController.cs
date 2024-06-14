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
    public class Admin_PaymentsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_Payments

        public ActionResult Index()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                return View(db.Payments.ToList());
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Admin_Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Admin_Payments/Create
        public ActionResult Create()
        {
            ViewBag.erAddPa = TempData["erAddPa"] as string;
            return View();
        }

        // POST: Admin_Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payment_id,payment_method")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.ngayTao = DateTime.Now;
                db.Payments.Add(payment);
                db.SaveChanges();
                TempData["erAddPa"] = "Thêm Phương thức thanh toán thành công!";
                ViewBag.erAddPa = TempData["erAddPa"] as string;
                return RedirectToAction("Create");
            }

            return View(payment);
        }

        // GET: Admin_Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.erEditPa = TempData["erEditPa"] as string;
            return View(payment);
        }

        // POST: Admin_Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payment_id,payment_method")] Payment payment)
        {
            var existingPayment = db.Payments.AsNoTracking().FirstOrDefault(p => p.payment_id == payment.payment_id);
            if (ModelState.IsValid)
            {
                payment.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                TempData["erEditPa"] = "Sửa thông tin phương thức thanh toán thành công!";
                ViewBag.erEditPa = TempData["erEditPa"] as string;
                return RedirectToAction("Edit");
            }
            return View(payment);
        }

        // GET: Admin_Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.erDeletePayment = TempData["erDeletePayment"] as string;
            return View(payment);
        }

        // POST: Admin_Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            try
            {
                db.Payments.Remove(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["erDeletePayment"] = "Phương thức thanh toán đang được sử dụng!";
                return RedirectToAction("Delete", new { id = payment.payment_id });
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
