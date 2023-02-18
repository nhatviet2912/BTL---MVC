using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL___MVC.Database;
using static System.Net.WebRequestMethods;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            //var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham);
            return View();
        }
        public ActionResult getData()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var sanpham = db.SanPhams.ToList();
            return Json(sanpham, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult getByID(int id)
        //{
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    return Json(sanPham, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetByID(int id)
        {
            var all = db.SanPhams.ToList();
            var kq = (from x in all
                      where x.Sanpham_id == id
                      select new
                      {
                          Sanpham_id = x.Sanpham_id,
                          Maloai_id = x.Maloai_id,
                          Tensanpham = x.Tensanpham,
                          Anh = x.Anh,
                          Soluong = x.Soluong,
                          Gia = x.Gia,
                          Mota = x.Mota,
                          Kichco = x.Kichco,
                          Dophangiai = x.Dophangiai,
                          Giakhuyenmai = x.Giakhuyenmai,
                          Viewcount = x.Viewcount,
                          ReducePrice = x.ReducePrice,
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ThemSP()
        {
            ViewBag.Maloai_id = new SelectList(db.LoaiSanPhams, "Maloai_id", "Tenloai");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSP([Bind(Include = "sanpham_id,maloai_id,tensanpham,anh,soluong,gia,mota,kichco,dophangiai,giakhuyenmai,Viewcount,ReducePrice")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);

                }
            }
            ViewBag.Maloai_id = new SelectList(db.LoaiSanPhams, "Maloai_id", "Tenloai", sanPham.Maloai_id);

            return Json(new { msg = true }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SuaSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSP([Bind(Include = "sanpham_id,maloai_id,tensanpham,anh,soluong,gia,mota,kichco,dophangiai,giakhuyenmai,Viewcount,ReducePrice")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);

                }

            }
            return Json(new { msg = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult XoaSP(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.maloai_id = new SelectList(db.LoaiSanPhams, "maloai_id", "tenloai");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "sanpham_id,maloai_id,tensanpham,anh,soluong,gia,mota,kichco,dophangiai,giakhuyenmai,Viewcount,ReducePrice")] SanPham sanPham,HttpPostedFileBase file1)
        {
            string _FileName = "";
            string _path = "";
            try
            {
                if (file1.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file1.FileName);
                    _path = Path.Combine(Server.MapPath("~/Image"), _FileName);
                    file1.SaveAs(_path);
                }
            }
            catch
            {

            }
            if (ModelState.IsValid)
            {
                sanPham.Anh = _FileName;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maloai_id = new SelectList(db.LoaiSanPhams, "maloai_id", "tenloai", sanPham.Maloai_id);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.maloai_id = new SelectList(db.LoaiSanPhams, "maloai_id", "tenloai", sanPham.Maloai_id);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "sanpham_id,maloai_id,tensanpham,anh,soluong,gia,mota,kichco,dophangiai,giakhuyenmai,Viewcount,ReducePrice")] SanPham sanPham, HttpPostedFileBase file1)
        {
            string _FileName = "";
            string _path = "";
            try
            {
                if (file1.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file1.FileName);
                    _path = Path.Combine(Server.MapPath("~/Image"), _FileName);
                    file1.SaveAs(_path);
                }
            }
            catch
            {

            }


            if (ModelState.IsValid)
            {
                sanPham.Anh = _FileName;
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maloai_id = new SelectList(db.LoaiSanPhams, "maloai_id", "tenloai", sanPham.Maloai_id);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ContentResult Upload()
        {
            BTL_MVCEntities db = new BTL_MVCEntities();
            try
            {
                string path = Server.MapPath("/Img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (string key in Request.Files)
                {
                    HttpPostedFileBase postFile = Request.Files[key];
                    postFile.SaveAs(path + postFile.FileName);
                    SanPham sp = new SanPham();
                    sp.Anh = postFile.FileName;
                    db.SanPhams.Add(sp);
                    db.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return Content("abc");
        }
    }
}
