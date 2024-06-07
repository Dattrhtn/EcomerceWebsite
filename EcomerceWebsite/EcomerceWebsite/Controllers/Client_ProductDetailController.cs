using EcomerceWebsite.Models;
using System;
using System.Collections.Generic;
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
            var product = db.Products.Where(p => p.product_id == product_id).FirstOrDefault();
            if (product != null)
            {
                var listImage = (from productImage in db.Products
                                 where productImage.productCode == product.productCode
                                 select productImage.Image).Distinct().ToList();
                var cate_name = db.Categories.Where(c => c.category_id == product.Category_category_id).Select(c => c.name).FirstOrDefault();
                var currentCateID = product.Category_category_id;
                var listRelatedProduct = db.Products.Where(p => p.Category_category_id == currentCateID).GroupBy(p => p.productCode).Select(p => p.FirstOrDefault()).ToList().Where(p => p.product_id != product.product_id);


                ViewBag.RealatedProducts = listRelatedProduct;
                ViewBag.product_name = product.name;
                ViewBag.product_price = product.price;
                ViewBag.product_description = product.description;
                ViewBag.SKU = product.SKU;
                ViewBag.cate_name = cate_name;
                ViewBag.listImage = listImage;
                ViewBag.test = product_id;
            }
            return View();
        }
    }
}