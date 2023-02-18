using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using BTL___MVC.Database;
using PagedList;
using Common;


namespace BTL___MVC.Controllers
{
    public class HomeController : Controller
    {
        BTL_MVCEntities db = new BTL_MVCEntities();
        public ActionResult Index(int? page = 1)
        {
            var ds = db.SanPhams.ToList();
            var pageSize = 20;
            var pageNumber = (page ?? 1);
            var sl = db.Sliders.ToList();
            ViewBag.sl = sl;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProductDetail(int id)
        {
            //tìm kiếm sản phẩm có id 
            var ma = db.SanPhams.Find(id);

            //FirstOrDefault trả về phần tử đầu tiên
            var ds = db.SanPhams.FirstOrDefault(s => s.Sanpham_id == id);

            //tìm kiếm sản phẩm có maloai == với sản phẩm có mã loại truyền vào
            var lsp = db.SanPhams.Where(s => s.Maloai_id == ma.Maloai_id).ToList();
            ViewBag.lsp = lsp;

            var loaisp = db.LoaiSanPhams.SingleOrDefault(s => s.Maloai_id == ma.Maloai_id);
            ViewBag.tenloaisp = loaisp.Tenloai;

            var ts = db.ThongSoKyThuats.Where(s => s.Sanpham_id == ma.Sanpham_id).ToList();
            ViewBag.ts = ts;

            //comment
            var cmt = db.BinhLuans.Where(s => s.Sanpham_id == ma.Sanpham_id).ToList();
            ViewBag.cmt = cmt;

            //moreImg
            var img = db.MoreImgs.Where(s => s.Sanpham_id == ma.Sanpham_id).ToList();
            ViewBag.img = img;
         
            return View(ds);
        }
        public ActionResult Menu()
        {
            var sp = db.LoaiSanPhams.ToList();
            return View(sp);
        }
        public ActionResult Category(int id, int? page = 1)
        {
            var pageSize = 20;
            var pageNumber = (page ?? 1);
            var loaisp = db.LoaiSanPhams.SingleOrDefault(s => s.Maloai_id == id);
            ViewBag.loaisp = loaisp.Tenloai;

            var sp = db.SanPhams.Where(s => s.Maloai_id == id).ToList();
            return View(sp.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult addCart(int id) 
        {
            // lấy về sản phẩm bằng id ng dùng chọn
            var spt = db.SanPhams.FirstOrDefault(s => s.Sanpham_id == id);
            List<CTDH> cart = null;

            //kiểm tra session khác rỗng (nếu session tồn tại)
            if (Session["cart"] != null)
            {
                //đưa dữ liệu có sẵn ở session vào cart
                cart = (List<CTDH>)Session["cart"];
                //lấy về sp(ở trong session) có id bằng id ng dùng chọn 
                var kt = cart.FirstOrDefault(s => s.Sanpham_id == id);
                //chưa có id sp trong cart
                if (kt == null)
                {
                    // khởi tạo đối tượng trong CTDH
                    CTDH sp = new CTDH();
                    sp.Sanpham_id = id;
                    sp.Soluong = 1;
                    sp.Gia = spt.Gia;
                    sp.Tensanpham = spt.Tensanpham;
                    sp.Anh = spt.Anh;
                    sp.Giakhuyenmai = spt.Giakhuyenmai;
                    //THÊM sp vào cart
                    cart.Add(sp);
                }
                // nếu có id sp trong session
                else
                {
                    // tăng sl thêm 1
                    kt.Soluong += 1;
                }
                // cập nhập lại session
                Session["cart"] = cart;
                

            }
            // nếu session  == null (session không tồn tại)
            else
            {

                //khởi tạo 
                cart = new List<CTDH>();
                CTDH sp = new CTDH();
                sp.Sanpham_id = id;
                sp.Soluong = 1;
                sp.Gia = spt.Gia;
                sp.Tensanpham = spt.Tensanpham;
                sp.Anh = spt.Anh;
                sp.Giakhuyenmai = spt.Giakhuyenmai;
                //THÊM sp vào cart
                cart.Add(sp);
                Session["cart"] = cart;

            }
            

            return RedirectToAction("Index");


        }

        public ActionResult FindText(string search, int? page = 1)
        {

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var sp = db.SanPhams.Include(s => s.Maloai_id);
            //kiếm tra tìm kiếm xem rỗng hay null
            if (!string.IsNullOrEmpty(search))
            {
                //where tìm kiếm theo tên
                sp = db.SanPhams.Where(x => x.Tensanpham.ToUpper().Contains(search.ToUpper())).OrderBy(x => x.Sanpham_id);
            }
            return View(sp.ToPagedList(pageNumber, pageSize));
        }
        
        

        public ActionResult Cart()
        {
           
            List<CTDH> ds = (List<CTDH>)Session["cart"];
            //List<CTDH> ds1 = new List<CTDH>();
            float tongtien = 0;
            //foreach (var d in ds)
            //{
            //    ds1.Add(d);

            //}
            if(ds != null)
            {
                foreach (CTDH x in ds)
                {
                    var a = x.Giakhuyenmai.GetValueOrDefault();
                    int c = Convert.ToInt32(a);
                    var b = x.Soluong.GetValueOrDefault();

                    tongtien += c * b;
                }

            }
            ViewBag.tongtien = tongtien;
            return View(ds);
        }
        public ActionResult Xoasp(int id)
        {
            List<CTDH> cart = (List<CTDH>)Session["cart"];
            var idsp = cart.Find(s => s.Sanpham_id == id);
            if(idsp != null)
            {
                cart.Remove(idsp);
            }
            Session["cart"]= cart;

            //return RedirectToAction("Cart");
            return Redirect(Request.UrlReferrer.ToString());


        }

        public ActionResult Themsl(int id)
        {
            List<CTDH> cart = (List<CTDH>)Session["cart"];
            var idsp = cart.Find(s => s.Sanpham_id == id);
            if(idsp != null)
            {
                idsp.Soluong++;

            }
            Session["cart"] = cart;
            //return RedirectToAction("Cart");
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Giamsl(int id)
        {
            List<CTDH> cart = (List<CTDH>)Session["cart"];
            
            var idsp = cart.Find(s => s.Sanpham_id == id);
            if(idsp != null)
            {
                if(idsp.Soluong < 2)
                {
                    idsp.Soluong = 1;
                }
                else
                {
                    idsp.Soluong--;
                }
            }
            Session["cart"] = cart;
            //return RedirectToAction("Cart");
            return Redirect(Request.UrlReferrer.ToString());

        }

        public ActionResult Xoagh()
        {
            Session["cart"] = null;

            return Redirect(Request.UrlReferrer.ToString());

        }

        
        [HttpGet]
        public ActionResult Checkout()
        {
            List<CTDH> cart = (List<CTDH>)Session["cart"];
            float tongtien = 0;
            foreach (CTDH x in cart)
            {
                var a = x.Giakhuyenmai.GetValueOrDefault();
                int c = Convert.ToInt32(a);
                var b = x.Soluong.GetValueOrDefault();

                tongtien += c * b;
            }
            ViewBag.tongtien = tongtien;
            return View(cart);
        }

        [HttpPost]
        public ActionResult Checkout(string username, string number, string email, string diachi)
        {
            DonHang dh = new DonHang();
            CTDH ctdh = new CTDH();
            int id = 1;
            int idct = 1;
            float tongtien = 0;

            //Lưu hóa đơn vào Đơn hàng
            try
            {
                var getAll = db.DonHangs.ToList();
                var getid = getAll.Select(s => new { s.Donhang_id }).OrderByDescending(s => s.Donhang_id).FirstOrDefault();
                id = getid.Donhang_id + 1;
            }
            catch (Exception)
            {

            }
            dh.Hoten = username;
            dh.Sdt = number;
            dh.Email = email;
            dh.Diachi = diachi;
            var cvdate = dh.Ngaydat.GetValueOrDefault();
            cvdate = DateTime.Now;
            dh.Ngaydat = cvdate;
            if (Session["taikhoan"] != null)
            {
                var tk = (TaiKhoan)Session["taikhoan"];
                dh.Khachhang_id = tk.Khachhang_id;
            }
            if (Session["cart"] != null)
            {
                List<CTDH> cart = (List<CTDH>)Session["cart"];
                foreach(CTDH x in cart)
                {
                    var a = x.Giakhuyenmai.GetValueOrDefault();
                    var c = Convert.ToInt32(a);
                    var b = x.Soluong.GetValueOrDefault();

                    tongtien += c * b;
                }

            }
            dh.Tongtien = tongtien;
            dh.Donhang_id = id;
            dh.Trangthai = 1;
            db.DonHangs.Add(dh);
            db.SaveChanges();

            //Lưu hóa đơn vào chi tiết đơn hàng

            if (Session["cart"] != null)
            {
                try
                {
                    var getAll = db.CTDHs.ToList();
                    var getid = getAll.Select(s => new { s.Ctdh_id }).OrderByDescending(s => s.Ctdh_id).FirstOrDefault();
                    idct = getid.Ctdh_id + 1;
                }
                catch (Exception)
                {

                }
                List<CTDH> cart = (List<CTDH>)Session["cart"];
                foreach(CTDH x in cart)
                {
                    ctdh.Tensanpham = x.Tensanpham;
                    ctdh.Anh = x.Anh;
                    ctdh.Soluong = x.Soluong;
                    ctdh.Gia = x.Gia;
                    ctdh.Giakhuyenmai = x.Giakhuyenmai;
                    ctdh.Sanpham_id = x.Sanpham_id;
                    ctdh.Donhang_id = dh.Donhang_id;
                    ctdh.Ctdh_id = idct;
                    var a = x.Giakhuyenmai.GetValueOrDefault();
                    var c = Convert.ToInt32(a);
                    var b = x.Soluong.GetValueOrDefault();

                    tongtien = c * b;
                    ctdh.Tongtien = tongtien;
                    db.CTDHs.Add(ctdh);
                    db.SaveChanges();
                    

                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Client/newoder.html"));
                content = content.Replace("{{Tensanpham}}", ctdh.Tensanpham);
                content = content.Replace("{{CustomerName}}", username);
                content = content.Replace("{{Phone}}", number);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", diachi);
                content = content.Replace("{{Total}}", dh.Tongtien.ToString());
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
                //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
                //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
                //của ứng dụng kém an toàn phải ở chế độ bật
                //  Đồng thời tài khoản Gmail cũng cần bật IMAP
                //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

                new MailHelper().SendMail(email, "Đơn hàng mới từ Siêu thị Điện Máy Xanh", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Siêu thị Điện Máy Xanh", content);
                
            }

            
            Session["cart"] = null;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string hoten, string sdt, string email, string diachi, string tieude, string noidung)
        {
            LienHe lh = new LienHe();
            if (ModelState.IsValid)
            {
                lh.Hoten = hoten;
                lh.Sdt = sdt;
                lh.Email = email;
                lh.Diachi = diachi;
                lh.Tieude = tieude;
                lh.Noidung = noidung;
                db.LienHes.Add(lh);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Sale(int? page = 1)
        {
            var sale = db.Khuyenmais.ToList();
            var pageSize = 8;
            var pageNumber = (page ?? 1);
       
            return View(sale.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Comment(string noidung, int id)
        {
            BinhLuan bl = new BinhLuan();
            KhachHang kh = new KhachHang();
            SanPham sp = new SanPham();
            int idbl = 1;
            try
            {
                var getAllBL = db.BinhLuans.ToList();
                var getBLid = getAllBL.Select(s => new { s.Binhluan_id }).OrderByDescending(s => s.Binhluan_id).FirstOrDefault();
                idbl = getBLid.Binhluan_id + 1;
            }
            catch (Exception)
            {

            }

            if (Session["taikhoan"] != null)
            {
                var tk = (TaiKhoan)Session["taikhoan"];
                bl.Khachhang_id = tk.Khachhang_id;
                var tentk = tk.KhachHang.Tenkhachhang;
                bl.Hoten = tentk;
                bl.Anh = tk.KhachHang.Anhdaidien;
            }
            bl.Noidung = noidung;
            bl.Binhluan_id = idbl;
            bl.Sanpham_id = id;
            db.BinhLuans.Add(bl);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}