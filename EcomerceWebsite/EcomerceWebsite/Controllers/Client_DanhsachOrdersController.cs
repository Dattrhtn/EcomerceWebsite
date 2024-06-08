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
            // order_item order_item1 =  new order_item();
            //order_item1.quantity = 2;
            //order_item1.price = 700;
            //order_item1.product_product_id = 1;
            //order_item1.order_order_id = 1;
            //order_item1.ngayTao = DateTime.Now;
            //db.order_item.Add(order_item1);
            //db.SaveChanges();

            var currentUserId = 1;

            // Giả sử account_id được lưu trong User.Identity.Name
            // hoặc bạn có thể có cách khác để lấy account_id của người dùng
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
