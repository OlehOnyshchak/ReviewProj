using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    public class Review
    {
        public Review()
        {
            TotalLikes = 0;
            TotalDislikes = 0;
            Date = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        // fkeys
        [Required]
        public virtual Reviewer Reviewer { get; set; }

        [Required]
        public virtual Enterprise Enterprise { get; set; }

        // TODO: show amount of likes and dislikes on each review
        [InverseProperty("Review")]
        public virtual List<Vote> Votes { get; set; }

        // reviewer feedback
        [Required]
        [Range(minimum: 0.0, maximum: 5.0,
            ErrorMessage = "Reviews's Mark should be in the range [0.0, 5.0]")]
        public double Mark { get; set; }

        public string Description { get; set; }

        // total like/dislike sum
        [Required]
        public int TotalLikes { get; set; }

        [Required]
        public int TotalDislikes { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
