using EcomerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        ModelDB db = new ModelDB();
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
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
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
    }
}