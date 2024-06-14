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
    public class Admin_shipmentsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_shipments
        public ActionResult Index()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var shipments = db.shipments.Include(s => s.account);
                return View(shipments.ToList());
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Admin_shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shipment shipment = db.shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Admin_shipments/Create
        public ActionResult Create()
        {
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name");
            return View();
        }

        // POST: Admin_shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "shipment_id,shipment_date,address,city,state,country,zip_code,account_account_id")] shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.ngayTao = DateTime.Now;
                db.shipments.Add(shipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", shipment.account_account_id);
            return View(shipment);
        }

        // GET: Admin_shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shipment shipment = db.shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            var states = new List<SelectListItem>
            {
                new SelectListItem { Value = "Chờ xác nhận", Text = "Chờ xác nhận" },
                new SelectListItem { Value = "Đang xử lý", Text = "Đang xử lý" },
                new SelectListItem { Value = "Đang giao hàng", Text = "Đang giao hàng" },
                new SelectListItem { Value = "Giao hàng thành công", Text = "Giao hàng thành công" },
                new SelectListItem { Value = "Đã hủy", Text = "Đã hủy" },
            };

            // Truyền danh sách các trạng thái vào ViewBag
            ViewBag.States = states;
            ViewBag.erEditPm = TempData["erEditPm"] as string;
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", shipment.account_account_id);
            return View(shipment);
        }

        // POST: Admin_shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "shipment_id,shipment_date,address,city,state,country,zip_code,account_account_id")] shipment shipment)
        {
            var existingPayment = db.shipments.AsNoTracking().FirstOrDefault(p => p.shipment_id == shipment.shipment_id);
            if (ModelState.IsValid)
            {
                shipment.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                TempData["erEditPm"] = "Thay đổi trạng thái giao hàng thành công!";
                ViewBag.erEditPm = TempData["erEditPm"] as string;
                return RedirectToAction("Edit");
            }
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", shipment.account_account_id);
            return View(shipment);
        }

        // GET: Admin_shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shipment shipment = db.shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Admin_shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            shipment shipment = db.shipments.Find(id);
            db.shipments.Remove(shipment);
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
