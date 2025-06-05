using OnlineStore.Models.Domain;

namespace OnlineStore.Services
{
    public interface IReviewService
    {
        public List<Review> GetReviews(int productID, int? amount);
        public void AddReview(Review review);
    }
}
