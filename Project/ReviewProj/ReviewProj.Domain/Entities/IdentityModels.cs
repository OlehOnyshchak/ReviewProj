﻿using Microsoft.AspNet.Identity;
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

    [Table("Owners")]
    public class Owner : ApplicationUser
    {
        public Owner() { }
        public Owner(ApplicationUser user) :
            base(user)
        { }

        public bool IsBanned { get; set; }

        [InverseProperty("Owner")]
        public List<Enterprise> Enterprises { get; set; }
    }

    [Table("Reviewers")]
    public class Reviewer : ApplicationUser
    {
        // NONE: check if 
        public Reviewer() { }
        // NOTE: check if this constructor don't create duplicates inside ApplicationUser table.
        // if you uncounted an error during adding to DB one of the ApplicationUser's inherited classes,
        // check if this error is related to "Primary key is already exist in ApplicationUser table" (AspNetUsers)
        public Reviewer(ApplicationUser user) :
            base(user)
        { }

        public bool IsBanned { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public double Rating { get; set; }
        public string AvatarPath { get; set; }
    }

    [Table("Admins")]
    public class Admin : ApplicationUser
    {
        public Admin() { }
        public Admin(ApplicationUser user) :
            base(user)
        { }
    }

    public class Ban
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BanId { get; set; }

        // fkeys
        public Admin Admin { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan BanDuration { get; set; }
    }

    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        // fkeys
        public Reviewer Reviewer { get; set; }
        public Enterprise Enterprise { get; set; }
        // TODO: show amount of likes and dislikes on each review
        [InverseProperty("Review")]
        public List<Vote> Votes;

        // reviewer feedback
        public double Mark { get; set; }
        public string Description { get; set; }

        // total like/dislike sum
        public double TotalRating { get; set; }
        public DateTime Date { get; set; }
    }

    public class Vote
    {
        [Key, Column(Order = 0)]
        public Review Review { get; set; }
        [Key, Column(Order = 0)]
        public Reviewer Voter { get; set; }

        public double VoteDelta { get; set; }
    }

    public class Resource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        public string DataPath { get; set; }

        // logical type of data, e.g. main picture
        public int Type { get; set; }
    }

    [ComplexType]
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }

    public class Enterprise
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EntId { get; set; }

        // fkeys
        public Owner Owner { get; set; }

        public Address Address { get; set; }
        public List<string> Contacts { get; set; }
        public List<Resource> Resources { get; set; }
        [InverseProperty("Enterprise")]
        public List<Review> Reviews { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int Type { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ReviewProj_1.5", throwIfV1Schema: false)
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Ban> Bans { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
    }
}