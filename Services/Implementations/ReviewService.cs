using OnlineStore.Data.Repositories;
using OnlineStore.Models.Entities;

namespace OnlineStore.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public void AddReview(Review review)
        {
            review.Date = DateTime.Now;
            _repository.Create(review);
        }

        public List<Review> GetReviews(long productID, int? amount)
        {
            return _repository.GetAllForProduct(productID);
        }
    }
}
