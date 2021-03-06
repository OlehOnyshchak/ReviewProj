﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Entities;
using ReviewProj.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewProj.WebUI.Controllers
{
    [Authorize]
    public class Owner1Controller : BaseController
    {
        private IOwnerRepository repository;
        private IEnterpriseRepository enterRepositority;
        public Owner1Controller(IOwnerRepository ownerRepository, IEnterpriseRepository enterRepositority)
        {
            repository = ownerRepository;
            this.enterRepositority = enterRepositority;
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public static Enterprise entResult = new Enterprise();
        public static int id = 0;
        public static int idContact = 0;
        private static string tempNameContact;

        public Owner1Controller()
        {
        }

        public Owner1Controller(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Owner
        public ActionResult Index()
        {
            return RedirectToAction("ProfileOwner", "Owner1");
        }
        public ActionResult ProfileOwner()
        {
            return View(GetModel());
        }
        public ActionResult EnterprisesList()
        {
            return View(getModelList());
        }

        public ActionResult SaveId(int ID)
        {
            Owner own = new Owner();
            own = GetOwner();
            foreach (Enterprise ent in own.Enterprises)
            {
                if (ent.EntId == ID)
                {
                    id = ID;
                    entResult = ent;
                }
            }
            return RedirectToAction("DetailsEnterprise", "Owner1");
        }

        public ActionResult DetailsEnterprise()
        {
            return View(getEnterprise(entResult));
        }
        public ActionResult AddContact()
        {
            var Model = new AddContacts();
            return View(Model);
        }
        [HttpPost]
        public ActionResult AddContact(AddContacts model)
        {
            Enterprise ent = enterRepositority.GetEnterpriseById(id);
            enterRepositority.AddContact(ent, model.contact);
            entResult = ent;
            return RedirectToAction("DetailsEnterprise", "Owner1");
        }

        public ActionResult EditContact(int idCont)
        {
            Contact cont = new Contact();
            cont = entResult.Contacts.Where(e => e.ContactId == idCont).FirstOrDefault();
            var Model = new AddContacts();
            idContact = idCont;
            Model.contact = cont.EmailOrPhone;
            tempNameContact = cont.EmailOrPhone;
            return View(Model);
        }

        [HttpPost]
        public ActionResult EditContact(AddContacts model)
        {
            Enterprise ent = enterRepositority.GetEnterpriseById(id);
            enterRepositority.EditContact(ent, model.contact,idContact);

          tempNameContact = null;
            idContact = 0;
            entResult = ent;
            return RedirectToAction("DetailsEnterprise", "Owner1");
        }

        public ActionResult DeleteContact(int idCont)
        {
            Enterprise ent = enterRepositority.GetEnterpriseById(id);
            enterRepositority.RemoveContact(ent, idCont);
            entResult = ent;
            return RedirectToAction("DetailsEnterprise", "Owner1");
        }
        public ActionResult EditRestData()
        {
            return View(getEnterprise(entResult));
        }
        [HttpPost]
        public ActionResult EditRestData(Enterprise model)
        {
            entResult.Name = model.Name;
            entResult.Address = model.Address;
            entResult.Type = model.Type;
            entResult.Description = model.Description;
            return RedirectToAction("DetailsEnterprise", "Owner1");
        }

        public ActionResult SaveChange()
        {
            //потребує зберігання змін в бд для елемента Enterprise з індексом, що зберігається в статичній змінній id, з ліста даного власника
            //дані для зміни в статичній змінній entResult
            Enterprise ent = enterRepositority.GetEnterpriseById(id);
            enterRepositority.ChangeData(ent, entResult);

            //занулення статичних змінних
            id = 0;
            entResult = new Enterprise();


            return RedirectToAction("Index", "Owner1");
        }
        private OwnerView GetModel()
        {

            Owner own = new Owner();
            own = GetOwner();
            //нодаємо моделі параметри власника
            var model = new OwnerView()
            {
                HasPassword = HasPassword(),
                Enterprises = own.Enterprises
            };
            model.CountEnterprises = model.Enterprises.Count();
            return model;
        }

        private Owner GetOwner()
        {
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            Owner own = new Owner();
            own.Enterprises = new List<Enterprise>();
            own = repository.FindByEmail(user.Email);

            foreach(Enterprise ent in own.Enterprises)
            {
                if(ent != null)
                {

                  //  ent.Contacts = enterRepositority.getList(ent);

                    // INTEGRATE
 //                   ent.Contacts = enterRepositority.GetEnterpriseContacts(ent);

                }
            }
            /* using (var db = new AppDbContext())
             {
                 //почук власника вбд за емейлом користувача що наразі в мережі

                 reviewer = db.Reviewers.Find(user.IndexOfRegister);*/

            //backup plan
            /*  foreach (var f in db.Enterprises)
              {
                  if(f.Owner.Id==own.Id)
                  {
                     own.Enterprises.Add(f);
                  }
              }

          }*/

            /*  Address ad = new Address
              {
                  City = "Lviv",
                  Street = "Str",
                  HouseNumber = "42a"
              };
              List<String> cont = new List<string>();
              cont.Add("0985478551");
              cont.Add("ghad@gmail.com");

              own.Enterprises.Add(new Enterprise
              {
                  Name = "Glories",
                  Address = ad,
                  Contacts = cont,
                  Description = "In a professional context it often happens that private or corporate clients corder a publication to be made and presented with the actual content still not being ready. Think of a news blog that's filled with content hourly on the day of going live. However, reviewers tend to be distracted by comprehensible content, say, a random text copied from a newspaper or the internet. The are likely to focus on the text, disregarding the layout and its elements. Besides, random text risks to be unintendedly humorous or offensive, an unacceptable risk in corporate environments. Lorem ipsum and its many variants have been employed since the early 1960ies, and quite likely since the sixteenth century."
              });
              */
            return own;
        }

        public Enterprise1 getEnterprise(Enterprise ent)
        {
            Enterprise1 newent = new Enterprise1();
            newent.Address = ent.Address;
            // INTEGRATE
            newent.Contacts = ent.Contacts;
            //newent.Contacts = ent.Contacts;
            newent.Description = ent.Description;
            newent.Name = ent.Name;
            newent.Rating = ent.Rating;
            newent.EntId = ent.EntId;
            newent.Resources = ent.Resources;

            return newent;
        }

        public List<Enterprise1> getModelList()
        {
            List<Enterprise1> newList = new List<Enterprise1>();

            foreach (Enterprise ent in GetOwner().Enterprises)
            {
                Enterprise1 ent1 = getEnterprise(ent);
                newList.Add(ent1);
            }

            return newList;
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        [HttpGet]
        [Authorize(Roles = "owner")]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "owner")]
        public ActionResult AddPhoto(HttpPostedFileBase file)
        {
            
            if (file != null && file.ContentLength > 0)
            {
                Enterprise ent = enterRepositority.GetEnterpriseById(id);
                Resource res = new Resource(file, ResourceType.MainImage, Server.MapPath("~/Content/UserResources"));

                enterRepositority.UpdateMainPhoto(ent, res);
                entResult = ent;

            }
            
            return RedirectToAction("DetailsEnterprise");
        }

        public ActionResult DeletePhoto(int ID)
        {
            Enterprise enter = enterRepositority.GetEnterpriseById(id);
            enterRepositority.RemovePhoto(enter,ID);
            entResult = enter;
            return RedirectToAction("DetailsEnterprise");
        }
        public ActionResult AppointMain(int ID)
        {
            Enterprise enter = enterRepositority.GetEnterpriseById(id);
            enterRepositority.AppointMain(enter, ID);
            entResult = enter;
            return RedirectToAction("DetailsEnterprise");
        }
    }
}