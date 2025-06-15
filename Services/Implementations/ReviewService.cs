using OnlineStore.Data;
using OnlineStore.Data.Models;
using OnlineStore.Models.Containers;

namespace OnlineStore.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _db;
        private readonly int REVIEWS_PER_PAGE_DEFAULT;

        public ReviewService(IConfiguration config, AppDbContext db)
        {
            _db = db;
            REVIEWS_PER_PAGE_DEFAULT = config.GetValue<int>("ReviewsPerPage");
        }

        public void AddReview(Review review)
        {
            _db.Reviews.Add(review);
            _db.SaveChanges();
        }

        public List<Review> GetReviews(long productID)
        {
            return _db.Reviews.Where(review => review.ProductID == productID).ToList();
        }

        public Page<Review> GetReviews(long productID, int page, int amount = -1)
        {
            if (page < 0) throw new ArgumentOutOfRangeException("Page number cannot be negative.");
            if (amount < 0) amount = REVIEWS_PER_PAGE_DEFAULT;

            int reviewsTotal = _db.Reviews.Where(review => review.ProductID == productID).Count();

            return new Page<Review>()
            {
                CurPage = page,
                MaxPage = Convert.ToInt32(Math.Ceiling((double)(reviewsTotal / amount))),
                ItemAmount = amount,
                Items = _db.Reviews.Where(review => review.ProductID == productID).ToList() // TODO pagination
            };
        }
    }
}
