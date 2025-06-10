using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Models.Entities;

namespace OnlineStore.Services.Implementations
{
    public class JsonReviewService : IReviewService
    {
        IDatabaseService _databaseService;

        public JsonReviewService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public void AddReview(Review review)
        {
            List<Review> reviews = (List<Review>)_databaseService.GetItems(null);
            review.Date = DateTime.Now;
            review.Id = reviews.Count > 0 ? reviews.Max(r => r.Id) + 1 : 0;
            reviews.Add(review);
            _databaseService.AddItems(reviews);
        }

        public List<Review> GetReviews(long productID, int? amount)
        {
            List<Review> reviews = (List<Review>)_databaseService.GetItems(null);
            reviews = reviews.Where(review => review.ProductID == productID).ToList();
            reviews.Sort((review1, review2) => review2.Date.CompareTo(review1.Date));
            return reviews.Count > amount ? reviews.GetRange(0, (int)amount) : reviews;
        }
    }
}
