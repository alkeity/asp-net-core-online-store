using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Services
{
    public interface IReviewService
    {
        public List<Review> GetReviews(long productID);
        Page<Review> GetReviews(long productID, int page, int amount = -1);
        public void AddReview(Review review);
    }
}
