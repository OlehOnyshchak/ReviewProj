using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ReviewProj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewProj.Controllers
{
    [Authorize]
    public class ProfileViewController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ProfileViewController()
        {
        }

        public ProfileViewController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: ProfileView
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
        public ActionResult EditProfile(ProfileViewData Model)
        {
            Reviewer reviewer = new Reviewer();
            reviewer = GetReviewer();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //зберігання оновленихданих в бд
                /*
                db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().Name = Model.Name.ToString();
                db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().Surname = Model.Surname.ToString();
                db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().sex=Model.Sex;
                db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().age = Model.Age.ToString();
                db.SaveChanges();
                */
            }

            return RedirectToAction("Index", "ProfileView");
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

        private bool HasPhoto()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            //перевірка чи в користувача є фото
            /*
            if (user != null&& bdUsers.Reviewers.Where(x => x.Email == User.Identity.Name).FirstOrDefault().ReviewerPhoto != null)
            {
                return true;
            }
            */
            return false;
        }

        private ProfileViewData GetModel()
        {

            Reviewer reviewer = new Reviewer();
            reviewer = GetReviewer();
            var model = new ProfileViewData
            {
                HasPassword = HasPassword(),
                HasPhoto = HasPhoto(),
                Nationality = reviewer.Nationality,
                Rating = reviewer.Rating,
                IsBanned = reviewer.IsBanned,
                BirthDate = reviewer.BirthDate
            };
            return model;
        }

        private Reviewer GetReviewer()
        {
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            Reviewer reviewer = new Reviewer();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //почук рев`ювера вбд за емейлом користувача що наразі в мережі
                /*
                // reviewer = db.Reviewers.Find(user.IndexOfRegister);

                //backup plan
               reviewer = db.Reviewers.Where(x => x.Email == user.Email).FirstOrDefault();*/
            }
            return reviewer;
        }

        public ActionResult DeleteProfile()
        {
            return View();
        }

        public ActionResult DeletePhoto()
        {
            Reviewer reviewer = new Reviewer();
            reviewer = GetReviewer();
            var db = new ApplicationDbContext();
            //видалення фото
            //db.Reviewers.Where(x => x.Id == reviewer.Id).FirstOrDefault().ReviewerPhoto = null;
            db.SaveChanges();
            return RedirectToAction("Index", "ProfileView");
        }

        public ActionResult AddPhoto()
        {
            AddPhotoModel photo = new AddPhotoModel();
            return View(photo);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(AddPhotoModel model, HttpPostedFileBase photo1)
        {
            Reviewer reviewer = new Reviewer();
            reviewer = GetReviewer();
            var db = new ApplicationDbContext();
            if (photo1 != null)
            {
                model.Photo = new byte[photo1.ContentLength];
                photo1.InputStream.Read(model.Photo, 0, photo1.ContentLength);
            }

            //додаванн фото
            //db.Reviewers.Where(x => x.Id ==reviewer.Id).FirstOrDefault().ReviewerPhoto = model.Photo;
            db.SaveChanges();

            return RedirectToAction("Index", "ProfileView");

        }
        public FileContentResult UserPhotos()
        {
            String userId = User.Identity.GetUserId();
            var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            /*перевірка чи в користувача є фото
            if (User.Identity.IsAuthenticated && userId != null && bdUsers.Reviewers.Where(x => x.Email == User.Identity.Name).FirstOrDefault().ReviewerPhoto!=null)
            {     
                    var userImage = bdUsers.Reviewers.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

                    return new FileContentResult(userImage.ReviewerPhoto, "image/jpeg");
                
            }*/
            if (false)
            {

            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }


}