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
    public class HomeController : BaseController
    {
        private IEnterpriseRepository entRepository;
        public int PageSize = 10;
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
                   new SelectListItem {Text = string.Format(Resources.Resource.StarNumberKey, 5), Value = "5" },
                   new SelectListItem {Text = string.Format(Resources.Resource.StarNumberKey, 4), Value = "4" },
                   new SelectListItem {Text = string.Format(Resources.Resource.StarNumberKey, 3), Value = "3" },
                   new SelectListItem {Text = string.Format(Resources.Resource.StarNumberKey, 2), Value = "2" },
                   new SelectListItem {Text = string.Format(Resources.Resource.StarNumberKey, 1), Value = "1" },
            };
        }

        private IList<SelectListItem> GetTypeItems()
        {
            return new List<SelectListItem>
            {
                   new SelectListItem {Text = Resources.Resource.RestaurantTypeKey, Value = "Restaurant" },
                   new SelectListItem {Text = Resources.Resource.HotelTypeKey, Value = "Hotel" },
                   new SelectListItem {Text = Resources.Resource.FastFoodTypeKey, Value = "FastFood" },
                   new SelectListItem {Text = Resources.Resource.ClubTypeKey, Value = "Club" },
                   new SelectListItem {Text = Resources.Resource.CafeTypeKey, Value = "Cafe" },
            };
        }

        private IList<SelectListItem> GetSortingItems()
        {
            return new List<SelectListItem>
            {
                   new SelectListItem {Text = "Name", Value = "Name", Selected = true },
                   new SelectListItem {Text = "Rating", Value = "Rating" }
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

        public ViewResult FilterList(EnterpriseListViewModel model, int page = 1)
        {
            // hack againts bug "Filter info is lost when we switch pages"
            // don't know how to pass here actual model, so we will use previous one
            if (model == null)
            {
                var cachedModel = Session["FilterModel"];
                if (cachedModel == null)
                {
                    return View("Index", Index(null, page).Model);
                }
                else
                {
                    model = (EnterpriseListViewModel)cachedModel;
                }
            }

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
                if (model.SelectedSortingCategory == "Name")
                {
                    query = query.OrderBy(ent => ent.Name);
                }
                else
                {
                    query = query.OrderBy(ent => ent.Rating);
                }

                model.Enterprises = new List<Enterprise>(query);
            }

            EnterpriseListViewModel newModel = UpdateModel(model, page);
            model.PagingInfo = new PagingInfo(newModel.PagingInfo);
            Session["FilterModel"] = new EnterpriseListViewModel(model);

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
                RatingCategories = model.RatingCategories.Count != 0 ? 
                    model.RatingCategories : this.GetRatingItems(),
                TypeCategories = model.TypeCategories.Count != 0 ?
                    model.TypeCategories : this.GetTypeItems(),
                SortingCategories = model.SortingCategories.Count != 0 ?
                    model.SortingCategories : this.GetSortingItems()
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
                    "Content/UserResources/" + fileName;
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
        
        // Change language
        public ActionResult ChangeLangToUA()
        {
            // Change the current culture for this user.
            SiteSession.CurrentUICulture = 2; //set Ukrainian

            // Cache the new current culture into the user HTTP session. 
            Session["CurrentUICulture"] = 2;

            // Redirect to the same page from where the request was made! 
            return Redirect(Request.UrlReferrer.ToString());
        }


        public ActionResult ChangeLangToEN()
        {
            SiteSession.CurrentUICulture = 1; //Set English
            Session["CurrentUICulture"] = 1;
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}