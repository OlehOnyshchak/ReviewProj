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

        public void AddContact(Enterprise enterprise, string emailOrPhone)
        {
            if (emailOrPhone != null)
            {
                Contact contact = new Contact();
                contact.EmailOrPhone = emailOrPhone;
                enterprise.Contacts.Add(contact);
                context.SaveChanges();
            }
        }
        public void RemoveContact(Enterprise enterprise, int idCont)
        {
            // enterprise.Contacts.RemoveAll(e => e.ContactId == idCont);
            enterprise.Contacts.Where(e => e.ContactId == idCont).FirstOrDefault().EmailOrPhone ="deleted";
            context.SaveChanges();
           
        }
        public void EditContact(Enterprise enterprise, string emailOrPhone, int idCont)
        {
            if (emailOrPhone != null)
            { 
                enterprise.Contacts.Where(e => e.ContactId==idCont).FirstOrDefault().EmailOrPhone=emailOrPhone;
                context.SaveChanges();
            }
        }

        public void ChangeRating(Enterprise ent, double rating)
        {
            context.Enterprises.Attach(ent);
            ent.Rating = rating;

            context.SaveChanges();
        }
       public void ChangeData(Enterprise entInDb, Enterprise entNewData)
        {
            entInDb.Address = entNewData.Address;
            entInDb.Description = entNewData.Description;
            entInDb.Name = entNewData.Name;

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
            // If Enterprise has Main Image, it becomes Secondary
            List<Resource> mainImages = enterprise.Resources.Where(res => res.Type == ResourceType.MainImage).ToList();
            mainImages.ForEach(res => res.Type = ResourceType.SecondaryImage);

            // Add new Main Image
            resource.Type = ResourceType.MainImage;
            enterprise.Resources.Add(resource);
            context.SaveChanges();
        }


        public void RemovePhoto(Enterprise enterprise, int id)
        {
            enterprise.Resources.RemoveAll(res => res.ResourceId == id);
            context.SaveChanges();
        }

        public void AppointMain(Enterprise enterprise, int id)
        {
            List<Resource> mainImages = enterprise.Resources.Where(res => res.Type == ResourceType.MainImage).ToList();
            mainImages.ForEach(res => res.Type = ResourceType.SecondaryImage);
            enterprise.Resources.Where(res => res.ResourceId == id).FirstOrDefault().Type = ResourceType.MainImage;
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

        public IQueryable<Enterprise> Enterprises
        {
            get
            {
                var ents = context.Enterprises;
                foreach (var ent in ents)
                {
                    context.Entry(ent).Reference(x => x.Owner).Load();
                    context.Entry(ent).Collection(x => x.Contacts).Load();
                    context.Entry(ent).Collection(x => x.Resources).Load();
                    context.Entry(ent).Collection(x => x.Reviews).Load();
                }

                return ents;
            }
        }

        // INTEGRATION
        //public List<string> GetEnterpriseContacts(Enterprise enterprise)
        //{    
        //    context.Entry(enterprise).Collection(e => e.Contacts).Load();
        //    return enterprise.Contacts;
        //}
    }
}
