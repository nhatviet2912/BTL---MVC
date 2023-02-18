using BTL___MVC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class ContactsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/Contacts
        public ActionResult Index()
        {
            var lh = db.LienHes.ToList();
            return View(lh);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienhe = db.LienHes.Find(id);
            if (lienhe == null)
            {
                return HttpNotFound();
            }
            return View(lienhe);
        }

        // POST: Admin/Khuyenmais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LienHe lienhe = db.LienHes.Find(id);
            db.LienHes.Remove(lienhe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}