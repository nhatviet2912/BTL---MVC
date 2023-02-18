using BTL___MVC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class DonHangsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/DonHangs
        public ActionResult Index()
        {
            //var dh = db.DonHangs.ToList();
            var a = db.DonHangs.Where(x => x.Trangthai == 1).ToList();
            return View(a);
        }
        //public ActionResult getData()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var dh = db.DonHangs.ToList();
        //    return Json(dh, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult CTDH(int id)
        {
            var ctdh = db.CTDHs.Where(x => x.Donhang_id == id).FirstOrDefault();
            return View(ctdh);
        }

        public ActionResult Chitiet()
        {
            var chitiet = db.CTDHs.ToList();
            return View(chitiet);
        }

        public ActionResult XacNhan(int id)
        {
            try
            {

                var dh = db.DonHangs.Find(id);
                ////lấy ra ctdh = id
                var ldh = db.CTDHs.ToList();
                var ctdh = ldh.FirstOrDefault(x => x.Donhang_id == dh.Donhang_id);

                //// lấy ra sản phẩm = id ctdh
                foreach (CTDH ct in ldh)
                {
                    SanPham sp = db.SanPhams.SingleOrDefault(x => x.Sanpham_id == ct.Sanpham_id);
                    var slsp = sp.Soluong - ctdh.Soluong;
                    sp.Soluong = slsp;

                }
                dh.Trangthai = 2;
                var cvdate = dh.Ngaydat.GetValueOrDefault();
                cvdate = DateTime.Now;
                dh.Ngaydat = cvdate;

                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var dh = db.DonHangs.Find(id);
            db.DonHangs.Remove(dh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DonHangDaBan()
        {
            var dh = db.DonHangs.Where(x => x.Trangthai == 2).ToList();
            return View(dh);
        }
    }
}