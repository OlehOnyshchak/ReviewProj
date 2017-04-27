using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Models;

namespace ReviewProj.Controllers
{
    public class HomeController : Controller
    {
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

            return View(ApplicationDbContext.Create().Enterprises.ToList<Enterprise>());
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