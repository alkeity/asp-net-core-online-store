using OnlineStore.Data.Models;
using OnlineStore.Models.Containers;
using OnlineStore.Models.DTO;

namespace OnlineStore.Services
{
    public interface IReviewService
    {
        public List<Review> GetReviews(long productID);
        Page<ReviewDTO> GetReviews(long productID, int page, int amount = -1);
        public void AddReview(Review review);
    }
}
