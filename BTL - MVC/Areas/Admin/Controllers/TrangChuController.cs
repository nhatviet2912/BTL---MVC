using BTL___MVC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult TrangChu()
        {
            ViewBag.tongdoanhthu = TongDoanhThu();
            ViewBag.tongdonhang = TongDonHangDaBan();
            ViewBag.soluongphanhoi = TongSoLuongPhanHoi();
            ViewBag.doanhthutheongay = TongDoanhThuTheoNgay(DateTime.Now.Day, DateTime.Now.Month);
            ViewBag.doanhthutheothang = TongDoanhThuTheoThang(DateTime.Now.Month);
            return View();
        }

        public decimal TongDoanhThu()
        {
            decimal tongtien = 0;
            var getdh = db.DonHangs.Where(s => s.Trangthai == 2);
            foreach(var x in getdh)
            {
                tongtien += Convert.ToDecimal(x.CTDHs.Sum(n => n.Tongtien).Value);
            }
            return tongtien;
        }

        public int TongDonHangDaBan()
        {
            int tongdonhang = db.DonHangs.Count(n => n.Trangthai == 2);
            return tongdonhang;
        }

        public int TongSoLuongPhanHoi()
        {
            int slphanhoi = db.LienHes.Count();
            return slphanhoi;
        }

        public decimal TongDoanhThuTheoNgay(int ngay, int thang)
        {
            decimal tongtien = 0;
            //lấy ra những đơn hàng có ngày tương ứng
            var list = db.DonHangs.Where(n => n.Ngaydat.Value.Day == ngay && n.Ngaydat.Value.Month == thang && n.Trangthai == 2);
            //duyệt chi tiết của đơn hàng và lấy ra tổng tiền của đơn hàng đó
            foreach(var x in list)
            {
                tongtien += Convert.ToDecimal( x.CTDHs.Sum(a => a.Tongtien).Value);
            }
            return tongtien;
        }

        public decimal TongDoanhThuTheoThang(int thang)
        {
            decimal tongtien = 0;
            //lấy ra những đơn hàng có ngày tương ứng
            var list = db.DonHangs.Where(n => n.Ngaydat.Value.Month == thang && n.Trangthai == 2);
            //duyệt chi tiết của đơn hàng và lấy ra tổng tiền của đơn hàng đó
            foreach (var x in list)
            {
                //tongtien += Convert.ToDecimal(x.CTDHs.Sum(a => a.Soluong * a.Giakhuyenmai).Value);
                tongtien += Convert.ToDecimal(x.CTDHs.Sum(a => a.Tongtien).Value);

            }
            return tongtien;
        }
    }
}