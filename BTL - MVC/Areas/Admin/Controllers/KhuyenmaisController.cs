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
    public class KhuyenmaisController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/Khuyenmais
        public ActionResult Index()
        {
            return View(db.Khuyenmais.ToList());
        }

        // GET: Admin/Khuyenmais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khuyenmai khuyenmai = db.Khuyenmais.Find(id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmai);
        }

        // GET: Admin/Khuyenmais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Khuyenmais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Khuyenmai_id,Tieude,Mota,Anh")] Khuyenmai khuyenmai, HttpPostedFileBase file1)
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
                khuyenmai.Anh = _FileName;
                db.Khuyenmais.Add(khuyenmai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khuyenmai);
        }

        // GET: Admin/Khuyenmais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khuyenmai khuyenmai = db.Khuyenmais.Find(id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmai);
        }

        // POST: Admin/Khuyenmais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Khuyenmai_id,Tieude,Mota,Anh")] Khuyenmai khuyenmai, HttpPostedFileBase file1)
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
                khuyenmai.Anh = _FileName;
                db.Entry(khuyenmai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khuyenmai);
        }

        // GET: Admin/Khuyenmais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khuyenmai khuyenmai = db.Khuyenmais.Find(id);
            if (khuyenmai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenmai);
        }

        // POST: Admin/Khuyenmais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Khuyenmai khuyenmai = db.Khuyenmais.Find(id);
            db.Khuyenmais.Remove(khuyenmai);
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
