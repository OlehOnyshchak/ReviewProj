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
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);
            Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);

            ProfileViewModel model = new ProfileViewModel()
            {
                BirthDate = reviewer.BirthDate,
                Nationality = reviewer.Nationality,
                Rating = reviewer.Rating,
                HasPhoto = resource != null
            };

            return View(model);
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
            if (file != null)
            {
                Reviewer reviewer = repository.FindByEmail(User.Identity.Name);

                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/App_Data/UserResources"), fileName);
                    file.SaveAs(path);
                    repository.UpdateMainPhoto(reviewer, fileName);
                }
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

        [Authorize(Roles = "reviewer")]
        public ActionResult EditProfile()
        {
            Reviewer reviewer = repository.FindByEmail(User.Identity.Name);
            Resource resource = reviewer.Resources.FirstOrDefault(res => res.Type == ResourceType.MainImage);

            ProfileViewModel model = new ProfileViewModel()
            {
                BirthDate = reviewer.BirthDate,
                Nationality = reviewer.Nationality,
                Rating = reviewer.Rating,
                HasPhoto = reviewer.Resources.FirstOrDefault(
                    res => res.Type == ResourceType.MainImage) != null
            };

            return View(model);
        }
    }
}