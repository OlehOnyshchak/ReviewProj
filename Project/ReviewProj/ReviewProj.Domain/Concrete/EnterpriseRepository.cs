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

        public IEnumerable<Enterprise> GetByName(string subName)
        {
            return string.IsNullOrEmpty(subName) ? 
                Enterprises : Enterprises.Where(ent => ent.Name.Contains(subName));
        }

        public IEnumerable<Enterprise> GetByType(EnterpriceType type)
        {
            return Enterprises.Where(ent => ent.Type == type);
        }

        public IEnumerable<Enterprise> GetByRating(int rating)
        {
            return Enterprises.Where(ent => ent.Rating == rating);
        }

        public IEnumerable<Enterprise> GetByTypeAndName(EnterpriceType type, string subName)
        {
            return this.GetByType(type).Intersect<Enterprise>(this.GetByName(subName)); 
        }

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

        public Enterprise GetEnterpriseById(int id)
        {
            return Enterprises.FirstOrDefault(ent => ent.EntId == id);
        }

        public void AddReview(int entId, string reviewerEmail, Review review)
        {
            review.Reviewer = new ReviewerRepository(context).FindByEmail(reviewerEmail);
            GetEnterpriseById(entId).Reviews.Add(review);
            context.SaveChanges();
        }

        // INTEGRATION
        //public void AddContact(Enterprise enterprise, string cont)
        //{
        //    enterprise.Contacts.Add(cont);
        //    context.SaveChanges();
        //}

        public void ChangeRating(Enterprise ent, double rating)
        {
            context.Entry(ent).State = EntityState.Modified;
            ent.Rating = rating;

            context.SaveChanges();
        }

        // INTEGRATION
        //public void AddListContacts(Enterprise ent)
        //{
        //    ent.Contacts = new List<string>();
        //    context.SaveChanges();
        //}


        //public void DeleteReview(int entId, int reviewId)
        //{
        //    Review review = GetEnterpriseById(entId).Reviews.FirstOrDefault(rev => rev.ReviewId == reviewId);
        //    GetEnterpriseById(entId).Reviews.RemoveAll(rev => rev.ReviewId == reviewId);
        //    context.SaveChanges();
        //}

        public void UpdateMainPhoto(Enterprise enterprise, Resource resource)
        {
            List<Resource> mainImages = enterprise.Resources.Where(res => res.Type == ResourceType.MainImage).ToList();
            mainImages.ForEach(res => res.Type = ResourceType.SecondaryImage);

            enterprise.Resources.Add(resource);
            context.SaveChanges();
        }

        public void RemoveMainPhoto(Enterprise enterprise)
        {
            enterprise.Resources.RemoveAll(res => res.Type == ResourceType.MainImage);
            context.SaveChanges();
        }

        public IQueryable<Enterprise> Enterprises
        {
            get { return context.Enterprises; }
        }

        // INTEGRATION
        //public List<string> GetEnterpriseContacts(Enterprise enterprise)
        //{    
        //    context.Entry(enterprise).Collection(e => e.Contacts).Load();
        //    return enterprise.Contacts;
        //}
    }
}
