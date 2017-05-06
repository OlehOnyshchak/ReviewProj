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
        [Key, Column(Order = 0)]
        public virtual Review Review { get; set; }
        [Key, Column(Order = 0)]
        public virtual Reviewer Voter { get; set; }

        [Required]
        public double VoteDelta { get; set; }
    }
}
