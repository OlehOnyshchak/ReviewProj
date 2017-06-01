using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewProj.WebUI.Controllers
{
    public class BanController : Controller
    {
        // GET: Ban
        public ActionResult BanMessage()
        {
            return View();
        }
    }
}