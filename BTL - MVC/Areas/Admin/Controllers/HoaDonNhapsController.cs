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
    public class HoaDonNhapsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/HoaDonNhaps
        public ActionResult Index()
        {
            var hoaDonNhaps = db.HoaDonNhaps.Include(h => h.NCC);
            return View(hoaDonNhaps.ToList());
        }
        public ActionResult CTHDN(int id)
        {
            CTHDN cTHDN = db.CTHDNs.Find(id);
            return View(cTHDN);
        }
        // GET: Admin/HoaDonNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Create
        public ActionResult Create()
        {
            ViewBag.Ncc_id = new SelectList(db.NCCs, "Ncc_id", "Tenncc");
            return View();
        }

        // POST: Admin/HoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hoadonnhap_id,Thanhtien,Ngaynhap,Ncc_id")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonNhaps.Add(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ncc_id = new SelectList(db.NCCs, "Ncc_id", "Tenncc", hoaDonNhap.Ncc_id);
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ncc_id = new SelectList(db.NCCs, "Ncc_id", "Tenncc", hoaDonNhap.Ncc_id);
            return View(hoaDonNhap);
        }

        // POST: Admin/HoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hoadonnhap_id,Thanhtien,Ngaynhap,Ncc_id")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ncc_id = new SelectList(db.NCCs, "Ncc_id", "Tenncc", hoaDonNhap.Ncc_id);
            return View(hoaDonNhap);
        }

        // GET: Admin/HoaDonNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonNhap);
        }

        // POST: Admin/HoaDonNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            db.HoaDonNhaps.Remove(hoaDonNhap);
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
