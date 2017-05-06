using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReviewProj.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        { }

        public ApplicationUser(ApplicationUser user)
        {
            this.AccessFailedCount = user.AccessFailedCount;
            // this.Claims - read only
            this.Email = user.Email;
            this.EmailConfirmed = user.EmailConfirmed;
            this.Id = user.Id;
            this.LockoutEnabled = user.LockoutEnabled;
            this.LockoutEndDateUtc = user.LockoutEndDateUtc;
            // this.Logins - read only
            this.PasswordHash = user.PasswordHash;
            this.PhoneNumber = user.PhoneNumber;
            this.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            // this.Roles - read only
            this.SecurityStamp = user.SecurityStamp;
            this.TwoFactorEnabled = user.TwoFactorEnabled;
            this.UserName = user.UserName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
