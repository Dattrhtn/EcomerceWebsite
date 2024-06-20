using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EcomerceWebsite.Models;
using Microsoft.Ajax.Utilities;

namespace EcomerceWebsite.Controllers
{
    public class Client_WishlistController : Controller
    {
        ModelDB db = new ModelDB();
        // GET: Client_Wishlist
        public ActionResult Index()
        {
            Session["Cuahang_active"] = "active";
            if (Session["Cuahang_active"] != null && Session["Cuahang_active"].ToString() == "active")
            {
                // Xóa các session khác
                Session.Remove("Contact_active");
                Session.Remove("Blog_active");
                Session.Remove("TramgChu_active");
            }
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var account_id = int.Parse(Session["account_id"] as string);
                Session["numberOfWishlist"] = db.wishlists.Where(w => w.account_account_id == account_id).Count();
                var wishlist = db.wishlists.Include(c => c.account).Include(c => c.Product).Where(c => c.account_account_id == account_id);
                return View(wishlist.ToList());
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Xoa(int? id)
        {
            var current_account = int.Parse(Session["account_id"] as string);
            wishlist wishlist = db.wishlists.Where(c => c.wishlist_id == id && c.account_account_id == current_account).FirstOrDefault();
            if (wishlist != null)
            {
                db.wishlists.Remove(wishlist);

                db.SaveChanges();
            }
            // Lấy thông tin về controller hiện tại
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            // Chuyển hướng đến action Index của controller hiện tại
            return RedirectToAction("Index", currentController);
        }

        public ActionResult AddToWishlist(string product_id, string selected_color, string selected_size)
        {
                var productId = Convert.ToInt32(product_id);
                if (string.IsNullOrEmpty(selected_size))
                {
                    var first_Size = db.Products.Where(p => p.product_id == productId).Select(p => p.size).FirstOrDefault();
                    selected_size = first_Size;
                }
                if (string.IsNullOrEmpty(selected_color))
                {
                    var first_color = db.Products.Where(p => p.product_id == productId).Select(p => p.color).FirstOrDefault();
                    selected_color = first_color.ToString();
                }
                var color = Convert.ToInt32(selected_color);
                var productCode = db.Products.Where(p => p.product_id == productId).Select(p => p.productCode).FirstOrDefault();
                var product = db.Products.Where(p => p.color == color && p.size == selected_size && p.productCode == productCode).FirstOrDefault();
                var account_id = int.Parse(Session["account_id"] as string);
                var check_product_ID = db.wishlists.Where(w => w.product_product_id == product.product_id && w.account_account_id == account_id).FirstOrDefault();
                if (check_product_ID == null)
                {
                    wishlist new_wishlist = new wishlist();
                    new_wishlist.account_account_id = account_id;
                    new_wishlist.product_product_id = product.product_id;
                    new_wishlist.ngayTao = DateTime.Now;

                    db.wishlists.Add(new_wishlist);
                    db.SaveChanges();

                    var dswl = new List<int>();
                    dswl.Add(productId);
                }
                TempData["product_id"] = product_id;
                return RedirectToAction("Index", "Client_ProductDetail");
        }

        public ActionResult check()
        {
            return View();
        }
    }
}