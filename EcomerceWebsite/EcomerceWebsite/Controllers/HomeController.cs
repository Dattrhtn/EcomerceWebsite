using EcomerceWebsite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class HomeController : Controller
{
    ModelDB db = new ModelDB();

    public ActionResult Index()
    {
        if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
        {
            ViewBag.CurentAccount = Session["name"] as string;
            var account_id = int.Parse(Session["account_id"] as string);
            Session["numberOfCart"] = db.carts.Where(c => c.account_account_id == account_id).Count();
        }
        else
        {
            Session["numberOfCart"] = 0;
        }

        ViewBag.BestSellingProducts = GetBestSellingProducts();

        return View();
    }

    private List<Product> GetBestSellingProducts()
    {
        var bestSellingProducts = (from product in db.Products
                                   join orderItem in db.order_item on product.product_id equals orderItem.product_product_id
                                   group orderItem by product into g
                                   orderby g.Sum(oi => oi.quantity) descending
                                   select g.Key).Take(3).ToList();

        return bestSellingProducts;
    }

    public ActionResult About()
    {
        ViewBag.Message = "Your application description page.";
        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Your contact page.";

        return View();
    }
}
