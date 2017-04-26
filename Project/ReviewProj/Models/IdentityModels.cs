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

    [Table("Owners")]
    public class Owner : ApplicationUser
    {
        public bool IsBanned { get; set; }

        [InverseProperty("Owner")]
        public List<Enterprise> Enterprises { get; set; }
    }

    [Table("Reviewers")]
    public class Reviewer : ApplicationUser
    {
        public bool IsBanned { get; set; }
        public DateTime BirthDate { get; set; }
        public String Nationality { get; set; }
        public double Rating { get; set; }

        //public int MyImageId { get; set; }
        //[ForeignKey("MyImageId")]
  //      public Resource Avatar { get; set; }

        [Column(TypeName = "varbinary")] // max(varbinary) = 5 Mb
        public byte[] Avatar { get; set; }
    }

    [Table("Admins")]
    public class Admin : ApplicationUser
    {
    }

    public class Ban
    {
        [Key]
        public int BanId { get; set; }

        //public string AdminId { get; set; }
        //[ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        //public string UserId { get; set; }
        //[ForeignKey("UserId")]  
        public ApplicationUser User { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan BanDuration { get; set; }
    }

    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        //public string ReviewerId { get; set; }
        //[ForeignKey("ReviewerId")]
        public Reviewer Reviewer { get; set; }

        //public int EntId { get; set; }
        //[ForeignKey("EntId")]
        public Enterprise Enterprise { get; set; }

        public List<Vote> Votes;

        public double Mark { get; set; }
        public String Description { get; set; }

        public double TotalRating { get; set; }
        public DateTime Date { get; set; }
    }

    [ComplexType]
    public class Vote
    {
        public Review Review { get; set; }
        public Reviewer Voter { get; set; }
        public double VoteDelta { get; set; }
    }

    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [Column(TypeName = "varbinary")] // max(varbinary) = 5 Mb
        public byte[] Data { get; set; }
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
        [Key]
        public int EntId { get; set; }

        // fkeys
        public Owner Owner { get; set; }
        public Address Address { get; set; }
        public List<String> Contacts { get; set; }
        public List<Resource> Resources { get; set; }
        [InverseProperty("Enterprise")]
        public List<Review> Reviews { get; set; }

        public string Name { get; set; }
        public double Rating { get; set; }
        public int Type { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ReviewProj_1.4", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Resource> Resources { get; set; }

        //public System.Data.Entity.DbSet<ReviewProj.Models.Enterprise> Enterprises { get; set; }
    }
}