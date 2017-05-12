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
        private IEnterpriseRepository entRepository;
        private IReviewRepository reviewRepository;
        public InstitutionDetailsController(IEnterpriseRepository enterpriseRepository, 
            IReviewRepository revRepo)
        {
            entRepository = enterpriseRepository;
            reviewRepository = revRepo;
        }

        // GET: InstitutionDetails
        public ActionResult Index(int id)
        {
            Enterprise ent = entRepository.Enterprises.FirstOrDefault(e => e.EntId == id);

            ViewBag.UserRole = Role.Guest;
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            // it shouldn't be here. It should be moved into some helper method, e.g. in UsersRepository clas
            if (user != null)
            {
                ViewBag.User = user;
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

            entRepository.ChangeRating(ent, this.RecalculateEnterpriceRating(ent));

            return View(ent);
        }

        private double RecalculateEnterpriceRating(Enterprise ent)
        {
            double denumerator = 0.0;
            double numerator = 0 ;

            foreach (var rev in ent.Reviews)
            {
                double mark = rev.Mark;
                double weight = Math.Pow(2, rev.TotalLikes) * Math.Pow(0.5, rev.TotalDislikes);
                numerator += mark * weight;
                denumerator += weight;
            }

            return  numerator / denumerator;
        }

        public ActionResult AddReview(int entId, string reviewText, string rating)
        {
            int mark = Convert.ToInt32(rating);
            entRepository.AddReview(entId, User.Identity.Name, new Review
            {
                Description = reviewText,
                Mark = mark
            });

            return RedirectToAction("Index", new { id = entId });
        }

        [Authorize (Roles = "admin")]
        public ActionResult DeleteReview(int reviewId, int entId)
        {
            reviewRepository.DeleteById(reviewId);
            return RedirectToAction("Index", new { id = entId });
        }
    }
}