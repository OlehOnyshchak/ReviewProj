using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    [Table("Owners")]
    public class Owner : ApplicationUser
    {
        public Owner()
        {
            IsBanned = false;
        }

        public Owner(ApplicationUser user) :
            base(user)
        {
            IsBanned = false;
        }

        [Required]
        public bool IsBanned { get; set; }

        [InverseProperty("Owner")]
        public virtual List<Enterprise> Enterprises { get; set; }
    }
}