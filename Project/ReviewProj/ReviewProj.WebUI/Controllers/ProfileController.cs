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

            return View(model);
        }

        [Authorize(Roles = "reviewer")]
        public string PhotoPath()
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);

            Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);

            if (resource != null)
            {
                return "~Content/UserResources/" + resource.DataPath;
            }

            return "~Context/AppResources/noImg.png";
        }
    }
}