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
  

        [Authorize(Roles ="owner")]
        public class RegisterEnterpriseController : BaseController
    {
        private IEnterpriseRepository repository;

        public RegisterEnterpriseController(IEnterpriseRepository enterpriseRepository)
        {
            repository = enterpriseRepository;
        }

        private ApplicationSignInManager _signInManager;
            private ApplicationUserManager _userManager;
            public static EnterpriseView enter = new EnterpriseView();
        private static List<Image> Images = new List<Image>();
            static int index = 0;


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
                enter.Name = model.Name;
                enter.Address = model.Address;
                enter.Type = model.Type;
                return RedirectToAction("AddContacts", "RegisterEnterprise");
            }

            public ActionResult RegistPart2()
            {
                //enter.Contacts.Add("0965458741");
                return View(enter.Contacts);
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
                enter.Contacts.Add(model.contact);
                enter.Contacts.IndexOf("");
                return RedirectToAction("RegistPart2", "RegisterEnterprise");
            }

            public ActionResult Edit(string itemName)
            {
                enter.Contacts.Remove(itemName);
                return RedirectToAction("AddContacts", "RegisterEnterprise", itemName);
            }
            public ActionResult Delete(string itemName)
            {
                enter.Contacts.Remove(itemName);
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
            return View(enter);
        }

        [HttpPost]
        public ActionResult RegistPart4(EnterpriseView model)
        {
            enter.Description = model.Description;
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
            //зберігання в бд без ресурсів
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            Owner own = new Owner();
            var enterprise = new Enterprise();
            
            Enterprise g = new Enterprise();
            enterprise = new Enterprise
            {
                Name = enter.Name,
                // changes in database
                //Type = enter.Type,
                Contacts = enter.Contacts,
                Address = enter.Address,
                // Resources = enter.Resources,
                Description = enter.Description,
            };
            enterprise.Contacts = new List<string>();
            enterprise.Resources = new List<Resource>();
            using (var db = new AppDbContext())
            {
                own = db.Owners.Where(x => x.Id == user.Id).FirstOrDefault();
                enterprise.Owner = own;
                db.Enterprises.Add(enterprise);

                db.SaveChanges();

            }
            Enterprise fent = repository.GetEnterpriseById(enterprise.EntId);
            foreach(Image im in Images)
            {
                SavePhoto(im.file,fent);
            }
            enter = new EnterpriseView();
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
        public ActionResult SavePhoto(HttpPostedFileBase file,Enterprise ent)
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