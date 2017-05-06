using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext()
            : base("ReviewProj_1.5", throwIfV1Schema: false)
        { }

        public static DBContext Create()
        {
            return new DBContext();
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
