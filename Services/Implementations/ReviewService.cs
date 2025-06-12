using OnlineStore.Data.Repositories;
using OnlineStore.Models.Containers;
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

        public List<Review> GetReviews(long productID)
        {
            return _repository.GetAllForProduct(productID);
        }

        public Page<Review> GetReviews(long productID, int page, int amount = -1)
        {
            return _repository.GetAllForProduct(productID, page, amount);
        }
    }
}
