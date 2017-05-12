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

        public ReviewerRepository()
        { }

        public ReviewerRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }

        public IQueryable<Reviewer> Reviewers
        {
            get { return context.Reviewers; }
        }

        public void UpdateMainPhoto(Reviewer reviewer, Resource resource)
        {
            List<Resource> mainImages = reviewer.Resources.Where(res => res.Type == ResourceType.MainImage).ToList();
            mainImages.ForEach(res => res.Type = ResourceType.SecondaryImage);

            reviewer.Resources.Add(resource);
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
