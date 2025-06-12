using OnlineStore.Models.Containers;
using OnlineStore.Models.Entities;

namespace OnlineStore.Data.Repositories
{
    public interface IReviewRepository
    {
        public List<Review> GetAllForProduct(long productId);

        public Page<Review> GetAllForProduct(long productId, int page, int amount);

        public void Create(Review review);
    }
}
