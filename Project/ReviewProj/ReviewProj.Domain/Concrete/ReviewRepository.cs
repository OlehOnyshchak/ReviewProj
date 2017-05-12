using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class ReviewRepository : IReviewRepository
    {
        private AppDbContext context = new AppDbContext();

        public IEnumerable<Review> Reviews
        {
            get { return context.Reviews; }
        }

        public void DeleteById(int reviewId)
        {
            context.Reviews.Remove(GetById(reviewId));
            context.SaveChanges();
        }

        public Review GetById(int reviewId)
        {
            return Reviews.FirstOrDefault(rev => rev.ReviewId == reviewId);
        }
    }
}
