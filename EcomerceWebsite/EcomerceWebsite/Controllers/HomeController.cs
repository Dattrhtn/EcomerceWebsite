using EcomerceWebsite.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace EcomerceWebsite.Controllers
{
    public class HomeController : Controller
    {

    ModelDB db = new ModelDB();
    private List<Product> GetBestSellingProducts()
    {
        var bestSellingProducts = (from product in db.Products
                                   join orderItem in db.order_item on product.product_id equals orderItem.product_product_id
                                   group orderItem by product into g
                                   orderby g.Sum(oi => oi.quantity) descending
                                   select g.Key).Take(3).ToList();

        return bestSellingProducts;
    }
    private List<Product> GetNewestProducts()
    {
        var newestProducts = db.Products.OrderByDescending(p => p.ngayTao).Take(3).ToList();
        return newestProducts;
    }

    public ActionResult Index()
    {
        Session["TramgChu_active"] = "active";
        if (Session["TramgChu_active"] != null && Session["TramgChu_active"].ToString() == "active")
        {
            // Xóa các session khác
            Session.Remove("Contact_active");
            Session.Remove("Blog_active");
            Session.Remove("Cuahang_active");
        }

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
            ViewBag.NewestProducts = GetNewestProducts(); // Thêm dòng này

        return View();
    }

    public ActionResult ExportToExcel(string code)
    {
        var query = Enumerable.Empty<object>();
        if (code == "Account")
            query = db.accounts.ToList();
        else if (code == "Product")
            query = db.Products.ToList();
        else if (code == "Order")
            query = db.Orders.ToList();
        else if (code == "Category")
            query = db.Categories.ToList();
        else if (code == "Payment")
            query = db.Payments.ToList();
        else if (code == "Shipment")
            query = db.Payments.ToList();

        var grid = new GridView();
        grid.DataSource = query;
        grid.DataBind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", $"attachment; filename=DanhSach{code}.xls");
        Response.ContentType = "application/ms-excel";

        Response.Charset = "";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        grid.RenderControl(htw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

            return View("MyView");
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

}