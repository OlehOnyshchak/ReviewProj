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

        public ReviewerRepository()
        { }

        public ReviewerRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }

        // Get All Reviewers
        public IQueryable<Reviewer> Reviewers
        {
            get { return context.Reviewers; }
        }

        // Update Main Photo
        public void UpdateMainPhoto(Reviewer reviewer, Resource resource)
        {
            // If Reviewer has Main Image, it becomes Secondary
            List<Resource> mainImages = reviewer.Resources.Where(res => res.Type == ResourceType.MainImage).ToList();
            mainImages.ForEach(res => res.Type = ResourceType.SecondaryImage);

            // Add new Main Image
            resource.Type = ResourceType.MainImage;
            reviewer.Resources.Add(resource);
            context.SaveChanges();
        }

        // Remove Main Photo if they exist
        public void RemoveMainPhoto(Reviewer reviewer)
        {
            reviewer.Resources.RemoveAll(res => res.Type == ResourceType.MainImage);
            context.SaveChanges();
        }

        // Update Reviewer Info
        // Return Updated Reviewer
        public Reviewer UpdateEntry(Reviewer existing, Reviewer updated)
        {
            context.Entry(existing).State = EntityState.Modified;
            if (updated.BirthDate != DateTime.MinValue)
                existing.BirthDate = updated.BirthDate;

            existing.Nationality = updated.Nationality;

            context.SaveChanges();

            return existing;
        }

        // Find Reviewer by Email
        public Reviewer FindByEmail(string email)
        {
            return context.Reviewers.FirstOrDefault(r => r.Email == email);
        }
    }
}
