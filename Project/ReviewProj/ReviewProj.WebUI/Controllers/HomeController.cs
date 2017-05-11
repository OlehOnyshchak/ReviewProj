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
        private IEnterpriseRepository repository;
        public int PageSize = 20;
        public HomeController(IEnterpriseRepository enterpriceRepository)
        {
            repository = enterpriceRepository;
        }
        public ViewResult Index(string search, int page = 1)
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

            // also should have specific method for this
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
                    CurrentPage = enterprises.Count(),
                    ItemsPerPage = PageSize,
                    TotalItems = enterprises.Count()
                },
                SearchString = search,
                AvailableCategories = new List<string> {
                    "5 stars", "4 stars", "3 stars", "2 stars", "1 star"
                }
            };

            return View(model);
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