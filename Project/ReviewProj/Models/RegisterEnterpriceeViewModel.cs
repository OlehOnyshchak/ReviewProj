using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace ReviewProj.Models
{
    public class EnterpriseView
    {
        public Address Address { get; set; }
        public List<String> Contacts = new List<String>();
        public List<Resource> Resources = new List<Resource>();
        public List<Review> Reviews = new List<Review>();

        public string Name { get; set; }
        public double Rating { get; set; }
        public int Type { get; set; }


    }
    public class Cont
    {
        public List<String> Contacts = new List<String>();
    }


    public class AddPhotoModelEnterprise
    {
        [Required]
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
    }

    public class AddContacts
    {
        [Required]
        public String contact { get; set; }
    }

}