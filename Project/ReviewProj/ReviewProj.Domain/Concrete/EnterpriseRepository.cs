using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;
using System.Data.Entity;

namespace ReviewProj.Domain.Concrete
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private AppDbContext context = new AppDbContext();


        // Return All Enterprises
        public IQueryable<Enterprise> Enterprises
        {
            get { return context.Enterprises; }
        }

        // Search by SubName
        public IEnumerable<Enterprise> GetByName(string subName)
        {
            return string.IsNullOrEmpty(subName) ? 
                Enterprises : Enterprises.Where(ent => ent.Name.Contains(subName));
        }

        // Search by Type
        public IEnumerable<Enterprise> GetByType(EnterpriceType type)
        {
            return Enterprises.Where(ent => ent.Type == type);
        }

        // Search by Rating
        public IEnumerable<Enterprise> GetByRating(int rating)
        {
            return Enterprises.Where(ent => ent.Rating == rating);
        }

        // Search by Type & Name
        public IEnumerable<Enterprise> GetByTypeAndName(EnterpriceType type, string subName)
        {
            return this.GetByType(type).Intersect<Enterprise>(this.GetByName(subName)); 
        }

        // Search by Ratings, Types and SubName
        public IEnumerable<Enterprise> GetFiltratedByName(IList<int> ratings, IList<EnterpriceType> types, string subName)
        {
            IEnumerable<Enterprise> entWithName = this.GetByName(subName);

            IEnumerable<Enterprise> result;
            if (ratings.Count == 0 && types.Count == 0)
            {
                result = entWithName;
            }
            else if (ratings.Count == 0)
            {
                result = entWithName.Where(ent => types.Contains(ent.Type));
            }
            else if (types.Count == 0)
            {
                result = entWithName.Where(ent => ratings.Contains(Convert.ToInt32(ent.Rating)));
            }
            else
            {
                result = entWithName.Where(ent => ratings.Contains(Convert.ToInt32(ent.Rating)) &&
                    types.Contains(ent.Type));
            }

            return result;
        }

        // Find Enterprise by ID
        public Enterprise GetEnterpriseById(int id)
        {
            return Enterprises.FirstOrDefault(ent => ent.EntId == id);
        }

        // Adding Review
        public void AddReview(int entId, string reviewerEmail, Review review)
        {
            review.Reviewer = new ReviewerRepository(context).FindByEmail(reviewerEmail);
            GetEnterpriseById(entId).Reviews.Add(review);
            context.SaveChanges();
        }

        // Change Rating
        public void ChangeRating(Enterprise ent, double rating)
        {
            context.Entry(ent).State = EntityState.Modified;
            ent.Rating = rating;

            context.SaveChanges();
        }

        // Update Main Photo
        public void UpdateMainPhoto(Enterprise enterprise, Resource resource)
        {
            // If Enterprise has Main Image, it becomes Secondary
            List<Resource> mainImages = enterprise.Resources.Where(res => res.Type == ResourceType.MainImage).ToList();
            mainImages.ForEach(res => res.Type = ResourceType.SecondaryImage);

            // Add new Main Image
            resource.Type = ResourceType.MainImage;
            enterprise.Resources.Add(resource);
            context.SaveChanges();
        }

        // Remove Main Photo
        public void RemoveMainPhoto(Enterprise enterprise)
        {
            enterprise.Resources.RemoveAll(res => res.Type == ResourceType.MainImage);
            context.SaveChanges();
        }

        // Delete Enterprise
        public void DeleteEnterprise(int id)
        {
            Enterprise enterprise = GetEnterpriseById(id);

            // Delete all Reviews of the Enterprise
            foreach (Review review in context.Reviews.ToList())
            {
                if (review.Enterprise.EntId == id)
                {
                    context.Reviews.Remove(review);
                }
            }

            // Delete all Resources of the Enterprise
            foreach (Resource resource in context.Resources.ToList())
            {
                if (enterprise.Resources.Select(r => r.ResourceId)
                    .Contains(resource.ResourceId))
                {
                    context.Resources.Remove(resource);
                }
            }

            // Delete the Enterprise
            context.Enterprises.Remove(enterprise);
            context.SaveChanges();
        }
    }
}
