using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.IO;
using ReviewProj.Domain.Entities;

namespace ReviewProj.WebUI.Models
{
    public class OwnerView
    {
        public bool HasPassword { get; set; }
        public bool IsBanned { get; set; }
        public List<Enterprise> Enterprises = new List<Enterprise>();
        public int CountEnterprises { get; set; }

    }

    public class EnterpriseViewForOwner
    {
        public Address Address { get; set; }
        public List<String> Contacts = new List<string>();
        public List<Resource> Resources = new List<Resource>();

        public List<Review> Reviews = new List<Review>();

        public string Description { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int Type { get; set; }
    }

    public class Enterprise1
    {
        [Key]
        public int EntId { get; set; }

        public Owner Owner { get; set; }
        public Address Address { get; set; }
        public List<Contact> Contacts = new List<Contact>();
        public List<Resource> Resources = new List<Resource>();
        public List<Review> Reviews = new List<Review>();

        public string Description { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int Type { get; set; }
    }
}