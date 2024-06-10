//using EcomerceWebsite.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace EcomerceWebsite.Controllers
//{
//    public class Client_DangNhap_DangKyController : Controller
//    {
//        // GET: Client_DangNhap_DangKy
//        ModelDB db = new ModelDB();
//        public ActionResult Index()
//        {
//            return View();
//        }
//        public ActionResult DangNhap()
//        {
//            ViewBag.mess = "";
//            var username = Request["username"];
//            var pass = Request["pass"];
//            var currentAccount = db.accounts.Where(ac => ac.username == (username) && ac.password == pass).FirstOrDefault();
//            if (currentAccount != null)
//            {
//                if(currentAccount.role == 1)
//                {
//                    return RedirectToAction("Index", "Admin_dashboard");
//                }
//                Session["Username"] = username;
//                Session["IsAuthenticated"] = true;
//                Session["name"] = currentAccount.first_name;
//                Session["account_id"] = currentAccount.account_id.ToString();
//                return RedirectToAction("Index", "Home");
//            }
//            TempData["mess"] = "Username hoặc password không đúng!";
//            return RedirectToAction("Index", "Login");
//        }
//        public ActionResult DangKy(string username, string pass, string last_name, string first_name, string phone_number, string address, string email)
//        {
//            ViewBag.mess = TempData["mess"] as string;
//            return View();
//        }
//        [HttpPost]
//        public ActionResult DangKyAccount(string username, string pass, string last_name, string first_name, string phone_number, string address, string email)
//        {
//            ViewBag.mess = "";
//             var newAccount = new account();
//            var checkUser = db.accounts.Where(ac => ac.username == username).Select(ac => ac.username).FirstOrDefault();
//            if (checkUser != null)
//            {
//                TempData["mess"] = "Username đã tồn tại!";
//                return RedirectToAction("DangKy");
//            }
//            newAccount.username = username;
//            newAccount.password = pass;
//            newAccount.last_name = last_name;
//            newAccount.first_name = first_name;
//            newAccount.phone_number = phone_number;
//            newAccount.address = address;
//            newAccount.email = email;
//            newAccount.role = 0;
//            newAccount.ngayTao = DateTime.Now;
//            try
//            {
//                db.accounts.Add(newAccount);
//                db.SaveChanges();
//                TempData["mess"] = "Đăng ký thành công!";
//            }
//            catch (Exception e)
//            {
//                TempData["mess"] = "Đăng ký thất bại!" + e.Message;
//            }
//            return RedirectToAction("DangKy");
//        }
//    }
//}