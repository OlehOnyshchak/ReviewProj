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

        public void UpdateMainPhoto(Reviewer reviewer, string fileName)
        {
            reviewer.Resources.ForEach(res => res.Type = ResourceType.SecondaryImage);
            reviewer.Resources.Add(new Resource
            {
                DataPath = fileName,
                Type = ResourceType.MainImage
            });

            context.SaveChanges();

        }

        public Reviewer FindByEmail(string email)
        {
            return context.Reviewers.FirstOrDefault(r => r.Email == email);
        }

        public void RemoveMainPhoto(Reviewer reviewer)
        {
            reviewer.Resources.RemoveAll(res => res.Type == ResourceType.MainImage);
            context.SaveChanges();
        }
    }
}
