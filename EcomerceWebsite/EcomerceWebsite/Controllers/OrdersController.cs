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
    public class OrdersController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.account).Include(o => o.Payment).Include(o => o.shipment);
            return View(orders.ToList());

        }

        // GET: Orders/Details/5
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

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name");
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method");
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address");

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,total_price,account_account_id,Payment_payment_id,Shipment_shipment_id")] Order order, [Bind(Include = "shipment_id,shipment_date,address,city,state,country,zip_code,account_account_id")] shipment shipment )
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                   // try
                    {
                        // Tạo đối tượng giao hàng
                        var account_id = int.Parse(Session["account_id"] as string);
                        var total_price = (from product in db.Products
                                           join cart in db.carts
                                           on product.product_id equals cart.product_product_id
                                           where cart.account_account_id == account_id
                                           select (cart.quantity * product.price)).Sum();

                        var newShipment = new shipment
                        {
                            address = shipment.address,
                            city = shipment.city,
                            country = shipment.country,
                            zip_code =shipment.zip_code,
                            account_account_id = account_id,
                            state = "Chờ xác nhận",
                            ngayTao = DateTime.Now,
                            shipment_date = DateTime.Now.AddDays(2)

                        };
                        db.shipments.Add(newShipment);
                        db.SaveChanges();
                        var id_shipment = db.shipments.OrderByDescending(s => s.ngayTao).Select(s => s.shipment_id).FirstOrDefault();
                        // Tạo đơn hàng với shipment_id đã được cập nhật
                        var newOrder = new Order
                        {
                            total_price = total_price,
                            account_account_id = account_id,
                            Payment_payment_id = Convert.ToInt32(order.Payment_payment_id),                            
                            Shipment_shipment_id = id_shipment,
                            ngayTao = DateTime.Now
                        };
                        db.Orders.Add(newOrder);
                        db.SaveChanges();
                        var id_order = db.Orders.OrderByDescending(s => s.ngayTao).Select(s => s.order_id).FirstOrDefault();
                        // Tạo các mặt hàng trong đơn hàng
                        var cartJoinProduct = (from p in db.Products
                                               join c in db.carts
                                               on p.product_id equals c.product_product_id
                                               where c.account_account_id == account_id
                                               select new
                                               {
                                                   product_id = p.product_id,
                                                   quantity = c.quantity,
                                                   price = p.price
                                               }).ToList();
                        foreach (var item in cartJoinProduct)
                        {
                            var orderItem = new order_item
                            {
                                quantity = item.quantity,
                                price = item.price,
                                product_product_id = item.product_id,
                                order_order_id = id_order,
                                ngayTao = DateTime.Now
                            };
                            db.order_item.Add(orderItem);
                            var product = db.Products.Where(p => p.product_id == item.product_id).FirstOrDefault();
                            product.quantity = product.quantity - Convert.ToInt32(item.quantity);
                            db.Entry(product).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.SaveChanges();

                        var listCart = db.carts.Where(c => c.account_account_id == account_id).ToList();
                        db.carts.RemoveRange(listCart);
                        db.SaveChanges();
                        Session["numberOfCart"] = db.carts.Where(c => c.account_account_id == account_id).Count();
                        // Commit transaction
                        transaction.Commit();

                        return RedirectToAction("OrderSuccess");
                    }
                    //catch (Exception ex)
                    //{
                    //    // Rollback transaction nếu có lỗi xảy ra
                    //    transaction.Rollback();
                    //    ModelState.AddModelError("", "Có lỗi xảy ra khi xử lý đơn hàng của bạn. Vui lòng thử lại.");
                    //    // Log lỗi nếu cần thiết
                    //}
                }
            }
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", order.account_account_id);
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method", order.Payment_payment_id);
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address", order.Shipment_shipment_id);
            return View("Checkout", order);
        }

        // GET: Orders/Edit/5
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
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", order.account_account_id);
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method", order.Payment_payment_id);
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address", order.Shipment_shipment_id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,total_price,account_account_id,Payment_payment_id,Shipment_shipment_id,ngayTao")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.account_account_id = new SelectList(db.accounts, "account_id", "first_name", order.account_account_id);
            ViewBag.Payment_payment_id = new SelectList(db.Payments, "payment_id", "payment_method", order.Payment_payment_id);
            ViewBag.Shipment_shipment_id = new SelectList(db.shipments, "shipment_id", "address", order.Shipment_shipment_id);
            return View(order);
        }

        // GET: Orders/Delete/5
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

        // POST: Orders/Delete/5
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

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}
