using EcomerceWebsite.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class Client_ProductDetailController : Controller
    {
        // GET: Client_ProductDetail
        ModelDB db = new ModelDB();
        public ActionResult Index(int? product_id)
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
                //TempData["messeage_outOfStock"] as string;
                if (!(TempData["messeage_outOfStock"] as string).IsNullOrWhiteSpace())
                {
                    TempData["messeage_outOfStock_1"] = TempData["messeage_outOfStock"];
                }
                ViewBag.CurentAccount = TempData["name"] as string;
                if (product_id == null)
                {
                    product_id = int.Parse(TempData["product_id"] as string);
                }
                var account_id = int.Parse(Session["account_id"] as string);
                Session["numberOfCart"] = db.carts.Where(c => c.account_account_id == account_id).Count();
                var product = db.Products.Where(p => p.product_id == product_id).FirstOrDefault();
                if (product != null)
                {
                    var listImage = (from productImage in db.Products
                                     where productImage.productCode == product.productCode
                                     select productImage.Image).Distinct().ToList();
                    var listSize = db.Products.Where(p => p.productCode == product.productCode).Select(p => p.size).Distinct().ToList();
                    var listColor = db.Products.Where(p => p.productCode == product.productCode).Select(p => p.color).Distinct().ToList();
                    var cate_name = db.Categories.Where(c => c.category_id == product.Category_category_id).Select(c => c.name).FirstOrDefault();
                    var currentCateID = product.Category_category_id;
                    var listRelatedProduct = db.Products.Where(p => p.Category_category_id == currentCateID).GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList().Where(p => p.product_id != product.product_id);

                    ViewBag.product_id = product_id;
                    ViewBag.RealatedProducts = listRelatedProduct;
                    ViewBag.product_name = product.name;
                    ViewBag.product_price = product.price;
                    ViewBag.product_description = product.description;
                    ViewBag.SKU = product.SKU;
                    ViewBag.cate_name = cate_name;
                    ViewBag.listImage = listImage;
                    ViewBag.listSize = listSize;
                    ViewBag.listColor = listColor;
                    ViewBag.test = product_id;
                }
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult AddToCart(string product_id, string selected_color, string selected_size, string quantity_value)
        {
            var productId = Convert.ToInt32(product_id);
            if (string.IsNullOrEmpty(selected_size))
            {
                var first_Size = db.Products.Where(p => p.product_id == productId).Select(p => p.size).FirstOrDefault();
                selected_size = first_Size;
            }
            var current_quantity = int.Parse(quantity_value);
            if (string.IsNullOrEmpty(selected_color))
            {
                var first_color = db.Products.Where(p => p.product_id == productId).Select(p => p.color).FirstOrDefault();
                selected_color = first_color.ToString();
            }
            var color = Convert.ToInt32(selected_color);
            var productCode = db.Products.Where(p => p.product_id == productId).Select(p => p.productCode).FirstOrDefault();
            var product = db.Products.Where(p => p.color == color && p.size == selected_size && p.productCode == productCode).FirstOrDefault();
            if (product == null)
            {
                //product = db.Products.Where(p => p.product_id == productId).FirstOrDefault();
                TempData["messeage_outOfStock"] = "Kích thước và màu đã hết hàng. Vui lòng chọn kích thước và màu khác!";
                return RedirectToAction("Index", new { product_id = productId });
            }
            else
            {
                if (current_quantity > product.quantity)
                {
                    TempData["messeage_outOfStock"] = "Số lượng vượt quá số lượng sản phẩm còn lại trong kho!";
                    return RedirectToAction("Index", new { product_id = productId });
                }
                var account_id = int.Parse(Session["account_id"] as string);
                var check_product_ID = db.carts.Where(c => c.product_product_id == product.product_id && c.account_account_id == account_id).FirstOrDefault();
                if (check_product_ID == null)
                {
                    cart new_Cart_Item = new cart();
                    new_Cart_Item.quantity = int.Parse(quantity_value);
                    new_Cart_Item.product_product_id = product.product_id;
                    new_Cart_Item.account_account_id = account_id;
                    new_Cart_Item.ngayTao = DateTime.Now;
                    db.carts.Add(new_Cart_Item);
                    db.SaveChanges();
                }
                else
                {
                    check_product_ID.quantity = int.Parse(quantity_value);
                    db.Entry(check_product_ID).State = EntityState.Modified;
                    db.SaveChanges();
                }
                TempData["product_id"] = product_id;
                return RedirectToAction("Index");
            }
        }
    }
}