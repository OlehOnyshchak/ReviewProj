using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewProj.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IEnterpriseRepository repository;

        public HomeController(IEnterpriseRepository enterpriceRepository)
        {
            repository = enterpriceRepository;
        }
        public ActionResult Index()
        {
            return View(repository.Enterprises);
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