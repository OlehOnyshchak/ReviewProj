using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

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


        // Перевіряємо ролі користувача
        public ActionResult Roles()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View((object)(roles == null ? "" : roles.First()));
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