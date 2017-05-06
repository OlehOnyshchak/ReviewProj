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
        Ban()
        {
            StartTime = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BanId { get; set; }

        // fkeys
        [Required]
        public Admin Admin { get; set; }
        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public TimeSpan BanDuration { get; set; }
    }
}
