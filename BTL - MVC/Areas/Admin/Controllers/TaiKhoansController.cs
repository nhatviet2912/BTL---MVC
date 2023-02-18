using BTL___MVC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL___MVC.Areas.Admin.Controllers
{
    public class TaiKhoansController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/TaiKhoans
        public ActionResult Index()
        {
            var tk = db.TaiKhoans.ToList();
            return View(tk);
        }


    }
}