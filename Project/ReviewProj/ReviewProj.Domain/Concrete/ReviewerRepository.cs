using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Entities;
using System.Data;
using System.Data.Entity;

namespace ReviewProj.Domain.Concrete
{
    public class ReviewerRepository : IReviewerRepository
    {
        private AppDbContext context = new AppDbContext();

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


        public Reviewer UpdateEntry(Reviewer existing, Reviewer updated)
        {
            context.Entry(existing).State = EntityState.Modified;
            if (updated.BirthDate != DateTime.MinValue)
                existing.BirthDate = updated.BirthDate;

            existing.Nationality = updated.Nationality;

            context.SaveChanges();

            return existing;
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
