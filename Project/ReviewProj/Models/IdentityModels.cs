using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System;

namespace ReviewProj.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Owner
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public bool IsBanned { get; set; }
    }

    public class Reviewer
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public bool IsBanned { get; set; }
        public DateTime BirthDate { get; set; }
        public String Nationality { get; set; }
        public double Rating { get; set; }

        public int MyImageId { get; set; }
        [ForeignKey("MyImageId")]
        public MyImage Avatar { get; set; }
    }

    public class Admin
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }

    public class OwnerEnterprise
    {
        [Key, Column(Order = 0)]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Owner Owner { get; set; }

        [Key, Column(Order = 1)]
        public int EntId { get; set; }
        [ForeignKey("EntId")]
        public Enterprise Enterprise { get; set; }
    }

    public class Ban
    {
        [Key]
        public int BanId { get; set; }

        public string AdminId { get; set; }
        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan BanDuration { get; set; }
    }

    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public string ReviewerId { get; set; }
        [ForeignKey("ReviewerId")]
        public Reviewer Reviewer { get; set; }

        public int EntId { get; set; }
        [ForeignKey("EntId")]
        public Enterprise Enterprise { get; set; }

        public double Mark { get; set; }
        public String Description { get; set; }

        public double TotalRating { get; set; }

        public DateTime Date { get; set; }
    }

    public class ReviewVoter
    {
        [Key, Column(Order = 0)]
        public int ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public Review Review { get; set; }

        [Key, Column(Order = 1)]
        public string VoterId { get; set; }
        [ForeignKey("VoterId")]
        public Reviewer Voter { get; set; }

        public double VoteDelta { get; set; }
    }

    public class MyImage
    {
        [Key]
        public int ImageId { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        public int Type { get; set; }
    }

    public class Enterprise
    {
        [Key]
        public int EntId { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Owner Owner { get; set; }

        public string Name { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        public double Rating { get; set; }

        public int Type { get; set; }
    }

    public class EnterpriseContact
    {
        [Key, Column(Order = 0)]
        public int EntId { get; set; }
        [ForeignKey("EntId")]
        public Enterprise Enterprise { get; set; }

        [Key, Column(Order = 1)]
        public String Contact { get; set; }
    }

    public class EnterpriseImage
    {
        [Key]
        public int EntImId { get; set; }

        public int EntId { get; set; }
        [ForeignKey("EntId")]
        public Enterprise Enterprise { get; set; }

        public int MyImageId { get; set; }
        [ForeignKey("MyImageId")]
        public MyImage Image { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ReviewProj", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        DbSet<Owner> Owners { get; set; }
        DbSet<Reviewer> Reviewers { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Enterprise> Enterprises { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Ban> Bans { get; set; }
        DbSet<MyImage> Images { get; set; }
        DbSet<EnterpriseImage> EnterpriseImages { get; set; }
        DbSet<EnterpriseContact> EnterpriseContacts { get; set; }
        DbSet<ReviewVoter> ReviewVoters { get; set; }
        DbSet<OwnerEnterprise> OwnerEnterprises { get; set; }
    }
}