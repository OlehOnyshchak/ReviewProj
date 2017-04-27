using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Models;

namespace ReviewProj.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext context = ApplicationDbContext.Create();

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string str)
        {
            int id = 5;
            context.Enterprises.FirstOrDefault(e => e.EntId == id);
            List<Enterprise> enterprises = (from e in context.Enterprises
                                            where e.Name.Contains(str)
                                            select e).ToList<Enterprise>();

            return View("Home/Index", enterprises);                               
        }
    }
}