using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Entities;

namespace ReviewProj.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new AppDBContext())
            {
                Resource res = new Resource();
                res.DataPath = "invalid";
                res.Format = ResourceFormat.JPEG;

                db.Resources.Add(res);
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}