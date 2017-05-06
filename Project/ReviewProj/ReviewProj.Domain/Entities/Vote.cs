using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewProj.Domain.Entities
{
    public class Vote
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }

        [Index("IX_Vote", 1, IsUnique = true)]
        public virtual Review Review { get; set; }
        [Index("IX_Vote", 2, IsUnique = true)]
        public virtual Reviewer Voter { get; set; }

        [Required]
        public double VoteDelta { get; set; }
    }
}
