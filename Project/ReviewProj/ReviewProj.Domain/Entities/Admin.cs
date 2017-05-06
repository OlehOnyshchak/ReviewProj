using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    [Table("Admins")]
    public class Admin : ApplicationUser
    {
        public Admin()
        {
        }

        public Admin(ApplicationUser user) :
            base(user)
        {
        }
    }
}
