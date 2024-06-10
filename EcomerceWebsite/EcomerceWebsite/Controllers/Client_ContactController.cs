using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class Client_ContactController : Controller
    {
        // GET: Client_Contact
        public ActionResult Index()
        {
            Session["Contact_active"] = "active";
            if (Session["Contact_active"] != null && Session["Contact_active"].ToString() == "active")
            {
                // Xóa các session khác
                Session.Remove("Cuahang_active");
                Session.Remove("Blog_active");
                Session.Remove("TramgChu_active");
            }
            return View();
        }
    }
}