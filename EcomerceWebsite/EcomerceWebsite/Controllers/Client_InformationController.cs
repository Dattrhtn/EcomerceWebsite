using EcomerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class Client_InformationController : Controller
    {
        ModelDB db = new ModelDB();
        public ActionResult Index()
        {
            var currentUserId = 1; // Giả sử account_id được lưu trong User.Identity.Name
            var account = db.accounts
                            .FirstOrDefault(a => a.account_id == currentUserId);

            if (account == null)
            {
                return HttpNotFound("Account not found");
            }

            return View(account);
    }
    }
}