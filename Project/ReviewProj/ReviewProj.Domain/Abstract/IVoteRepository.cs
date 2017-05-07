using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Abstract
{
    public interface IVoteRepository
    {
        IQueryable<Vote> Votes { get; }
    }
}
