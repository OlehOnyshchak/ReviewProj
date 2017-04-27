using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Models;
using System.IO;
using System.Drawing;

namespace ReviewProj.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context = ApplicationDbContext.Create();

        public ActionResult Index()
        {
            //ApplicationUser user = new ApplicationUser { UserName = "check@gmal.com", Email = "check@gmal.com",
            //EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false };
            //using (var db = new ApplicationDbContext())
            //{
            //    var query = from b in db.Reviewers
            //                select b;
            //    var q = query.ToArray();

            //    Reviewer rv = new Reviewer(user);
            //    rv.IsBanned = true;
            //    rv.AvatarFormat = "png";
            //    rv.Nationality = "Ukrainian";
            //    db.Reviewers.Add(rv);
            //    db.SaveChanges();
            //}



            //byte[] array = null;
            //using (var ms = new MemoryStream())
            //{
            //    Image.FromFile(@"D:\development\ReviewProj\Project\ReviewProj\Content\images\resource_7359.png")
            //        .Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //    array = ms.ToArray();
            //}

            //context.Resources.First().Data = array;
            //context.SaveChanges();


            // Не знаю для чого цей рядок, але з ним все працює.
            var r = context.Resources.ToList<Resource>();

            return View(context.Enterprises.ToList<Enterprise>());
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