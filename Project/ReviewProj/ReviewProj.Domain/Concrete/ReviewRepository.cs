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

        public ReviewRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }

        public void VoteForReview(int reviewID, string reviewerEmail, bool isLike)
        {
            Review review =  GetById(reviewID);
            context.Reviews.Attach(review);

            Reviewer reviewer = context.Reviewers
                .Where(r => r.Email == reviewerEmail)
                .First();
            
            context.Reviewers.Attach(reviewer);

            Vote vote = new Vote();
            vote.VoteDelta = (isLike) ? 1.0 : -1.0;

            vote.Review = review;
            vote.Voter = reviewer;

            review.Votes.Add(vote);

            if (isLike)
            {
                review.TotalLikes++;
            }
            else
            {
                review.TotalDislikes++;
            }

            context.Entry(review).Reference(r => r.Enterprise).Load();
            context.Entry(review).Reference(r => r.Reviewer).Load();

            context.SaveChanges();
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
