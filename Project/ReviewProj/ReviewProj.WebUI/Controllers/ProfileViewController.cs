using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewProj.WebUI.Controllers
{
    public class ProfileViewController : Controller
    {
        // GET: ProfileView
        public ActionResult Index()
        {
            return View();
        }
    }
}