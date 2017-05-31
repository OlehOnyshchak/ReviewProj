using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string EmailOrPhone { get; set; }
        // fkeys
        [Required]
        public virtual Enterprise Enterprise { get; set; }
    }
}
