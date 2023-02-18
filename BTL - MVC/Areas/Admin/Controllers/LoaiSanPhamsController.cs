using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL___MVC.Database;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class LoaiSanPhamsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/LoaiSanPhams
        public ActionResult getAll()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.LoaiSanPhams.ToList(), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult getByID (int id)
        //{
        //    LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
        //    return Json(loaiSanPham, JsonRequestBehavior.AllowGet);

        //}
        public JsonResult GetByID(int id)
        {
            var all = db.LoaiSanPhams.ToList();
            var kq = (from x in all
                      where x.Maloai_id == id
                      select new
                      {
                          Maloai_id = x.Maloai_id,
                          Tenloai = x.Tenloai,
                          
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/LoaiSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Create

        public ActionResult ThemLSP()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemLSP([Bind(Include = "maloai_id,tenloai")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.LoaiSanPhams.Add(loaiSanPham);
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new { msg = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SuaLSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaLSP([Bind(Include = "maloai_id,tenloai")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(loaiSanPham).State = EntityState.Modified;
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

        public ActionResult XoaLSP()
        {
            return View();
        }
        [HttpPost]
        public JsonResult XoaLSP(int id)
        {
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            db.LoaiSanPhams.Remove(loaiSanPham);
            db.SaveChanges();
            return Json(new { msg = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maloai_id,tenloai")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                
                 db.LoaiSanPhams.Add(loaiSanPham);
                 db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);

        }


        // GET: Admin/LoaiSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maloai_id,tenloai")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            db.LoaiSanPhams.Remove(loaiSanPham);
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
    }
}
