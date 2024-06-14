using EcomerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class Client_DangNhap_DangKyController : Controller
    {
        // GET: Client_DangNhap_DangKy
        ModelDB db = new ModelDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            ViewBag.mess = "";
            var email = Request["mail"];
            var pass = Request["pass"];
            var currentAccount = db.accounts.Where(ac => ac.email == (email) && ac.password == pass).FirstOrDefault();
            if (currentAccount != null)
            {
                Session["IsAuthenticated"] = true;
                Session["name"] = currentAccount.first_name;
                Session["account_id"] = currentAccount.account_id.ToString();
                if (currentAccount.role == 1)
                {
                    return RedirectToAction("Index", "Admin_dashboard");
                }
                return RedirectToAction("Index", "Home");
            }
            TempData["mess"] = "Email hoặc password không đúng!";
            return RedirectToAction("Index", "Login");
        }
      
        public ActionResult DangKy()
        {
            ViewBag.mess = TempData["mess"] as string;
            return View();
        }
        [HttpPost]
        public ActionResult DangKyAccount(string pass, string last_name, string first_name, string phone_number, string address, string email)
        {
            ViewBag.mess = "";
             var newAccount = new account();
            var checkEmail = db.accounts.Where(ac => ac.email == email).Select(ac => ac.email).FirstOrDefault();
            if (checkEmail != null)
            {
                TempData["mess"] = "Email đã tồn tại!";
                return RedirectToAction("DangKy");
            }
            newAccount.password = pass;
            newAccount.last_name = last_name;
            newAccount.first_name = first_name;
            newAccount.phone_number = phone_number;
            newAccount.address = address;
            newAccount.email = email;
            newAccount.role = 0;
            newAccount.ngayTao = DateTime.Now;
            try
            {
                db.accounts.Add(newAccount);
                db.SaveChanges();
                TempData["mess"] = "Đăng ký thành công!";
            }
            catch (Exception e)
            {
                TempData["mess"] = "Đăng ký thất bại!" + e.Message;
            }
            return RedirectToAction("DangKy");
        }
    }
}
