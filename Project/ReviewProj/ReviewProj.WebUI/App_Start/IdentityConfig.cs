using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ReviewProj.WebUI.Models;
using ReviewProj.Domain.Entities;
using ReviewProj.Domain.Concrete;

namespace ReviewProj.WebUI
{
    public static class IdentityConfig
    {
        // Тут створюємо користувачів з Ролями. 
        // admin@gmail.com
        // reviewer@gmail.com
        // owner@gmail.com
        // Pa$$word0
        public static void RegisterUsers()
        {
            AppDbContext context = AppDbContext.Create();

            ApplicationUserManager userManager =
                new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            RoleManager<IdentityRole> roleManager = 
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Admin 
            Admin admin = new Admin { Email = "admin@gmail.com", UserName = "admin@gmail.com",
            EmailConfirmed = true };
            string password = "Pa$$word0";
            var result = userManager.Create(admin, password);

            // Reviewer
            Reviewer reviewer = new Reviewer { Email = "reviewer@gmail.com", UserName = "reviewer@gmail.com",
            BirthDate = new DateTime(1993, 10, 24) };
            userManager.Create(reviewer, password);

            // Owner
            Owner owner = new Owner { Email = "owner@gmail.com", UserName = "owner@gmail.com" };
            userManager.Create(owner, password);

            IdentityRole roleAdmin = new IdentityRole { Name = "admin" };
            IdentityRole roleOwner = new IdentityRole { Name = "owner" };
            IdentityRole roleReviewer = new IdentityRole { Name = "reviewer" };

            roleManager.Create(roleAdmin);
            roleManager.Create(roleOwner);
            roleManager.Create(roleReviewer);

            userManager.AddToRole(reviewer.Id, roleReviewer.Name);
            userManager.AddToRole(owner.Id, roleOwner.Name);
            userManager.AddToRole(admin.Id, roleAdmin.Name);


            context.SaveChanges();
        }
    }
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<AppDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
