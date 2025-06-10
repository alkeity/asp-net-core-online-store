using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories
{
    public interface IReviewRepository
    {
        public List<Review> GetAllForProduct(long productId);

        public void Create(Review review);
    }
}
