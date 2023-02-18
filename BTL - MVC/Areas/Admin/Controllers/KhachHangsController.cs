using BTL___MVC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class KhachHangsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/KhachHangs
        public ActionResult Index()
        {
            var kh = db.KhachHangs.ToList();
            return View(kh);
        }
    }
}