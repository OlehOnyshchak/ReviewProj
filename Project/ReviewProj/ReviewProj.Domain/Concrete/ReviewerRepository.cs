using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class ReviewerRepository : IReviewerRepository
    {
        private AppDbContext context = new AppDbContext();

        public IQueryable<Reviewer> Reviewers
        {
            get { return context.Reviewers; }
        }

        public Reviewer FindByEmail(string email)
        {
            return context.Reviewers.FirstOrDefault(r => r.Email == email);
        }
    }
}
