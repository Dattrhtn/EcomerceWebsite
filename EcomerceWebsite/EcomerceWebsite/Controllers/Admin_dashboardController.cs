using EcomerceWebsite.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebsite.Controllers
{
    public class OrderSummary
    {
        public int Nam { get; set; }
        public int SoLuongDonHang { get; set; }

        public string DoanhThu { get; set; }
    }

    public class OrderSummary2
    {
        public DateTime Nam { get; set; }
        public int SoLuongDonHang { get; set; }

        public string DoanhThu { get; set; }
    }

    public class spChay
    {
        public string productCode { get; set; }
        public string name { get; set; }

        public decimal? price { get; set; }
        public string image { get; set; }
        public int product_id { get; set; }
        public int totalOrders { get; set; }

    }

    public class spe
    {
        public string productCode { get; set; }
        public string name { get; set; }

        public decimal? price { get; set; }
        public string image { get; set; }
        public int product_id { get; set; }
        public int totalOrders { get; set; }


    }

    public class MyViewModel
    {
        public List<OrderSummary2> ngay { get; set; }
        public List<OrderSummary> thang { get; set; }
        public List<OrderSummary> nam { get; set; }

        public List<spChay> spBanChay { get; set; }

        public List<spe> spKhongBanDuoc { get; set; }
    }
    public class Admin_dashboardController : Controller
    {
        // GET: Admin_dashboard
        private ModelDB db = new ModelDB();
        public ActionResult Index()
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                var latestFourYears = Enumerable.Range(DateTime.Now.Year - 3, 4);
                var topThreeYears = db.Orders
                 .GroupBy(o => o.ngayTao.Year)
                .Select(g => new OrderSummary
                {
                    Nam = g.Key,
                    SoLuongDonHang = g.Count(),
                    DoanhThu = g.Sum(x => x.total_price ?? 0).ToString()
                })
                .ToList();
                var result = latestFourYears
               .GroupJoin(topThreeYears, year => year, data => data.Nam, (year, data) => new OrderSummary
               {
                   Nam = year,
                   SoLuongDonHang = data.Select(d => d.SoLuongDonHang).DefaultIfEmpty(0).FirstOrDefault(),
                   DoanhThu = data.Select(d => d.DoanhThu).DefaultIfEmpty("0").FirstOrDefault()
               })
               .ToList();


                var latestMonths = Enumerable.Range(1, DateTime.Now.Month);
                var topMonth = db.Orders
                 .GroupBy(o => o.ngayTao.Month)
                .Select(g => new OrderSummary
                {
                    Nam = g.Key,
                    SoLuongDonHang = g.Count(),
                    DoanhThu = g.Sum(x => x.total_price ?? 0).ToString()
                })
                .ToList();
                var result2 = latestMonths
               .GroupJoin(topMonth, month => month, data => data.Nam, (month, data) => new OrderSummary
               {
                   Nam = month,
                   SoLuongDonHang = data.Select(d => d.SoLuongDonHang).DefaultIfEmpty(0).FirstOrDefault(),
                   DoanhThu = data.Select(d => d.DoanhThu).DefaultIfEmpty("0").FirstOrDefault()
               })
               .ToList();



                var latestDates = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(-i).Date);

                var topDates = db.Orders.ToList()
                    .Where(o => o.ngayTao >= DateTime.Today.AddDays(-6)) // Lấy dữ liệu cho 7 ngày gần nhất
                    .ToList()
                    .GroupBy(o => o.ngayTao.Date)
                    .Select(g => new OrderSummary2
                    {
                        Nam = g.Key,
                        SoLuongDonHang = g.Count(),
                        DoanhThu = g.Sum(x => x.total_price ?? 0).ToString()
                    })
                    .ToList();

                var result3 = latestDates
                    .GroupJoin(topDates, date => date, data => data.Nam, (date, data) => new OrderSummary2
                    {
                        Nam = date,
                        SoLuongDonHang = data.Select(d => d.SoLuongDonHang).DefaultIfEmpty(0).FirstOrDefault(),
                        DoanhThu = data.Select(d => d.DoanhThu).DefaultIfEmpty("0").FirstOrDefault()
                    })
                    .ToList();




                var dtThang = db.Orders.Where(x => x.ngayTao.Month == DateTime.Now.Month && x.ngayTao.Year == DateTime.Now.Year).Sum(x => x.total_price);
                ViewBag.dtThang = dtThang;
                var dtNam = db.Orders.Where(x => x.ngayTao.Year == DateTime.Now.Year).Sum(x => x.total_price);
                ViewBag.dtNam = dtNam;
                var tongdt = db.Orders.Sum(x => x.total_price);
                ViewBag.tongdt = tongdt;

                var spBanChay = (from p in db.Products
                                 join oi in db.order_item on p.product_id equals oi.product_product_id into gj
                                 from subOrderItem in gj.DefaultIfEmpty()
                                 group subOrderItem by new { p.productCode, p.name, p.price, p.Image, p.product_id } into g
                                 orderby g.Count() descending
                                 select new spChay
                                 {
                                     productCode = g.Key.productCode,
                                     name = g.Key.name,
                                     price = g.Key.price,
                                     image = g.Key.Image,
                                     product_id = g.Key.product_id,
                                     totalOrders = g.Count()
                                 }).Take(5).ToList();

                var spKhongBanDuoc = (from p in db.Products
                                      join oi in db.order_item on p.product_id equals oi.product_product_id into gj
                                      from subOrderItem in gj.DefaultIfEmpty()
                                      group subOrderItem by new { p.productCode, p.name, p.price, p.Image, p.product_id } into g
                                      orderby g.Count() ascending
                                      select new spe
                                      {
                                          productCode = g.Key.productCode,
                                          name = g.Key.name,
                                          price = g.Key.price,
                                          image = g.Key.Image,
                                          product_id = g.Key.product_id,
                                          totalOrders = g.Count()
                                      }).Take(5).ToList();
                //ViewBag.spBanChay = spBanChay;
                //ViewBag.spKhongBanDuoc = spKhongBanDuoc;
                MyViewModel model = new MyViewModel
                {
                    ngay = result3,
                    thang = result2,
                    nam = result,
                    spBanChay = spBanChay,
                    spKhongBanDuoc = spKhongBanDuoc
                };
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }
    }
}