using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Entities;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ReviewProj.WebUI.Models;

namespace ReviewProj.WebUI.Controllers
{
    public class InstitutionDetailsController : Controller
    {
        private IEnterpriseRepository repository;
        private IReviewerRepository reviewers;

        public InstitutionDetailsController(IEnterpriseRepository enterpriseRepository)
        {
            repository = enterpriseRepository;
        }

        // GET: InstitutionDetails
        public ActionResult Index(int id)
        {
            Enterprise ent = repository.Enterprises.FirstOrDefault(e => e.EntId == id);

            ViewBag.UserRole = Role.Guest;
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            // it shouldn't be here. It should be moved into some helper method, e.g. in UsersRepository clas
            if (user != null)
            {
                switch (userManager.GetRoles(user.Id).FirstOrDefault())
                {
                    case "reviewer":
                        ViewBag.UserRole = Role.Reviewer;
                        break;
                    case "owner":
                        ViewBag.UserRole = Role.Owner;
                        break;
                    case "admin":
                        ViewBag.UserRole = Role.Admin;
                        break;
                    default:
                        break;
                }
            }

            return View(ent);
        }

        public ActionResult AddReview(int entId, string reviewText, string rating)
        {
            int mark = Convert.ToInt32(rating);
            repository.AddReview(entId, User.Identity.Name, new Review
            {
                Description = reviewText,
                Mark = mark
            });

            return RedirectToAction("Index", new { id = entId });
        }
    }
}