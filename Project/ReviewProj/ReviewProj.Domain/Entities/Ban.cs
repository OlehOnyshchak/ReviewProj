using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    public class Ban
    {
        public Ban()
        {
            StartTime = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BanId { get; set; }

        // fkeys
        [Required]
        public virtual Admin Admin { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
