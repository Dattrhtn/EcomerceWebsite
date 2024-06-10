using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class Client_BlogController : Controller
    {
        // GET: Client_Blog
        public ActionResult Index()
        {
            Session["Blog_active"] = "active";
            if (Session["Blog_active"] != null && Session["Blog_active"].ToString() == "active")
            {
                // Xóa các session khác
                Session.Remove("Contact_active");
                Session.Remove("TramgChu_active");
                Session.Remove("Cuahang_active");
            }
            return View();
        }
        public ActionResult BlogDetail1()
        {
            return View();
        }
        public ActionResult BlogDetail2()
        {
            return View();
        }
        public ActionResult BlogDetail3()
        {
            return View();
        }
    }
}