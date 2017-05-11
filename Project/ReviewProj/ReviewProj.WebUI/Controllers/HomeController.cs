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
using System.IO;

namespace ReviewProj.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IEnterpriseRepository entRepository;
        public int PageSize = 20;
        public HomeController(IEnterpriseRepository enterpriceRepository)
        {
            entRepository = enterpriceRepository;
        }

        public ViewResult Index(EnterpriseListViewModel model, int page = 1)
        {
            if (model == null) model = new EnterpriseListViewModel();

            this.SaveRole();

            if (string.IsNullOrEmpty(model.SearchString))
                model.Enterprises = entRepository.Enterprises;

            EnterpriseListViewModel newModel = UpdateModel(model, page);

            return View(newModel);
        }


        private IList<SelectListItem> GetRatingItems()
        {
            return new List<SelectListItem>
            {
                   new SelectListItem {Text = "5 stars", Value = "5" },
                   new SelectListItem {Text = "4 stars", Value = "4" },
                   new SelectListItem {Text = "3 stars", Value = "3" },
                   new SelectListItem {Text = "2 stars", Value = "2" },
                   new SelectListItem {Text = "1 stars", Value = "1" },
            };
        }

        private IList<SelectListItem> GetTypeItems()
        {
            return new List<SelectListItem>
            {
                   new SelectListItem {Text = "Restaurant", Value = "Restaurant" },
                   new SelectListItem {Text = "Hotel", Value = "Hotel" },
                   new SelectListItem {Text = "FastFood", Value = "FastFood" },
                   new SelectListItem {Text = "Club", Value = "Club" },
                   new SelectListItem {Text = "Cafe", Value = "Cafe" },
            };
        }

        private EnterpriceType GetEntType(string entType)
        {
            switch (entType)
            {
                case "Restaurant":
                    return EnterpriceType.Restaurant;
                case "Hotel":
                    return EnterpriceType.Hotel;
                case "FastFood":
                    return EnterpriceType.FastFood;
                case "Club":
                    return EnterpriceType.Club;
                case "Cafe":
                    return EnterpriceType.Cafe;
                default:
                    throw new Exception("Shouldn't be here");
            }
        }

        public ViewResult FilterList(EnterpriseListViewModel model)
        {
            if (ModelState.IsValid)
            {
                IList<EnterpriceType> types = new List<EnterpriceType>();
                foreach(var checkBox in model.TypeCategories)
                {
                    if (checkBox.Selected)
                    {
                        types.Add(this.GetEntType(checkBox.Value));
                    }
                }

                IList<int> ratings = new List<int>();
                foreach (var checkBox in model.RatingCategories)
                {
                    if (checkBox.Selected)
                    {
                        ratings.Add(int.Parse(checkBox.Value));
                    }
                }

                string name = model.SearchString;
                IEnumerable<Enterprise> query = ratings.Count == 0 && types.Count == 0 ?
                    entRepository.GetByName(name) : entRepository.GetFiltratedByName(ratings, types, name);

                model.Enterprises = new List<Enterprise>(query);
            }

            EnterpriseListViewModel newModel = UpdateModel(model, 1);

            return View("Index", newModel);
        }

        private IQueryable<Enterprise> SelectEnterprisesForPage(IQueryable<Enterprise> enterprises, int page)
        {
            return enterprises
                .OrderBy(e => -e.Rating)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
        }

        private EnterpriseListViewModel UpdateModel(EnterpriseListViewModel model, int page)
        {
            return new EnterpriseListViewModel
            {
                Enterprises = this.SelectEnterprisesForPage(model.Enterprises.AsQueryable(), page),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = model.Enterprises.Count(),
                    ItemsPerPage = this.PageSize,
                    TotalItems = model.Enterprises.Count()
                },
                SearchString = model.SearchString,
                RatingCategories = this.GetRatingItems(),
                TypeCategories = this.GetTypeItems()
            };
        }

        // Перевіряємо ролі користувача
        public ActionResult Roles()
        {
            IList<string> roles = new List<string> { "Role is undefined" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);

            return View((object)(roles == null ? "" : roles.First()));
        }

        private void SaveRole()
        {
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

        [HttpGet]
        public FileContentResult GetImage(string fileName)
        {
            string filePath;
            if (fileName != null && fileName != "")
            {
                filePath = HttpContext.Server.MapPath("~") +
                    "App_Data/UserResources/" + fileName;
            }
            else
            {
                filePath = HttpContext.Server.MapPath("~") + "Content/AppResources/no_image_available.png";
            }

            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(filePath);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);

            string contentType = "image/" + filePath.Substring(filePath.LastIndexOf('.') + 1);

            return File(imageData, contentType);
        }
    }
}