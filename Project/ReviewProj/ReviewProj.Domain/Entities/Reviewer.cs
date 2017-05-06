using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    [Table("Reviewers")]
    public class Reviewer : ApplicationUser
    {
        public Reviewer()
        {
            IsBanned = false;
            RegistrationDate = DateTime.Now;
            Rating = 0.0;
        }
        public Reviewer(ApplicationUser user) :
            base(user)
        {
            IsBanned = false;
            RegistrationDate = DateTime.Now;
            Rating = 0.0;
        }

        [Required]
        public bool IsBanned { get; set; }
        // NOTE: you'll have an sql error, if you add Review to DB
        // with default BirthDate.year. Minimum supported year by sql server is 1777 or so
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public string Nationality { get; set; }

        [Required]
        public double Rating { get; set; }

        public string AvatarPath { get; set; }
    }
}
