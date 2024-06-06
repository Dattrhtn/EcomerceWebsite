using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Index()
        {
            List<Product> products_Search = TempData["products_Search"] as List<Product>;
            List<Product> productsByCategory = TempData["products_ByCategory"] as List<Product>;
            List<Product> productsBySize = TempData["products_BySize"] as List<Product>;
            List<Product> productsByColor = TempData["products_ByColor"] as List<Product>;
            List<Product> productsByPrice = TempData["products_ByPrice"] as List<Product>;
            List<Product> productsSortByPrice = TempData["products_SortByPrice"] as List<Product>;
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
            return View(list_products);
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
            ViewBag.Messeage = product_id;
            return View();
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
