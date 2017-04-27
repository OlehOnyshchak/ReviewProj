using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Models;

namespace ReviewProj.Controllers
{
    public class InstitutionDetailsController : Controller
    {
        ApplicationDbContext context = ApplicationDbContext.Create();
        // GET: InstitutionDetails
        public ActionResult Index(int id)
        {
            Enterprise ent = context.Enterprises.FirstOrDefault(e => e.EntId == id);
            return View(ent);
        }
    }
}