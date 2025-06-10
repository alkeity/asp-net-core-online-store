using OnlineStore.Models.Entities;

namespace OnlineStore.Services
{
    public interface IReviewService
    {
        public List<Review> GetReviews(long productID, int? amount);
        public void AddReview(Review review);
    }
}
