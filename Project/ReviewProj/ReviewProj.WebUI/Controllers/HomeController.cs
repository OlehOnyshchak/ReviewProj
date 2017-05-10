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
using ReviewProj.WebUI.Models;

namespace ReviewProj.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IEnterpriseRepository repository;
        public int PageSize = 20;
        public HomeController(IEnterpriseRepository enterpriceRepository)
        {
            repository = enterpriceRepository;
        }
        public ViewResult Index(string search, int page = 1)
        {
            IQueryable<Enterprise> enterprises = repository.Enterprises;
            if (!String.IsNullOrEmpty(search))
            {
                enterprises = enterprises.Where(ent => ent.Name.Contains(search));
            }

            EnterpriseListViewModel model = new EnterpriseListViewModel
            {
                Enterprises = enterprises
                .OrderBy(e => -e.Rating)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = enterprises.Count() > PageSize * (page - 1) ? page : 1,
                    ItemsPerPage = PageSize,
                    TotalItems = enterprises.Count()
                },
                SearchString = search
            };

            return View(model);
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