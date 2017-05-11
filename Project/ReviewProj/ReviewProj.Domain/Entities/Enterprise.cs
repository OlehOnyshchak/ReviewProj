using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    public class Enterprise
    {
        public Enterprise()
        {
            Rating = 2.5;
            Type = EnterpriceType.Restaurant;
        }

        // properties
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EntId { get; set; }

        // fkeys
        [Required]
        public virtual Owner Owner { get; set; }

        public virtual Address Address { get; set; }

        public virtual List<string> Contacts { get; set; }

        public virtual List<Resource> Resources { get; set; }

        [InverseProperty("Enterprise")]
        public virtual List<Review> Reviews { get; set; }

        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        [Range(minimum: 0.0, maximum: 5.0,
            ErrorMessage = "Enterprise's Rating should be in the range [0.0, 5.0]")]
        public double Rating { get; set; }

        [Required]
        public EnterpriceType Type { get; set; }
    }

    public enum EnterpriceType
    {
        Restaurant,
        Hotel,
        FastFood,
        Club,
        Cafe
    }
}
