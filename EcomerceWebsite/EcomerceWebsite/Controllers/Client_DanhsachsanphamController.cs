using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EcomerceWebsite.Models;
using Microsoft.Ajax.Utilities;

namespace EcomerceWebsite.Controllers
{
    public class Client_DanhsachsanphamController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Client_Danhsachsanpham
        [HttpGet]
        public ActionResult Index(int page = 1, int pageSize = 12)
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
                Session["numberOfCart"] = db.carts.Where(c => c.account_account_id == account_id).Count();
                List<Product> products_Search = TempData["products_Search"] as List<Product>;
                List<Product> productsByCategory = TempData["products_ByCategory"] as List<Product>;
                List<Product> productsBySize = TempData["products_BySize"] as List<Product>;
                List<Product> productsByColor = TempData["products_ByColor"] as List<Product>;
                List<Product> productsByPrice = TempData["products_ByPrice"] as List<Product>;
                List<Product> productsSortByPrice = TempData["products_SortByPrice"] as List<Product>;
                string messAddError = TempData["MessAddCateError"] as string;
                if (messAddError != "")
                {
                    ViewBag.messAddError = messAddError;
                }
                var products = db.Products.Include(p => p.Category);
                var categories = db.Categories.ToList();

                var list_products = products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList();
                TempData["list"] = categories;
                ViewBag.listCate = TempData["list"] as List<Category>;

                if (products_Search != null && products_Search.Count > 0)
                {
                    list_products = products_Search;
                }
                if (productsByCategory != null && productsByCategory.Count > 0)
                {
                    list_products = productsByCategory;
                }
                if (productsBySize != null && productsBySize.Count > 0)
                {
                    list_products = productsBySize;
                }
                if (productsByColor != null && productsByColor.Count > 0)
                {
                    list_products = productsByColor;
                }
                if (productsByPrice != null && productsByPrice.Count > 0)
                {
                    list_products = productsByPrice;
                }
                if (productsSortByPrice != null && productsSortByPrice.Count > 0)
                {
                    list_products = productsSortByPrice;
                }

                // Phân trang
                var totalItems = list_products.Count;
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var paginatedProducts = list_products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.TotalItems = totalItems;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;

                return View(paginatedProducts);
            }
                return RedirectToAction("Index", "Login");

            
        }

        // GET: Client_Danhsachsanpham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Search(string search_value)
        {
            ViewBag.value = search_value;
            var products = db.Products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).Where(p => p.name.Contains(search_value)).ToList();
            if (products.Count > 0)
            {
                TempData["products_Search"] = products;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
            }
            return View();

        }

        public ActionResult SearchbyCategory(int? category_id)
        {
            var productsByCategory = db.Products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).Where(p => p.Category_category_id == category_id).ToList();
            if (productsByCategory.Count > 0)
            {
                TempData["products_ByCategory"] = productsByCategory;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
            }
            return View();
        }

        public ActionResult SearchbySize(string size)
        {
            var products = db.Products.Where(p => p.size == size).ToList();
            var productsBySize = products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList();
            if (productsBySize.Count > 0)
            {
                TempData["products_BySize"] = productsBySize;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
            }
            return View();
        }

        public ActionResult SearchbyColor(int? color)
        {
            var products = db.Products.Where(p => p.color == color).ToList();
            var productsByColor = products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList();
            if (productsByColor.Count > 0)
            {
                TempData["products_ByColor"] = productsByColor;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
            }
            return View();
        }

        public ActionResult SearchByPrice(string min_price, string max_price)
        {
            if (min_price.Trim() == "" || max_price.Trim() == "")
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
                return View();
            }
            int min_value = Convert.ToInt32(min_price);
            int max_value = Convert.ToInt32(max_price);
            var products = db.Products.Where(p => p.price >= min_value && p.price <= max_value).ToList();
            var productsByPrice = products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList();
            if (productsByPrice.Count > 0)
            {
                TempData["products_ByPrice"] = productsByPrice;
                return RedirectToAction("Index");
            }
            ViewBag.Messeage = "Không tồn tại sản phẩm!";
            return View();
        }
        public ActionResult SortbyPrice()
        {
            var products = db.Products.OrderByDescending(p => p.price).ToList();
            var productsSortByPrice = products.GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList();
            if (productsSortByPrice.Count > 0)
            {
                TempData["products_SortByPrice"] = productsSortByPrice;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Messeage = "Không tồn tại sản phẩm!";
            }
            return View();
        }

        public ActionResult AddToCart(int? product_id)
        {
            try
            {
                var product = db.Products.Where(p => p.product_id == product_id).FirstOrDefault();
                var account_id = int.Parse(Session["account_id"] as string);
                var check_product_ID = db.carts.Where(c => c.product_product_id == product_id && c.account_account_id == account_id).FirstOrDefault();
                if (check_product_ID == null)
                {
                    cart new_Cart_Item = new cart();
                    new_Cart_Item.quantity = 1;
                    new_Cart_Item.product_product_id = product.product_id;
                    new_Cart_Item.account_account_id = account_id;
                    new_Cart_Item.ngayTao = DateTime.Now;
                    db.carts.Add(new_Cart_Item);
                    db.SaveChanges();
                }
                else
                {
                    check_product_ID.quantity = check_product_ID.quantity + 1;
                    db.Entry(check_product_ID).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //product.quantity = product.quantity - 1;
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
            }
            catch (Exception e)
            {
                TempData["MessAddCateError"] = "Thêm sản phẩm lỗi!" + e.InnerException?.Message;
            }
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
