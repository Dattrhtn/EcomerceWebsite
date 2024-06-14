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
    public class Client_DanhSachOrdersController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Client_DanhSachOrder
        public ActionResult Index()
        {
            var account_id = int.Parse(Session["account_id"] as string);
            var currentUserId = account_id; // Giả sử account_id được lưu trong User.Identity.Name
            var orders = db.Orders
                           .Include(o => o.account)
                           .Include(o => o.Payment)
                           .Include(o => o.shipment)
                           .Where(o => o.account.account_id == currentUserId);
            return View(orders.ToList());
        }

        // GET: Client_DanhSachOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Include(o => o.order_item).FirstOrDefault(o => o.order_id == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Client_DanhSachOrder/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            var account_id = int.Parse(Session["account_id"] as string);
            Order order = db.Orders.Include(o => o.shipment).FirstOrDefault(o => o.order_id == id);
            var oder_oder_item = (from od in db.Orders
                                  join ot in db.order_item
                                  on od.order_id equals ot.order_order_id
                                  where od.order_id == id
                                  select ot).ToList();
                                
                                 
            foreach (var item in oder_oder_item)
            {

                var product = db.Products.Where(p => p.product_id == item.product_product_id).FirstOrDefault();
                product.quantity = product.quantity + Convert.ToInt32(item.quantity);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.shipment.state == "Chờ xác nhận")
            {
                order.shipment.state = "Đã hủy";
                db.SaveChanges();
                TempData["Message"] = "Đơn hàng đã được hủy thành công.";
            }
            else
            {
                TempData["Error"] = "Đơn hàng không thể hủy được.";
            }

            return RedirectToAction("Details", new { id = id });
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
