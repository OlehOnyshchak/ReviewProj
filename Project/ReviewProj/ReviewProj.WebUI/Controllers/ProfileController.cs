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
using System.IO;

namespace ReviewProj.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private IReviewerRepository repository;

        public ProfileController(IReviewerRepository reviewerRepository)
        {
            repository = reviewerRepository;
        }

        [HttpGet]
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

            ProfileViewModel model = new ProfileViewModel()
            {
                BirthDate = reviewer.BirthDate,
                Nationality = reviewer.Nationality,
                Rating = reviewer.Rating,
                HasPhoto = resource != null
            };

            return model;
        }

        [HttpPost]
        [Authorize(Roles = "reviewer")]
        public ActionResult Index(string newNationality, DateTime newBirthDate)
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);
            // Save
            Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);

            ProfileViewModel model = new ProfileViewModel()
            {
                BirthDate = newBirthDate,
                Nationality = newNationality,
                Rating = reviewer.Rating,
                HasPhoto = resource != null
            };

            return View(model);

        }

        [HttpGet]
        [Authorize (Roles = "reviewer")]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "reviewer")]
        public ActionResult AddPhoto(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                Reviewer reviewer = repository.FindByEmail(User.Identity.Name);

                Resource res = new Resource(file, ResourceType.MainImage, Server.MapPath("~/App_Data/UserResources"));
               
                repository.UpdateMainPhoto(reviewer, res);
                
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "reviewer")]
        public ActionResult DeletePhoto()
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);
            repository.RemoveMainPhoto(reviewer);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "reviewer")]
        public FileContentResult Photo()
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);

            Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);
            string fileName;
            if (resource != null)
            {
                fileName = HttpContext.Server.MapPath("~") + 
                    "App_Data/UserResources/" + resource.DataPath;
            }
            else
            {
                fileName = HttpContext.Server.MapPath("~") + "Content/AppResources/noImg.png";
            }
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(fileName);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);

            string contentType = "image/" + fileName.Substring(fileName.LastIndexOf('.') + 1);

            return File(imageData, contentType);
        }
    }
}