using BTL___MVC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BTL___MVC.Controllers
{
    public class LoginController : Controller
    {
        BTL_MVCEntities db = new BTL_MVCEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tentk, string matkhau)
        {
            var ds = db.TaiKhoans.ToList();
            
            var md = matkhau;
            foreach(TaiKhoan tk in ds)
            {
                
                //var mk = tk.GetMD5()
                if (tentk == tk.Tentk.Trim() && md == tk.Matkhau.Trim() && tk.usergroup_id == 2)
                {
                    Session["taikhoan"] = tk;
                    return RedirectToAction("Index", "Home");
                }
                else if(tentk == tk.Tentk.Trim() && md == tk.Matkhau.Trim() && tk.usergroup_id == 1)
                {
                    Session["admin"] = tk;
                    return Redirect("/Admin/TrangChu/TrangChu");

                }
            }
            ViewBag.loi = "Tên đăng nhập hoặc tài khoản không đúng";
            return View();
        }
        [HttpGet]
        public ActionResult Dangki()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangki(string hoten, string matkhau, string email, string sdt, string tentk, string diachi, string nhaplaipass)
        {
            TaiKhoan tk = new TaiKhoan();
            KhachHang kh = new KhachHang();
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.FirstOrDefault(s => s.Tentk == tentk.ToLower());
                var em = db.KhachHangs.FirstOrDefault(s => s.Email == email.ToLower());
                if(string.IsNullOrEmpty(tentk) == true || string.IsNullOrEmpty(matkhau) == true || string.IsNullOrEmpty(hoten) == true)
                {
                    ViewBag.loi = "Vui lòng điền đầy đủ thông tin";
                    return View();
                }
                else if(user != null)
                {
                    ViewBag.loi = "Tên tài khoản đã tồn tại";
                    return View();
                }
                else if(em != null)
                {
                    ViewBag.loi = "Email đã tồn tại";
                    return View();
                }
                else if(nhaplaipass != matkhau)
                {
                    ViewBag.loi = "Xác nhận mật khẩu không đúng";
                    return View();
                }
                //else if(email == kh.Email)
                //{
                //    tk.Khachhang_id = kh.Khachhang_id;
                //}
                {
                    tk.Tentk = tentk.ToLower();
                    tk.Matkhau = matkhau;
                    tk.usergroup_id = 2;
                    kh.Tenkhachhang = hoten;
                    kh.Sdt = sdt;
                    kh.Diachi = diachi;
                    kh.Email = email;
                    tk.Khachhang_id = kh.Khachhang_id;
                    db.TaiKhoans.Add(tk);
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("DangNhap", "Login");

        }
        public ActionResult DangXuat()
        {
            Session["cart"] = null;    
            Session.Remove("taikhoan");
            Session.Remove("admin");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult QuenPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuenPass(string tentk, string matkhaul1, string matkhaul2)
        {
            TaiKhoan tk = new TaiKhoan();
            KhachHang kh = new KhachHang();
            if (ModelState.IsValid)
            {
                //lấy ra kh có tk == với tk nhập
                var getkh = db.KhachHangs.FirstOrDefault(s => s.Sdt == tentk || s.Email == tentk);
                //lấy ra tk = vs kh trên
                var gettk = db.TaiKhoans.FirstOrDefault(s => s.Khachhang_id == getkh.Khachhang_id);
                //var gettk = db.TaiKhoans.Find(getkh);
                // gán pass = matkhaul1
                if(string.IsNullOrEmpty(tentk) == true || string.IsNullOrEmpty(matkhaul1) == true || string.IsNullOrEmpty(matkhaul2) == true)
                {
                    ViewBag.loi = "Vui lòng điền đầy đủ thông tin";
                    return View();
                }
                else if (getkh == null)
                {
                    ViewBag.loi = "Không tìm thấy Email hoặc số điện thoại";
                    return View();
                }
                else if(matkhaul1 != matkhaul2)
                {
                    ViewBag.loi = "Xác nhận mật khẩu không đúng";
                    return View();
                }
                else
                {
                    gettk.Matkhau = matkhaul1;
                    db.SaveChanges();
                }
                
            }
            return RedirectToAction("DangNhap", "Login");

        }
        //create a string MD5
        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");

        //    }
        //    return byte2String;
        //}

    }
}