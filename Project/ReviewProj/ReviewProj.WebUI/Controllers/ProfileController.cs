using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ReviewProj.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Domain.Abstract;
using ReviewProj.WebUI.Models;
using ReviewProj.Domain.Concrete;

namespace ReviewProj.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private IReviewerRepository repository;

        public ProfileController(IReviewerRepository reviewerRepository)
        {
            repository = reviewerRepository;
        }

        // GET: ProfileView
        [Authorize(Roles = "reviewer")]
        public ActionResult Index()
        {            

            return View(GetModel());
        }

        public ActionResult EditProfile()
        {

            return View(GetModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileViewModel Model)
        {
            Reviewer reviewer = new Reviewer();
            reviewer = repository.FindByEmail(User.Identity.Name);
            using (var db = new AppDbContext())
            {
                if (reviewer != null)
                {
                    //може вилетіти якщо ввести не коректні дані
                    db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().BirthDate = Model.BirthDate;
                    db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().Nationality = Model.Nationality;
                    db.SaveChanges();
                }
                else RedirectToAction("About", "Home");
            }

            return RedirectToAction("Index", "Profile");
        }

        [Authorize(Roles = "reviewer")]
        public string PhotoPath()
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);

            if (reviewer != null)
            {
                Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);

                if (resource != null)
                {
                    return "~Content/UserResources/" + resource.DataPath;
                }
            }

            return "~Context/AppResources/noImg.png";
        }

        public ProfileViewModel GetModel()
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);

            Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);

            string fileName = null;
            if (resource != null)
            {
                fileName = resource.DataPath;
            }

            ProfileViewModel model = new ProfileViewModel()
            {
                BirthDate = reviewer.BirthDate,
                IsBanned = reviewer.IsBanned,
                Nationality = reviewer.Nationality,
                Rating = reviewer.Rating,
                HasPhoto = reviewer.Resources.FirstOrDefault(
                    res => res.Type == ResourceType.MainImage) != null
            };
            return model;
        }
    }
}