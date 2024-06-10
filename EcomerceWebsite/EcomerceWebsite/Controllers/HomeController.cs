using EcomerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace EcomerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        ModelDB db = new ModelDB();
        public ActionResult Index()
        {
            Session["numberOfCart"] = db.carts.Count();
            return View();
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
    }
}