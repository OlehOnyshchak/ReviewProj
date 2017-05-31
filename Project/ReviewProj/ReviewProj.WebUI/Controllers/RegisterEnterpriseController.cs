using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;
using ReviewProj.Domain.Entities;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Abstract;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;

namespace ReviewProj.WebUI.Controllers
{
    [Authorize(Roles = "owner")]
    public class RegisterEnterpriseController : Controller
    {
        private IEnterpriseRepository repository;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private static List<Image> Images = new List<Image>();
        private static int index = 0;

        public static EnterpriseView enterpiceView = new EnterpriseView();

        public RegisterEnterpriseController(IEnterpriseRepository enterpriseRepository)
        {
            repository = enterpriseRepository;
        }

        public RegisterEnterpriseController()
        {

        }

        public RegisterEnterpriseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: RegisterEnterprise
        public ActionResult Index()
        {
            return RedirectToAction("Register", "RegisterEnterprise");
        }
        public ActionResult Register()
        {
            var model = new EnterpriseView();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(EnterpriseView model)
        {
            enterpiceView.Name = model.Name;
            enterpiceView.Address = model.Address;
            enterpiceView.Type = model.Type;
            return RedirectToAction("AddContacts", "RegisterEnterprise");
        }

        public ActionResult RegistPart2()
        {
            //enter.Contacts.Add("0965458741");
            return View(enterpiceView.Contacts);
        }

        public ActionResult AddContacts(string ell)
        {
            var Model = new AddContacts();
            if (ell != null)
            {
                Model.contact = ell;
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult AddContacts(AddContacts model)
        {
            enterpiceView.Contacts.Add(model.contact);
            enterpiceView.Contacts.IndexOf("");
            return RedirectToAction("RegistPart2", "RegisterEnterprise");
        }

        public ActionResult Edit(string itemName)
        {
            enterpiceView.Contacts.Remove(itemName);
            return RedirectToAction("AddContacts", "RegisterEnterprise", itemName);
        }
        public ActionResult Delete(string itemName)
        {
            enterpiceView.Contacts.Remove(itemName);
            return RedirectToAction("RegistPart2", "RegisterEnterprise");
        }

        public ActionResult RegistPart3()
        {
            //enter.Contacts.Add("0965458741");
            // return View(enter.Resources);
            return View(Images);
        }

        public ActionResult AddPhoto()
        {
            AddPhotoModelEnterprise photo = new AddPhotoModelEnterprise();
            return View(photo);

        }

        public ActionResult DeletePhoto(int ID)
        {
            Image im = new Image();
            im = Images.Where(x => x.imageId == ID).FirstOrDefault();
            Images.Remove(im);
            return RedirectToAction("RegistPart3", "RegisterEnterprise");
        }
        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddPhoto(AddPhotoModel model, HttpPostedFileBase photo1)
        //{
        //    Resource res = new Resource();
        //    if (photo1 != null)
        //    {
        //        model.Photo = new byte[photo1.ContentLength];
        //        photo1.InputStream.Read(model.Photo, 0, photo1.ContentLength);
        //        // Now we store in the database path to the resource
        //        //res.Data = model.Photo;
        //        res.ResourceId = index;
        //        index++;
        //        enter.Resources.Add(res);
        //    }
        /*
                    [HttpPost]
                    [Authorize]
                    [ValidateAntiForgeryToken]
                    public ActionResult AddPhoto(AddPhotoModelEnterprise model, HttpPostedFileBase photo1)
                    {
                        Resource res = new Resource();
                        if (photo1 != null)
                        {
                            model.Photo = new byte[photo1.ContentLength];
                            photo1.InputStream.Read(model.Photo, 0, photo1.ContentLength);
                            // Now we store in the database path to the resource
                            //res.Data = model.Photo;
                            res.ResourceId = index;
                            index++;
                            enter.Resources.Add(res);
                        }

                    //db.Rewievers.Where(x => x.Id == reviewer.Id).FirstOrDefault().Name = "s";


                    return RedirectToAction("RegistPart3", "RegisterEnterprise");
                }
                */

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(AddPhotoModelEnterprise model, HttpPostedFileBase photo1)
        {
            Resource res = new Resource();

            if (photo1 != null)
            {
                model.Photo = new byte[photo1.ContentLength];
                photo1.InputStream.Read(model.Photo, 0, photo1.ContentLength);

                Image im = new Image
                {
                    file = photo1,
                    Photo = model.Photo,
                    imageId = index
                };
                index++;
                Images.Add(im);

            }
            return RedirectToAction("RegistPart3", "RegisterEnterprise");
        }



        public ActionResult RegistPart4()
        {
            return View(enterpiceView);
        }

        [HttpPost]
        public ActionResult RegistPart4(EnterpriseView model)
        {
            enterpiceView.Description = model.Description;
            return RedirectToAction("SaveDataset", "RegisterEnterprise");
        }

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddPhoto(AddPhotoModel model, HttpPostedFileBase photo1)
        //{
        //    Resource res = new Resource();
        //    if (photo1 != null)
        //    {
        //        model.Photo = new byte[photo1.ContentLength];
        //        photo1.InputStream.Read(model.Photo, 0, photo1.ContentLength);
        //        // Now we store in the database path to the resource
        //        //res.Data = model.Photo;
        //        res.ResourceId = index;
        //        index++;
        //        enter.Resources.Add(res);
        //    }

        //    //db.Rewievers.Where(x => x.Id == reviewer.Id).FirstOrDefault().Name = "s";


        //    return RedirectToAction("RegistPart3", "RegisterEnterprice");
        //}

        // changes in database
        /*
        public FileContentResult GetChoosedPhotos(int ID)
        {
            foreach (Resource res in enter.Resources)
            {
                if (res.ResourceId == ID)
                {
                    return new FileContentResult(res.Data, "image/jpeg");
                }

            }
            return new FileContentResult(enter.Resources[0].Data, "image/jpeg");

        }
        */
        public FileContentResult GetChoosedPhotos(int ID)
        {
            foreach (Image res in Images)
            {
                if (res.imageId == ID)
                {
                    return new FileContentResult(res.Photo, "image/jpeg");
                }
            }

            return new FileContentResult(Images[0].Photo, "image/jpeg");
        }

        public ActionResult SaveDataset()
        {
            // it shouldn't be done explicitly
            //зберігання в бд без ресурсів
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);

            Enterprise enterprise = new Enterprise
            {
                Name = enterpiceView.Name,
                // changes in database
                //Type = enter.Type,
                // Contacts = enter.Contacts,
                Address = enterpiceView.Address,
                // Resources = enter.Resources,
                Description = enterpiceView.Description,
            };

            enterprise.Resources = new List<Resource>();
     //       enterprise.Contacts = enterpiceView.Contacts;
            // це треба винести у EnterpriceRepository
            using (var db = new AppDbContext())
            {
                Owner owner = db.Owners.Where(x => x.Id == user.Id).FirstOrDefault();
                enterprise.Owner = owner;
                db.Enterprises.Add(enterprise);

                db.SaveChanges();
            }

            // я думаю кращий варіант би був все в той ентерпрайс запхати, а тоді
            // його зберігати
            Enterprise fent = repository.GetEnterpriseById(enterprise.EntId);
            // INTEGRATION
            //repository.AddListContacts(fent);
            //foreach (var contact in enterprise.Contacts)
            //{
            //    repository.AddContact(fent, contact.EmailOrPhone);
            //}

            foreach (Image im in Images)
            {
                SavePhoto(im.file, fent);
            }

            enterpiceView = new EnterpriseView();
            Images = new List<Image>();
            index = 0;
            // return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Owner1");

        }

        private EnterpriseView GetModel()
        {
            Enterprise ent = new Enterprise();
            //changes in DB
            //ent = GetEnterprise();
            var model = new EnterpriseView
            {
                Address = ent.Address,
                //  Contacts=ent.Contacts,
                Name = ent.Name,
                Rating = ent.Rating,
                Resources = ent.Resources,
                Reviews = ent.Reviews,
                //changes in database
                //Type = ent.Type
            };

            return model;
        }

        [HttpPost]
        [Authorize(Roles = "reviewer")]
        public ActionResult SavePhoto(HttpPostedFileBase file, Enterprise ent)
        {
            if (file != null && file.ContentLength > 0)
            {
                Resource res = new Resource(file, ResourceType.MainImage, Server.MapPath("~/Content/UserResources"));

                repository.UpdateMainPhoto(ent, res);

            }

            return RedirectToAction("Index");
        }


        // changes in DB
        /*
        private Enterprise GetEnterprise()
        {
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            Enterprise reviewer = new Enterprise();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // reviewer = db.Rewievers.Find(user.IndexOfRegister);

                //backup plan

                // reviewer = db.Rewievers.Where(x => x.Email == user.Email).FirstOrDefault();
            }
            return reviewer;
        }
        */
    }

}