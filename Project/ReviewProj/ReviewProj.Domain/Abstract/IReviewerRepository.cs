using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Abstract
{
    public interface IReviewerRepository
    {
        IQueryable<Reviewer> Reviewers { get; }
        Reviewer FindByEmail(string email);
        void UpdateMainPhoto(Reviewer reviewer, Resource fileName);
        void RemoveMainPhoto(Reviewer reviewer);
    }
}
