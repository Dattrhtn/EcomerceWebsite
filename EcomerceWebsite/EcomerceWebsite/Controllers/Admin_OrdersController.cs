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
    public class Admin_OrdersController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_Orders
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var orders = db.Orders.Include(o => o.account).Include(o => o.Payment).Include(o => o.shipment);
                var totalItems = orders.ToList().Count;

                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var paginatedProducts = orders.OrderBy(x => x.ngayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.TotalItems = totalItems;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;

                return View(paginatedProducts);
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Admin_Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin_Orders/Create
        public ActionResult Create()
        {
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name");
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method");
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address");
            return View();
        }

        // POST: Admin_Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,total_price,account_account_id,Payment_payment_id,Shipment_shipment_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.ngayTao = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", order.account_account_id);
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method", order.Payment_payment_id);
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address", order.Shipment_shipment_id);
            return View(order);
        }

        // GET: Admin_Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var itemShipment = db.shipments.FirstOrDefault(p => p.shipment_id == order.Shipment_shipment_id);
            string tmp = ""; 
            tmp = itemShipment.address?.ToString() + " " + itemShipment.city?.ToString() + " " + itemShipment.country?.ToString();
            ViewBag.address = tmp;
            ViewBag.zipCode = itemShipment.zip_code;
            ViewBag.shipDate = itemShipment.shipment_date;
            ViewBag.state = itemShipment.state;

            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", order.account_account_id);
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method", order.Payment_payment_id);
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address", order.Shipment_shipment_id);
            return View(order);
        }

        // POST: Admin_Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,total_price,account_account_id,Payment_payment_id,Shipment_shipment_id")] Order order)
        {
            var existingPayment = db.Orders.AsNoTracking().FirstOrDefault(p => p.order_id == order.order_id);

            if (ModelState.IsValid)
            {
                order.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", order.account_account_id);
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method", order.Payment_payment_id);
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address", order.Shipment_shipment_id);
            return View(order);
        }

        // GET: Admin_Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin_Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
